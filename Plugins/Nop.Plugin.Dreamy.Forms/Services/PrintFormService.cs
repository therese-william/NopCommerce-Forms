using iTextSharp.text;
using iTextSharp.text.pdf;
using Nop.Core;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Tax;
using Nop.Plugin.Dreamy.Forms.Models;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Stores;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Dreamy.Forms.Services
{
    public class PrintFormService : IPrintFormService
    {

        #region Fields
        protected readonly IFormService _formService;

        private readonly ILocalizationService _localizationService;
        private readonly ILanguageService _languageService;
        private readonly IWorkContext _workContext;
        private readonly IMeasureService _measureService;
        private readonly IPictureService _pictureService;
        private readonly IStoreService _storeService;
        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingContext;
        private readonly IWebHelper _webHelper;

        private readonly MeasureSettings _measureSettings;
        private readonly PdfSettings _pdfSettings;
        #endregion

        #region Ctor
        public PrintFormService(IFormService formService,
        ILocalizationService localizationService,
        ILanguageService languageService,
        IWorkContext workContext,
        IMeasureService measureService,
        IPictureService pictureService,
        IStoreService storeService,
        IStoreContext storeContext,
        ISettingService settingContext,
        IWebHelper webHelper,
        MeasureSettings measureSettings,
        PdfSettings pdfSettings)
        {
            _formService = formService;
            _localizationService = localizationService;
            _languageService = languageService;
            _workContext = workContext;
            _measureService = measureService;
            _pictureService = pictureService;
            _storeService = storeService;
            _storeContext = storeContext;
            _settingContext = settingContext;
            _webHelper = webHelper;
            _measureSettings = measureSettings;
            _pdfSettings = pdfSettings;
        }
        #endregion

        #region Utilities

        protected virtual Font GetFont()
        {
            //nopCommerce supports unicode characters
            //nopCommerce uses Free Serif font by default (~/App_Data/Pdf/FreeSerif.ttf file)
            //It was downloaded from http://savannah.gnu.org/projects/freefont
            string fontPath = Path.Combine(_webHelper.MapPath("~/App_Data/Pdf/"), _pdfSettings.FontFileName);
            var baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            var font = new Font(baseFont, 10, Font.NORMAL);
            return font;
        }
        #endregion

        public string CreateFormPdf(int SubmissionId)
        {
            string filePath = "";
            var form = _formService.GetSubmissionForPreview(SubmissionId);
            filePath = PrintFormToPdf(form);
            return filePath;
        }
        public virtual string PrintFormToPdf(SubmissionModel form)
        {
            if (form == null)
                throw new ArgumentNullException("form");

            string fileName = string.Format("form_{0}_{1}.pdf", form.FormName, CommonHelper.GenerateRandomDigitCode(4));
            string filePath = Path.Combine(_webHelper.MapPath("~/content/files/ExportImport"), fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                if (fileStream == null)
                    throw new ArgumentNullException("fileStream");

                if (form.SubmissionFields == null)
                    throw new ArgumentNullException("Form Fields");

                var submission = _formService.GetFormSubmission(form.SubmissionId);

                var pageSize = PageSize.A4;

                if (_pdfSettings.LetterPageSizeEnabled)
                {
                    pageSize = PageSize.LETTER;
                }

                var doc = new Document(pageSize);
                PdfWriter.GetInstance(doc, fileStream);
                doc.Open();

                //fonts
                var titleFont = GetFont();
                titleFont.SetStyle(Font.BOLD);
                titleFont.Color = BaseColor.BLACK;
                var font = GetFont();

                var formTable = new PdfPTable(2);
                formTable.WidthPercentage = 100f;
                formTable.DefaultCell.Border = Rectangle.NO_BORDER;
                formTable.SetWidths(new[] { 0.2f, 0.8f });


                var pdfSettingsByStore = _settingContext.LoadSetting<PdfSettings>(_storeContext.CurrentStore.Id);

                #region Header

                //logo
                var logoPicture = _pictureService.GetPictureById(pdfSettingsByStore.LogoPictureId);
                var logoExists = logoPicture != null;

                //header
                var headerTable = new PdfPTable(logoExists ? 2 : 1);
                //headerTable.RunDirection = GetDirection(lang);
                headerTable.DefaultCell.Border = Rectangle.NO_BORDER;

                //store info
                var store = _storeContext.CurrentStore;
                var anchor = new Anchor(store.Url.Trim(new[] { '/' }), font);
                anchor.Reference = store.Url;

                var cellHeader = new PdfPCell(new Phrase(form.FormName + " submitted on "+submission.SubmitDate.ToShortDateString() , titleFont));
                cellHeader.Phrase.Add(new Phrase(Environment.NewLine));
                cellHeader.Phrase.Add(new Phrase(anchor));
                //cellHeader.Phrase.Add(new Phrase(Environment.NewLine));
                //cellHeader.Phrase.Add(new Phrase(String.Format(_localizationService.GetResource("PDFInvoice.OrderDate", lang.Id), _dateTimeHelper.ConvertToUserTime(order.CreatedOnUtc, DateTimeKind.Utc).ToString("D", new CultureInfo(lang.LanguageCulture))), font));
                cellHeader.Phrase.Add(new Phrase(Environment.NewLine));
                cellHeader.Phrase.Add(new Phrase(Environment.NewLine));
                cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
                cellHeader.Border = Rectangle.NO_BORDER;

                headerTable.AddCell(cellHeader);

                if (logoExists)
                    //if (lang.Rtl)
                    //    headerTable.SetWidths(new[] { 0.2f, 0.8f });
                    //else
                        headerTable.SetWidths(new[] { 0.8f, 0.2f });
                headerTable.WidthPercentage = 100f;

                //logo               
                if (logoExists)
                {
                    var logoFilePath = _pictureService.GetThumbLocalPath(logoPicture, 0, false);
                    var logo = Image.GetInstance(logoFilePath);
                    logo.Alignment = Element.ALIGN_LEFT;
                    logo.ScaleToFit(65f, 65f);

                    var cellLogo = new PdfPCell();
                    cellLogo.Border = Rectangle.NO_BORDER;
                    cellLogo.AddElement(logo);
                    headerTable.AddCell(cellLogo);
                }
                doc.Add(headerTable);

                #endregion

                #region write form to pdf

                foreach (var field in form.SubmissionFields)
                {
                    if (!field.FieldType.StartsWith("Display"))
                    {
                        var titlecell = new PdfPCell(new Phrase(field.FieldName, titleFont));
                        titlecell.Border = Rectangle.NO_BORDER;
                        formTable.AddCell(titlecell);
                        var valuecell = new PdfPCell(new Phrase(field.FieldValue, font));
                        valuecell.Border = Rectangle.NO_BORDER;
                        formTable.AddCell(valuecell);
                    }
                    else
                    {
                        var cell = new PdfPCell(new Phrase(field.FieldName, titleFont));
                        cell.Colspan = 2;
                        cell.Border = Rectangle.NO_BORDER; 
                        formTable.AddCell(cell);
                    }
                }

                #endregion
                doc.Add(formTable);
                doc.Close();
            }
            return filePath;
        }
    }
}

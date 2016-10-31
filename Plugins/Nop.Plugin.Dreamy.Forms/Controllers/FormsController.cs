using Nop.Core.Data;
using Nop.Plugin.Dreamy.Forms.Domain;
using Nop.Plugin.Dreamy.Forms.Models;
using Nop.Plugin.Dreamy.Forms.Services;
using Nop.Services.Localization;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Nop.Plugin.Dreamy.Forms.Extensions;
using Nop.Web.Framework.Mvc;
using Nop.Services.Authentication;
using Nop.Services.Messages;
using Nop.Core.Domain.Messages;
using System.Web;
using System.IO;
using Nop.Core;

namespace Nop.Plugin.Dreamy.Forms.Controllers
{
    public class FormsController : BasePluginController
    {
        private readonly IFormService _formService;
        private readonly IPrintFormService _printFormService;
        private readonly ILocalizationService _localizationService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IEmailSender _emailSender;
        private readonly EmailAccountSettings _emailAccountSettings;
        private readonly IEmailAccountService _emailAccountService;
        private readonly IStoreContext _storeContext;

        public FormsController(IFormService formService, ILocalizationService localizationService, IAuthenticationService authenticationService, IEmailSender emailSender,
            EmailAccountSettings emailAccountSettings, IEmailAccountService emailAccountService, IPrintFormService printFormService, IStoreContext storeContext)
        {
            _formService = formService;
            _localizationService = localizationService;
            _authenticationService = authenticationService;
            _emailSender = emailSender;
            _emailAccountSettings = emailAccountSettings;
            _emailAccountService = emailAccountService;
            _printFormService = printFormService;
            _storeContext = storeContext;
        }
        public ActionResult Manage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult List(DataSourceRequest command, FormListModel model)
        {
            var forms = _formService.GetAllForms(model.SearchFormName,
                command.Page - 1, command.PageSize, true);
            var gridModel = new DataSourceResult
            {
                Data = forms.Select(x =>
                {
                    var formModel = x.ToModel();
                    return formModel;
                }),
                Total = forms.TotalCount
            };

            return Json(gridModel);
        }

        #region Create / Edit / Delete Form

        public ActionResult Create()
        {
            var model = new FormsRecord();
            model.Published = true;

            return View(model.ToModel());
        }

        [HttpPost]
        public JsonResult GetHtmlFormsList()
        {
            var FormsList = _formService.GetHtmlFormsList();
            return Json(FormsList,JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(FormModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var form = model.ToEntity();
                _formService.SaveForm(form);

                SuccessNotification(_localizationService.GetResource("Plugins.Dreamy.Forms.Added"));
                return continueEditing ? RedirectToAction("Edit", new { id = form.Id }) : RedirectToAction("Manage");
            }

            return View(model);
        }


        public ActionResult Edit(int id)
        {

            var form = _formService.GetFormById(id);
            if (form == null)
                return RedirectToAction("Manage");

            return View(form.ToModel());
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(FormModel model, bool continueEditing)
        {
            var form = _formService.GetFormById(model.Id);
            if (form == null)
                //No form found with the specified id
                return RedirectToAction("Manage");

            if (ModelState.IsValid)
            {
                form.AdminEmails = model.AdminEmails;
                form.FormName = model.FormName;
                form.Published = model.Published;

                _formService.UpdateForm(form);
                //search engine name
                //model.SeName = manufacturer.ValidateSeName(model.SeName, manufacturer.Name, true);
                //_urlRecordService.SaveSlug(manufacturer, model.SeName, 0);
                //locales
                //UpdateLocales(manufacturer, model);
                //ACL
                //SaveManufacturerAcl(manufacturer, model);
                //Stores
                //SaveStoreMappings(manufacturer, model);

                //activity log
                //_customerActivityService.InsertActivity("EditManufacturer", _localizationService.GetResource("ActivityLog.EditManufacturer"), manufacturer.Name);

                SuccessNotification(_localizationService.GetResource("Plugins.Dreamy.Forms.Updated"));

                if (continueEditing)
                {
                    //selected tab
                    //SaveSelectedTabIndex();

                    return RedirectToAction("Edit", new { id = form.Id });
                }
                return RedirectToAction("Manage");
            }


            //If we got this far, something failed, redisplay form
            //templates
            //PrepareTemplatesModel(model);
            //discounts
            //PrepareDiscountModel(model, manufacturer, true);
            //ACL
            //PrepareAclModel(model, manufacturer, true);
            //Stores
            //PrepareStoresMappingModel(model, manufacturer, true);

            return View(model);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            var form = _formService.GetFormById(id);
            if (form == null)
                //No manufacturer found with the specified id
                return RedirectToAction("Manage");

            _formService.DeleteForm(form);

            SuccessNotification(_localizationService.GetResource("Plugins.Dreamy.Forms.Deleted"));
            return RedirectToAction("Manage");
        }
        #endregion

        #region Form Fields
        [HttpPost]
        public ActionResult FieldList(DataSourceRequest command,object model, int formId)
        {
            var formfields = _formService.GetFormFields(formId,
                command.Page - 1, command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = formfields.Select(x =>
                {
                    var formfieldsModel = x.ToModel();
                    return formfieldsModel;
                }),
                Total = formfields.TotalCount
            };

            return Json(gridModel);

        }
        [HttpPost]
        public ActionResult FieldInsert(int fieldformId, FormFieldsModel field)
        {
            if (field != null)
            {
                field.FormId = fieldformId;
                if (field.FieldAllowedValues == null)
                {
                    field.FieldAllowedValues = "";
                }
                _formService.SaveFormField(field.ToEntity());
            }

            return new NullJsonResult();
        }
        [HttpPost]
        public ActionResult FieldUpdate(FormFieldsModel model)
        {
            var field = _formService.GetFieldById(model.Id);
            if (field == null)
                //No form found with the specified id
                return RedirectToAction("Manage");
            
            field.DisplayOrder = model.DisplayOrder;
            field.FieldAllowedValues = model.FieldAllowedValues;
            field.FieldName = model.FieldName;
            field.FieldType = model.FieldType;
            field.FormId = model.FormId;
            field.IsRequired = model.IsRequired;
            field.IsForAdminOnly = model.IsForAdminOnly;
            _formService.UpdateFormField(field);            

            return new NullJsonResult();
        }
        [HttpPost]
        public ActionResult FieldDelete(FormFieldsModel model)
        {
            var field = _formService.GetFieldById(model.Id);
            if (field == null)
                //No form found with the specified id
                return RedirectToAction("Manage");

            _formService.DeleteFormField(field);
            return new NullJsonResult();
        }
        #endregion

        [HttpPost]
        public JsonResult FormSubmit()
        {
            try
            {
                var formidstr = Request["formid"];
                int formid;
                if (formidstr != null && formidstr != "" && Int32.TryParse(formidstr, out formid))
                {
                    var form = _formService.GetFormById(formid);
                    var currentCustomer = _authenticationService.GetAuthenticatedCustomer();

                    string emailbody = "";
                    emailbody += "<b>A new " + form.FormName + " submitted with the below details:</b><hr/>";

                    //add new form submission
                    var formsubmission = new FormSubmissionsRecord();
                    formsubmission.FormId = formid;
                    if (currentCustomer != null)
                    {
                        formsubmission.CustomerId = currentCustomer.Id;
                        formsubmission.SenderEmail = currentCustomer.Email;
                    }
                    formsubmission.SubmitDate = DateTime.Now;
                    int submissionid = _formService.SaveFormSubmission(formsubmission);
                    //get form fields
                    var formfields = _formService.GetFormFieldsNoPaging(formid).ToList();
                    //get fields values
                    //add new submission value for each field
                    foreach (var field in formfields)
                    {
                        string fieldvalue = "";
                        if (!field.IsForAdminOnly)
                        {
                            fieldvalue = Request["field" + field.Id];
                        }
                        FormSubmissionsValuesRecord valuerecord = new FormSubmissionsValuesRecord();
                        valuerecord.FieldValue = fieldvalue;
                        valuerecord.FormFieldId = field.Id;
                        valuerecord.SubmissionId = submissionid;
                        _formService.SaveFieldValue(valuerecord);
                        if (!field.FieldType.Contains("Display"))
                        {
                            emailbody += "<b>" + field.FieldName + ":</b>" + fieldvalue + "<br/>";
                        }
                    }
                    emailbody += "<br/>click <a href='" + _storeContext.CurrentStore.Url + "Admin/Forms/SubmissionPreview/" + submissionid.ToString() + "'>here</a> to view submitted form";
                    //send email to form admin that a new form was submitted
                    var emailAccount = _emailAccountService.GetEmailAccountById(_emailAccountSettings.DefaultEmailAccountId);
                    var toemails = form.AdminEmails.Split(',');
                    foreach (var toemail in toemails)
                    {
                        try
                        {
                            _emailSender.SendEmail(emailAccount, "New " + form.FormName + " submitted.", emailbody, (currentCustomer != null ? currentCustomer.Email : emailAccount.Email), (currentCustomer != null ? currentCustomer.Username : emailAccount.DisplayName), toemail, toemail);
                        }
                        catch
                        {

                        }
                    }
                    return Json(form.FormName + " successfully submitted.", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

            }
            return Json("Error occured! please try again later or contact administration.",JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SubmissionsList(DataSourceRequest command, FormListModel model)
        {
            var submissions = _formService.GetFormSubmissionsList(command.Page - 1, command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = submissions.ToList(),
                Total = submissions.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult Submissions()
        {
            return View();
        }

        public ActionResult SubmissionPreview(int SubmissionId)
        {
            var s = _formService.GetSubmissionForPreview(SubmissionId);
            return View(s);
        }

        [HttpPost, ParameterBasedOnFormName("save-print", "print")]
        public ActionResult SubmissionUpdate(SubmissionModel model, bool print)
        {
            int submissionid = Int32.Parse(Request["submissionid"]);
            int formid = Int32.Parse(Request["formid"]);
            //get form fields (isforadminonly == true)
            var formfields = _formService.GetFormFieldsNoPaging(formid).Where(f => f.IsForAdminOnly == true).ToList();
            //get fields values from request
            foreach (var field in formfields)
            {
                string fieldvalue = Request["field" + field.Id];

                int valueid = Int32.Parse(Request["valueid_" + field.Id]);
                if (valueid > 0)
                {
                    //update field value
                    var submissionvalue = _formService.GetSubmissionFieldValue(submissionid, field.Id);
                    submissionvalue.FieldValue = fieldvalue;
                    _formService.UpdateFieldValue(submissionvalue);
                }
                else
                {
                    //insert field value
                    var submissionvalue = new FormSubmissionsValuesRecord();
                    submissionvalue.FieldValue = fieldvalue;
                    submissionvalue.FormFieldId = field.Id;
                    submissionvalue.SubmissionId = submissionid;
                    _formService.SaveFieldValue(submissionvalue);
                }
            }

            if (print)
            {
                var filepath = _printFormService.CreateFormPdf(submissionid);
                var filename = Path.GetFileName(filepath);
                byte[] filedata = System.IO.File.ReadAllBytes(filepath);
                string contentType = MimeMapping.GetMimeMapping(filepath);

                var cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = filename,
                    Inline = true,
                };

                Response.Headers.Add("Content-Disposition", cd.ToString());

                return File(filedata, contentType);
            }

            return View("Submissions");
        }
    }
}

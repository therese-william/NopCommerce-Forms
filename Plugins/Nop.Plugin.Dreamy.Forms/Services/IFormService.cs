using Nop.Core;
using Nop.Plugin.Dreamy.Forms.Domain;
using Nop.Plugin.Dreamy.Forms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Dreamy.Forms.Services
{
    public interface IFormService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Form"></param>
        void SaveForm(FormsRecord Form);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Form"></param>
        void UpdateForm(FormsRecord Form);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Form"></param>
        void DeleteForm(FormsRecord Form);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FormId"></param>
        /// <returns></returns>
        FormsRecord GetFormById(int FormId);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IPagedList<FormsRecord> GetAllForms(string formName = "",
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            bool showHidden = false);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Field"></param>
        void SaveFormField(FormFieldsRecord Field);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Field"></param>
        void UpdateFormField(FormFieldsRecord Field);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Field"></param>
        void DeleteFormField(FormFieldsRecord Field);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FormId"></param>
        /// <returns></returns>
        IPagedList<FormFieldsRecord> GetFormFields(int FormId, int pageIndex, int pageSize);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Submission"></param>
        int SaveFormSubmission(FormSubmissionsRecord Submission);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Submission"></param>
        void UpdateFormSubmission(FormSubmissionsRecord Submission);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Submission"></param>
        void DeleteFormSubmission(FormSubmissionsRecord Submission);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<FormSubmissionsRecord> GetAllFormsSubmissions();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FormId"></param>
        /// <returns></returns>
        IList<FormSubmissionsRecord> GetSubmissionsByFormId(int FormId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SubmissionId"></param>
        /// <returns></returns>
        FormSubmissionsRecord GetFormSubmission(int SubmissionId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value"></param>
        void SaveFieldValue(FormSubmissionsValuesRecord Value);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value"></param>
        void UpdateFieldValue(FormSubmissionsValuesRecord Value);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value"></param>
        void DeleteFieldValue(FormSubmissionsValuesRecord Value);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SubmissionId"></param>
        /// <param name="FieldId"></param>
        /// <returns></returns>
        FormSubmissionsValuesRecord GetSubmissionFieldValue(int SubmissionId, int FieldId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SubmissionId"></param>
        /// <returns></returns>
        IList<FormSubmissionsValuesRecord> GetSubmissionValues(int SubmissionId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        FormFieldsRecord GetFieldById(int Id);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<SelectListItem> GetHtmlFormsList();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FormId"></param>
        /// <returns></returns>
        List<FormFieldsRecord> GetFormFieldsNoPaging(int FormId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SubmissionId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedList<SubmissionListModel> GetFormSubmissionsList(int pageIndex = 0, int pageSize = int.MaxValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SubmissionId"></param>
        /// <returns></returns>
        SubmissionModel GetSubmissionForPreview(int SubmissionId);
    }
}

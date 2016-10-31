using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Customers;
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
    public class FormService : IFormService
    {
        private readonly IRepository<FormsRecord> _formRepository;
        private readonly IRepository<FormFieldsRecord> _formFieldsRepository;
        private readonly IRepository<FormSubmissionsRecord> _formSubmissionsRepository;
        private readonly IRepository<FormSubmissionsValuesRecord> _formSubmissionValueRepository;
        private readonly IRepository<Customer> _customerRepository;


        public FormService( IRepository<FormsRecord> formRepository,
                            IRepository<FormFieldsRecord> formFieldsRepository,
                            IRepository<FormSubmissionsRecord> formSubmissionsRepository,
                            IRepository<FormSubmissionsValuesRecord> formSubmissionValueRepository,
                            IRepository<Customer> customerRepository)
        {
            _formRepository = formRepository;
            _formFieldsRepository = formFieldsRepository;
            _formSubmissionsRepository = formSubmissionsRepository;
            _formSubmissionValueRepository = formSubmissionValueRepository;
            _customerRepository = customerRepository;
        }
        public void SaveForm(Domain.FormsRecord Form)
        {
            _formRepository.Insert(Form);
        }

        public void UpdateForm(Domain.FormsRecord Form)
        {
            _formRepository.Update(Form);
        }

        public void DeleteForm(Domain.FormsRecord Form)
        {
            _formRepository.Delete(Form);
        }

        public Domain.FormsRecord GetFormById(int FormId)
        {
            return(_formRepository.GetById(FormId));
        }

        public FormFieldsRecord GetFieldById(int Id)
        {
            return (_formFieldsRepository.GetById(Id));
        }

        public IPagedList<FormsRecord> GetAllForms(string formName = "",
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            bool showHidden = false)
        {
            try
            {
                var query = _formRepository.Table;
                if (!showHidden)
                {
                    query = query.Where(f => f.Published);
                }
                if (!String.IsNullOrWhiteSpace(formName))
                {
                    query = query.Where(f => f.FormName.Contains(formName));
                }
                return new PagedList<FormsRecord>(query.ToList(), pageIndex, pageSize);
            }
            catch
            {
                return (new PagedList<FormsRecord>(new List<FormsRecord>(),pageIndex,pageSize));
            }
        }

        public List<SelectListItem> GetHtmlFormsList()
        {
            try
            {
                var result = new List<SelectListItem>();
                var query = _formRepository.Table;
                query = query.Where(f => f.Published);
                var forms = query.ToList();
                foreach (var form in forms)
                {
                    var formfields = GetFormFieldsNoPaging(form.Id);
                    formfields = formfields.Where(f => f.IsForAdminOnly == false).ToList();
                    if (formfields.Count > 0)
                    {
                        SelectListItem formitem = new SelectListItem();
                        formitem.Text = form.FormName;

                        formitem.Value = "";
                        formitem.Value += "<div style='width:100%;'><div id='ajaxBusy' style='display:none;left:-50%;position:fixed;top:50%;width:100%;z-index:100000;'>"+
                                        "<span style='background:url(/administration/content/images/ajax-loading.gif) no-repeat;width:40px;height:40px;float:right;margin:9px 9px 0px 0px;'>&nbsp;</span></div>" +
                                        "<script type='text/javascript'>" +
                                            "$( document ).ready(function() { $('#form" + form.Id + "')[0].reset();});" +
                                                "function submitform(){" +
                                                    "$('#form" + form.Id + "').validate();" +
                                                    "if($('#form" + form.Id + "').valid()){ $('#ajaxBusy').show();" +
                                                    "var actionurl = $('#form" + form.Id + "')[0].action;" +
                                                    "$.ajax({" +
                                                            "url: actionurl," +
                                                            "type: 'post'," +
                                                            "dataType: 'json'," +
                                                            "data: $('#form" + form.Id + "').serialize()," +
                                                            "success: function(data) { $('#ajaxBusy').hide();" +
                                                                "alert(data); $('#form" + form.Id + "')[0].reset();" +
                                                            "}" +
                                                    "});}" +
                                            "}" +
                                            "</script>" +
                            "<form id='form" + form.Id + "' action='/Forms/FormSubmit' method='post'>";
                        formitem.Value += "<input type='hidden' id='formid' name='formid' value='"+form.Id+"' />";

                        foreach (var field in formfields)
                        {
                            formitem.Value += "<div class='form-group row'>";
                            if (field.FieldType == "Display Text" || field.FieldType == "Display Title")
                            {

                                formitem.Value += "<div class='col-md-12 col-xs-12'><span";
                                if (field.FieldType == "Display Title") 
                                {
                                    formitem.Value += " style='font-size:1.5em; font-weight:700;' ";
                                }
                                formitem.Value += ">";
                                formitem.Value += field.FieldName;
                                formitem.Value += "</span></div>";
                                formitem.Value += "<div class='col-md-12 col-xs-12'>";                                
                            }
                            else
                            {
                                formitem.Value += "<div class='col-md-3 col-xs-12'><label class='control-label' for='field" + field.Id + "'>";
                                formitem.Value += field.FieldName;
                                if (field.IsRequired)
                                {
                                    formitem.Value += "(*)";
                                }
                                formitem.Value += ":</label></div>";
                                formitem.Value += "<div class='col-md-9 col-xs-12'>";
                            }
                            switch (field.FieldType)
                            {
                                case "Text":
                                    formitem.Value += "<input class='form-control' type='text' id='field" + field.Id + "' name='field" + field.Id + "'";
                                    if (field.IsRequired)
                                    {
                                        formitem.Value += " data-val='true' data-val-required='Please enter "+field.FieldName+"' ";
                                    }
                                    formitem.Value += " />";
                                    break;
                                case "Email":
                                    formitem.Value += "<input class='email form-control' data-val-email='Wrong email' type='text' id='field" + field.Id + "' name='field" + field.Id + "'";
                                    if (field.IsRequired)
                                    {
                                        formitem.Value += " data-val='true' data-val-required='Please enter " + field.FieldName + "' ";
                                    }
                                    formitem.Value += " />";
                                    break;
                                case "Date":
                                    formitem.Value += "<input class='form-control' type='date' id='field" + field.Id + "' name='field" + field.Id + "' ";
                                    if (field.IsRequired)
                                    {
                                        formitem.Value += " data-val='true' data-val-required='Please enter " + field.FieldName + "' ";
                                    }
                                    formitem.Value += " />";
                                    break;
                                case "Number":
                                    formitem.Value += "<input class='form-control' type='number' id='field" + field.Id + "' name='field" + field.Id + "' ";
                                    if (field.IsRequired)
                                    {
                                        formitem.Value += " data-val='true' data-val-required='Please enter " + field.FieldName + "' ";
                                    }
                                    formitem.Value += " />";
                                    break;
                                case "List":
                                    formitem.Value += "<select class='form-control' id='field" + field.Id + "' name='field" + field.Id + "' placeholder='Choose " + field.FieldName + " ...'>";
                                    if (field.FieldAllowedValues != null)
                                    {
                                        var fieldvalues = field.FieldAllowedValues.Split(',');
                                        foreach (var fieldvalue in fieldvalues)
                                        {
                                            formitem.Value += "<option value='"+fieldvalue+"'>"+fieldvalue+"</option>";
                                        }
                                    }
                                    formitem.Value += "</select>";
                                    break;
                                case "Multi line text":
                                    formitem.Value += "<textarea class='form-control' rows='3' cols='20' id='field" + field.Id + "' name='field" + field.Id + "' ";
                                    if (field.IsRequired)
                                    {
                                        formitem.Value += " data-val='true' data-val-required='Please enter "+field.FieldName+"' ";
                                    }
                                    formitem.Value += " ></textarea>";
                                    break;
                            }
                            if ((field.IsRequired || field.FieldType == "Email" ) && field.FieldType != "List")
                            {
                                formitem.Value += "<span class='field-validation-valid' data-valmsg-for='field" + field.Id + "' data-valmsg-replace='true' style='color:red;'>&nbsp;</span>";
                            }

                            formitem.Value += "</div>";
                            formitem.Value += "</div>";
                        }
                        formitem.Value += "<div class='buttons'><input class='button-1 btn btn-default' type='button' value='Submit' onclick='submitform()'/></div>";
                        formitem.Value += "</form>";
                        formitem.Value += "</div>";
                        result.Add(formitem);
                    }
                }
                return result;
            }
            catch
            {
                return (new List<SelectListItem>());
            }

        }

        public void SaveFormField(FormFieldsRecord Field)
        {
            try
            {
                _formFieldsRepository.Insert(Field);
            }
            catch (Exception ex) { }
        }

        public void UpdateFormField(Domain.FormFieldsRecord Field)
        {
            _formFieldsRepository.Update(Field);
        }

        public void DeleteFormField(Domain.FormFieldsRecord Field)
        {
            _formFieldsRepository.Delete(Field);
        }

        public IPagedList<Domain.FormFieldsRecord> GetFormFields(int FormId,
            int pageIndex = 0,
            int pageSize = int.MaxValue)
        {
            try
            {
                var query = _formFieldsRepository.Table;
                if (FormId != 0)
                {
                    query = query.Where(f => f.FormId == FormId).OrderBy(f=>f.DisplayOrder);
                }
                return new PagedList<FormFieldsRecord>(query.ToList(), pageIndex, pageSize);
            }
            catch
            {
                return new PagedList<FormFieldsRecord>(new List<FormFieldsRecord>(),pageIndex,pageSize);
            }
        }

        public List<FormFieldsRecord> GetFormFieldsNoPaging(int FormId)
        {
            try
            {
                var query = _formFieldsRepository.Table;
                if (FormId != 0)
                {
                    query = query.Where(f => f.FormId == FormId);
                }
                query = query.OrderBy(f => f.DisplayOrder);
                return query.ToList();
            }
            catch
            {
                return new List<FormFieldsRecord>();
            }
        }

        public int SaveFormSubmission(FormSubmissionsRecord Submission)
        {
            _formSubmissionsRepository.Insert(Submission);
            return Submission.Id;
        }

        public void UpdateFormSubmission(FormSubmissionsRecord Submission)
        {
            _formSubmissionsRepository.Update(Submission);
        }

        public void DeleteFormSubmission(Domain.FormSubmissionsRecord Submission)
        {
            _formSubmissionsRepository.Delete(Submission);
        }

        public IList<Domain.FormSubmissionsRecord> GetAllFormsSubmissions()
        {
            try
            {
                return (_formSubmissionsRepository.Table.ToList());
            }
            catch
            {
                return new List<FormSubmissionsRecord>();
            }
        }

        public IList<Domain.FormSubmissionsRecord> GetSubmissionsByFormId(int FormId)
        {
            try
            {
                return (_formSubmissionsRepository.Table.Where(s => s.FormId == FormId).ToList());
            }
            catch
            {
                return new List<FormSubmissionsRecord>();
            }
        }

        public void SaveFieldValue(Domain.FormSubmissionsValuesRecord Value)
        {
            _formSubmissionValueRepository.Insert(Value);
        }

        public void UpdateFieldValue(Domain.FormSubmissionsValuesRecord Value)
        {
            _formSubmissionValueRepository.Update(Value);
        }

        public void DeleteFieldValue(Domain.FormSubmissionsValuesRecord Value)
        {
            _formSubmissionValueRepository.Delete(Value);
        }

        public Domain.FormSubmissionsValuesRecord GetSubmissionFieldValue(int SubmissionId, int FieldId)
        {
            try
            {
                return (_formSubmissionValueRepository.Table.Where(v => v.SubmissionId == SubmissionId && v.FormFieldId == FieldId).ToList().FirstOrDefault());
            }
            catch
            {
                return null;
            }
        }

        public IList<Domain.FormSubmissionsValuesRecord> GetSubmissionValues(int SubmissionId)
        {
            try
            {
                return(_formSubmissionValueRepository.Table.Where(v => v.SubmissionId == SubmissionId).ToList());
            }
            catch
            {
                return new List<FormSubmissionsValuesRecord>();
            }
        }

        public IPagedList<SubmissionListModel> GetFormSubmissionsList(int pageIndex = 0,
            int pageSize = int.MaxValue)
        {
            try
            {
                var squery = from sub in _formSubmissionsRepository.Table
                            join f in _formRepository.Table on sub.FormId equals f.Id
                            //join c in _customerRepository.Table on sub.CustomerId equals c.Id into t_c
                            //from c in t_c.DefaultIfEmpty()
                            select new 
                            {
                                subId = sub.Id,
                                formId = f.Id,
                                FormName = f.FormName,
                                SubmitDate = sub.SubmitDate,
                                CustomerId = sub.CustomerId
                            };
                var squerylist = squery.ToList();
                var cquery = from c in _customerRepository.Table
                             select new
                             {
                                 c.Id,
                                 c.Username
                             };
                var cquerylist = cquery.ToList();
                var query = from s in squerylist
                            join c in cquerylist on s.CustomerId equals c.Id into t_c
                            from c in t_c.DefaultIfEmpty()
                            select new SubmissionListModel()
                            {
                                SubmissionId = s.subId,
                                SubmitDate = s.SubmitDate,
                                CustomerId = c != null ? c.Id : 0,
                                CustomerName = c != null ? c.Username : "",
                                FormId = s.formId,
                                FormName = s.FormName
                            };
                return new PagedList<SubmissionListModel>(query.ToList(), pageIndex, pageSize);
            }
            catch(Exception ex)
            {
                return new PagedList<SubmissionListModel>(new List<SubmissionListModel>(), pageIndex, pageSize);
            }
        }

        public FormSubmissionsRecord GetFormSubmission(int SubmissionId)
        {
            try
            {
                return( _formSubmissionsRepository.Table.Where(s => s.Id == SubmissionId).ToList().FirstOrDefault() );
            }
            catch
            {
                return null;
            }
        }

        public SubmissionModel GetSubmissionForPreview(int SubmissionId)
        {
            SubmissionModel model = new SubmissionModel();
            model.SubmissionFields = new List<SubmissionFields>();
            var squery = from sub in _formSubmissionsRepository.Table
                         join f in _formRepository.Table on sub.FormId equals f.Id
                         where sub.Id == SubmissionId
                         select new
                         {
                             subId = sub.Id,
                             formId = f.Id,
                             FormName = f.FormName,
                             SubmitDate = sub.SubmitDate
                         };
            var submission = squery.ToList().FirstOrDefault();
            model.FormId = submission.formId;
            model.FormName = submission.FormName;
            model.SubmissionId = SubmissionId;

            var vquery = from v in _formSubmissionValueRepository.Table 
                         where (v.SubmissionId == SubmissionId )
                         select new{
                             ValueId = v.Id,
                             FieldValue=v.FieldValue,
                             FieldId = v.FormFieldId
                         };
            var vquerylist = vquery.ToList();
            var fquery = from f in _formFieldsRepository.Table
                         where f.FormId == model.FormId
                         select new { 
                             FieldId = f.Id,
                             FieldName = f.FieldName,
                             FieldType = f.FieldType,
                             FieldAllowedValues = f.FieldAllowedValues,
                             IsRequired = f.IsRequired,
                             IsForAdminOnly = f.IsForAdminOnly,
                             DisplayOrder = f.DisplayOrder
                         };
            var fquerylist = fquery.ToList();

            var query = from f in fquerylist
                        join v in vquerylist on f.FieldId equals v.FieldId into t_v
                        from v in t_v.DefaultIfEmpty()
                        select new SubmissionFields()
                        {
                            FormId = model.FormId,
                            FieldName = f.FieldName,
                            FieldType = f.FieldType,
                            FieldAllowedValues = f.FieldAllowedValues,
                            IsRequired = f.IsRequired,
                            IsForAdminOnly = f.IsForAdminOnly,
                            FieldValue = v != null ? v.FieldValue : "",
                            FieldValueId = v != null ? v.ValueId : 0,
                            DisplayOrder = f.DisplayOrder,
                            Id = f.FieldId
                        };

            model.SubmissionFields = query.OrderBy(s => s.DisplayOrder).ToList();
            return model;
        }
    }
}

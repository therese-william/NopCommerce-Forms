using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Dreamy.Forms.Domain
{
    public class FormFieldsRecord : BaseEntity
    {
        private ICollection<FormSubmissionsValuesRecord> _submissionValues;
        public int FormId { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public string FieldAllowedValues { get; set; }
        public bool IsRequired { get; set; }
        public bool IsForAdminOnly { get; set; }
        public int DisplayOrder { get; set; }
        public virtual FormsRecord Form { get; set; }
        public virtual ICollection<FormSubmissionsValuesRecord> SubmissionValues
        {
            get { return _submissionValues ?? (_submissionValues = new List<FormSubmissionsValuesRecord>()); }
            protected set { _submissionValues = value; }
        }
    }
}

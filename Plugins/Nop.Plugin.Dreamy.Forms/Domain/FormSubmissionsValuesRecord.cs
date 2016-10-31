using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Dreamy.Forms.Domain
{
    public class FormSubmissionsValuesRecord : BaseEntity
    {
        public int SubmissionId { get; set; }
        public int FormFieldId { get; set; }
        public string FieldValue { get; set; }
        public virtual FormSubmissionsRecord FormSubmission { get; set; }
        public virtual FormFieldsRecord FormField { get; set; }
        
    }
}

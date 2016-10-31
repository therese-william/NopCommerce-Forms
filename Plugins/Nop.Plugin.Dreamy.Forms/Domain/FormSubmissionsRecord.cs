using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Dreamy.Forms.Domain
{
    public class FormSubmissionsRecord : BaseEntity
    {

        private ICollection<FormSubmissionsValuesRecord> _submissionValues;
        public DateTime SubmitDate { get; set; }
        public int FormId { get; set; }
        public int CustomerId { get; set; }
        public string SenderEmail { get; set; }
        public virtual FormsRecord Form { get; set; }
        public virtual ICollection<FormSubmissionsValuesRecord> SubmissionValues
        {
            get { return _submissionValues ?? (_submissionValues = new List<FormSubmissionsValuesRecord>()); }
            protected set { _submissionValues = value; }
        }
    }
}

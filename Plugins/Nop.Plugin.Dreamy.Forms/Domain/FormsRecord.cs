using Nop.Core;
//using Nop.Web.Framework;
//using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Dreamy.Forms.Domain
{
    public class FormsRecord : BaseEntity
    {
        private ICollection<FormFieldsRecord> _fields;
        private ICollection<FormSubmissionsRecord> _submissions;
        public string FormName { get; set; }
        public string AdminEmails { get; set; }
        public bool Published { get; set; }
        public virtual ICollection<FormFieldsRecord> Fields
        {
            get { return _fields ?? (_fields = new List<FormFieldsRecord>()); }
            protected set { _fields = value; }
        }
        public virtual ICollection<FormSubmissionsRecord> Submissions
        {
            get { return _submissions ?? (_submissions = new List<FormSubmissionsRecord>()); }
            protected set { _submissions = value; }
        }

    }
}

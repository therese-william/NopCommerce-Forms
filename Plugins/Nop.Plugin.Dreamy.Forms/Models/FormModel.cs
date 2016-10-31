using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Dreamy.Forms.Models
{
    public class FormModel : BaseNopEntityModel
    {
        public FormModel()
        {
        }
        [NopResourceDisplayName("Plugins.Dreamy.Forms.Fields.FormName")]
        public string FormName { get; set; }
        [NopResourceDisplayName("Plugins.Dreamy.Forms.Fields.AdminEmails")]
        public string AdminEmails { get; set; }
        [NopResourceDisplayName("Plugins.Dreamy.Forms.Fields.Published")]
        public bool Published { get; set; }
    }
}

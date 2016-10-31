using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Dreamy.Forms.Models
{
    public class FormFieldsModel : BaseNopEntityModel
    {
        public int FormId { get; set; }
        [NopResourceDisplayName("Plugins.Dreamy.FormFields.Fields.FieldName")]
        public string FieldName { get; set; }
        [NopResourceDisplayName("Plugins.Dreamy.FormFields.Fields.FieldType")]
        public string FieldType { get; set; }
        [NopResourceDisplayName("Plugins.Dreamy.FormFields.Fields.FieldAllowedValues")]
        public string FieldAllowedValues { get; set; }
        [NopResourceDisplayName("Plugins.Dreamy.FormFields.Fields.IsRequired")]
        public bool IsRequired { get; set; }
        [NopResourceDisplayName("Plugins.Dreamy.FormFields.Fields.IsForAdminOnly")]
        public bool IsForAdminOnly { get; set; }
        [NopResourceDisplayName("Plugins.Dreamy.FormFields.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }
    }
}

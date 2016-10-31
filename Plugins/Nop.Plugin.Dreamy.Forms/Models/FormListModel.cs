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
    public class FormListModel : BaseNopModel
    {
        [NopResourceDisplayName("Plugins.Dreamy.Forms.List.SearchFormName")]
        [AllowHtml]
        public string SearchFormName { get; set; }

    }
}

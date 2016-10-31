using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Dreamy.Forms
{
    class AdminAreaRegisteration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_forms",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Forms", action = "Manage", area = "Admin", id = "" },
                new[] { "Nop.Plugin.Dreamy.Forms.Controllers" }
            );
        }
    
    }
}

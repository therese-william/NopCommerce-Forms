using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Dreamy.Menu
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
                "Admin_menu",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Topic", action = "Index", area = "Admin", id = "" },
                new[] { "Nop.Plugin.Dreamy.Menu.Controllers" }
            );
        }

    }
}

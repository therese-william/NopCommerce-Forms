using Nop.Plugin.Dreamy.Menu.Infrastructure;
using Nop.Web.Framework.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Dreamy.Menu
{
    public class RouteConfig : IRouteProvider
    {

        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            ViewEngines.Engines.Insert(0, new CustomViewEngine());

            var route = routes.MapRoute("Plugin.Dreamy.Menu.Topic", "Admin/Topic/List",
                new { controller = "Topic", action = "List" }, new[] { "Nop.Plugin.Dreamy.Menu.Controllers" });
            routes.Remove(route);
            routes.Insert(0, route);
        }

        public int Priority
        {
            get { return 0; }
        }

    }
}

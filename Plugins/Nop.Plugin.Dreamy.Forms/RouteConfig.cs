using Nop.Plugin.Dreamy.Forms.Infrastructure;
using Nop.Web.Framework.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nop.Plugin.Dreamy.Forms
{
    public class RouteConfig : IRouteProvider
    {

        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            ViewEngines.Engines.Insert(0, new CustomViewEngine());
            ViewEngines.Engines.Insert(1, new FormViewEngine());

            var route = routes.MapRoute("Plugin.Dreamy.Forms.Manage", "Admin/Forms/Manage",
                new { controller = "Forms", action = "Manage" }, new[] { "Nop.Plugin.Dreamy.Forms.Controllers" });
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Dreamy.Forms.List", "Admin/Forms/List",
                new { controller = "Forms", action = "List" }, new[] { "Nop.Plugin.Dreamy.Forms.Controllers" });
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Dreamy.Forms.Create", "Admin/Forms/Create",
                new { controller = "Forms", action = "Create" }, new[] { "Nop.Plugin.Dreamy.Forms.Controllers" });
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Dreamy.Forms.Edit", "Admin/Forms/Edit/{id}",
                new { controller = "Forms", action = "Edit", id = UrlParameter.Optional }, new[] { "Nop.Plugin.Dreamy.Forms.Controllers" });
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Dreamy.Forms.Delete", "Admin/Forms/Delete/{id}",
                new { controller = "Forms", action = "Delete", id = UrlParameter.Optional }, new[] { "Nop.Plugin.Dreamy.Forms.Controllers" });
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Dreamy.Forms.FieldList", "Admin/Forms/FieldList/{formId}",
                new { controller = "Forms", action = "FieldList", formId = UrlParameter.Optional }, new[] { "Nop.Plugin.Dreamy.Forms.Controllers" });
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Dreamy.Forms.FieldInsert", "Admin/Forms/FieldInsert/{fieldformId}",
                new { controller = "Forms", action = "FieldInsert", fieldformId = UrlParameter.Optional }, new[] { "Nop.Plugin.Dreamy.Forms.Controllers" });
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Dreamy.Forms.FieldUpdate", "Admin/Forms/FieldUpdate",
                new { controller = "Forms", action = "FieldUpdate" }, new[] { "Nop.Plugin.Dreamy.Forms.Controllers" });
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Dreamy.Forms.FieldDelete", "Admin/Forms/FieldDelete",
                new { controller = "Forms", action = "FieldDelete" }, new[] { "Nop.Plugin.Dreamy.Forms.Controllers" });
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Dreamy.Forms.GetHtmlFormsList", "Admin/Forms/GetHtmlFormsList",
                new { controller = "Forms", action = "GetHtmlFormsList" }, new[] { "Nop.Plugin.Dreamy.Forms.Controllers" });
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Dreamy.Forms.SubmissionsList", "Admin/Forms/SubmissionsList",
                new { controller = "Forms", action = "SubmissionsList" }, new[] { "Nop.Plugin.Dreamy.Forms.Controllers" });
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Dreamy.Forms.Submissions", "Admin/Forms/Submissions",
                new { controller = "Forms", action = "Submissions" }, new[] { "Nop.Plugin.Dreamy.Forms.Controllers" });
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Dreamy.Forms.SubmissionPreview", "Admin/Forms/SubmissionPreview/{SubmissionId}",
                new { controller = "Forms", action = "SubmissionPreview" }, new[] { "Nop.Plugin.Dreamy.Forms.Controllers" });
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Dreamy.Forms.SubmissionUpdate", "Admin/Forms/SubmissionUpdate",
                new { controller = "Forms", action = "SubmissionUpdate" }, new[] { "Nop.Plugin.Dreamy.Forms.Controllers" });
            routes.Remove(route);
            routes.Insert(0, route);
        }

        public int Priority
        {
            get { return 0; }
        }
    }
}

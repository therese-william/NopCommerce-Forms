using Nop.Web.Framework.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Dreamy.Forms.Infrastructure
{
    public class CustomViewEngine : RazorViewEngine
    {
        public CustomViewEngine()
        {
            ViewLocationFormats = new[] { 
                "~/Plugins/Dreamy.Forms/Views/{0}.cshtml", 
                //"~/Plugins/Dreamy.Forms/Views/Forms/{0}.cshtml", 
                "~/Plugins/Dreamy.Forms/Views/Shared/{0}.cshtml", 
                "~/Plugins/Dreamy.Forms/Views/Shared/EditorTemplates/{0}.cshtml" 
            };
            PartialViewLocationFormats = new[] { 
                "~/Plugins/Dreamy.Forms/Views/{0}.cshtml", 
                //"~/Plugins/Dreamy.Forms/Views/Forms/{0}.cshtml", 
                "~/Plugins/Dreamy.Forms/Views/Shared/{0}.cshtml", 
                "~/Plugins/Dreamy.Forms/Views/Shared/EditorTemplates/{0}.cshtml" 
            };
            AreaPartialViewLocationFormats = new[] { 
                "~/Plugins/Dreamy.Forms/Views/{0}.cshtml", 
                //"~/Plugins/Dreamy.Forms/Views/Forms/{0}.cshtml", 
                "~/Plugins/Dreamy.Forms/Views/Shared/{0}.cshtml", 
                "~/Plugins/Dreamy.Forms/Views/Shared/EditorTemplates/{0}.cshtml" 
            };
            AreaViewLocationFormats = new[] { 
                "~/Plugins/Dreamy.Forms/Views/{0}.cshtml", 
                //"~/Plugins/Dreamy.Forms/Views/Forms/{0}.cshtml", 
                "~/Plugins/Dreamy.Forms/Views/Shared/{0}.cshtml", 
                "~/Plugins/Dreamy.Forms/Views/Shared/EditorTemplates/{0}.cshtml" 
            };
        }
    }
}

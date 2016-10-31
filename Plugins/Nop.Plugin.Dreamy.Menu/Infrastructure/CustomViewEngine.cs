using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Dreamy.Menu.Infrastructure
{
    public class CustomViewEngine : RazorViewEngine
    {
        public CustomViewEngine()
        {
            ViewLocationFormats = new[] { 
                "~/Plugins/Dreamy.Menu/Views/{1}/{0}.cshtml"
            };
            PartialViewLocationFormats = new[] { 
                "~/Plugins/Dreamy.Menu/Views/{1}/{0}.cshtml"
            };
            AreaPartialViewLocationFormats = new[] { 
                "~/Plugins/Dreamy.Menu/Views/{1}/{0}.cshtml"
            };
            AreaViewLocationFormats = new[] { 
                "~/Plugins/Dreamy.Menu/Views/{1}/{0}.cshtml"
            };
        }
    }
}

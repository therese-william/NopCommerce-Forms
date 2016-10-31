using Nop.Core.Plugins;
using Nop.Plugin.Dreamy.Menu.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Dreamy.Menu
{
    public class MenuPlugin:BasePlugin
    {
        private MenuObjectContext _context;
        public MenuPlugin(MenuObjectContext context)
        {
            _context = context;
        }

        public override void Install()
        {
            _context.Install();
            base.Install();
        }

        public override void Uninstall()
        {
            _context.Uninstall();
            base.Uninstall();
        }
    }
}

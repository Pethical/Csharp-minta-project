using Pethical.Framework;
using Pethical.Framework.Modularization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellModule
{
    [Priority(2)]
    public class ShellModule : Module
    {
        protected override void InitializeModule()
        {
            RegisterType(typeof(IShellWindow), typeof(ShellWindow));            
        }

    }
}

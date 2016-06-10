using Microsoft.Practices.Prism.Events;
using Pethical.Framework.Messaging;
using Pethical.Framework.Modularization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintaModule2
{
    [Priority(1000), ValidUserRequired("admin")]
    public class MintaModule2 : Module
    {
        public override string Name
        {
            get
            {
                return "Üzengető modul";
            }
        }
        protected override void InitializeModule()
        {
            RegisterView<StateButton>("ButtonRegion");                                        
        }
    }
}

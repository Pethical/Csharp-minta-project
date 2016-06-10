using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Pethical.Framework.Messaging;
using Pethical.Framework.Modularization;
using Pethical.Framework.TestModule;
using Pethical.Framework.UI;
using Pethical.Framework.Utils;
using Pethical.Framework.Views;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Pethical.Framework.MintaModule
{
    [Priority(500)]
    public class MintaModule : Module
    {
        public override string Name
        {
            get
            {
                return "Súgó";
            }
        }

        public MintaModule() : base() {
            
        }

        protected override void InitializeModule()
        {
            EventCommand command = new EventCommand(null, "Súgó");
            command.Children.Add(new AboutCommand("Névjegy", "2014"));
            RegisterCommand(command);            
            RegisterView<ModuleButton>("ButtonRegion");
            ServiceFinder.GetInstance<IEventAggregator>().GetEvent<AboutEvent>().Subscribe((sender) => {                
                MessageBox.Show("Névjegy", "Névjegy");
            });
        }
    }



}

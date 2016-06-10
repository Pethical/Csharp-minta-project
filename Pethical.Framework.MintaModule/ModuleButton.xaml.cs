using Microsoft.Practices.Prism.Events;
using Pethical.Framework.Messaging;
using Pethical.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pethical.Framework.MintaModule
{
    /// <summary>
    /// Interaction logic for ModuleButton.xaml
    /// </summary>
    public partial class ModuleButton : UserControl
    {
        public ModuleButton()
        {
            
            InitializeComponent();

            ServiceFinder.GetInstance<IEventAggregator>().GetEvent<StringEvent>().Subscribe((str) =>
            {
                msg.Text = str;
            });

            send.Click += (o, e) => {
                ServiceFinder.GetInstance<IEventAggregator>().GetEvent<StringEvent>().Publish( msg.Text );
            };
        }
    }
}

using Microsoft.Practices.Prism.Events;
using Pethical.Framework;
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

namespace MintaModule2
{
    /// <summary>
    /// Interaction logic for StateLabel.xaml
    /// </summary>
    public partial class StateButton : UserControl
    {

        private string _content;
        public StateButton()
        {
            InitializeComponent();
            
            btn.IsEnabled = false;

            btn.IsEnabledChanged += (o, e) => {
                btn.Content = btn.IsEnabled ? "Üzenet érkezett" : "Várakozás üzenetre";
            };

            ServiceFinder.GetInstance<IEventAggregator>().GetEvent<StringEvent>().Subscribe((str) =>
            {
                _content = str;                
                btn.IsEnabled =  !String.IsNullOrEmpty(_content) ;                
            });

            btn.Click += (o, e) => { MessageBox.Show(_content, "Üzenet a másik modultól"); };
        }
    }
}

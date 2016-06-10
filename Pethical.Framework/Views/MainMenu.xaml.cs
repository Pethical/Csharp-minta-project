using Pethical.Framework.UI;
using Pethical.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Pethical.Framework.Views
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Menu
    {
        public MainMenu()
        {
            InitializeComponent();
            InitMenu();
            ServiceFinder.GetInstance<ICommandContainer>().Commands.CollectionChanged += (o, e) => {
               Items.Clear();
               InitMenu();
            };
        }

        private void InitMenu()
        {
            MenuBuilder builder = new MenuBuilder(ServiceFinder.GetInstance<ICommandContainer>().Commands);
            foreach(MenuItem item in builder.Menu)
            {
                Items.Add(item);
            }

        }

    }
}

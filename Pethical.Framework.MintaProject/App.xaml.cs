using System;
using System.Windows;

namespace Pethical.Framework.MintaProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        /// <summary>
        /// Az alkalmazás indítását végzi el és a hibakezelést állítja be
        /// </summary>
        /// <param name="e">StartUpEventArgs</param>
        protected override void OnStartup(StartupEventArgs e)
        {
           // File.Delete("Application.log");
            base.OnStartup(e);
            PethicalApplication<MainWindow>.Start();
        }
   
    }
}

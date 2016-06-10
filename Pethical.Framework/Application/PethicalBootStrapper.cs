using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Pethical.Framework.Membership;
using Pethical.Framework.Messaging;
using Pethical.Framework.Modularization;
using Pethical.Framework.Services.ErrorHandling;
using Pethical.Framework.Services.Membership;
using Pethical.Framework.Utils;
using Pethical.Framework.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pethical.Framework
{
    /// <summary>
    /// Ez az osztály végzi el az alkalmazás kezdeti beállítását,
    /// pl. modulok betöltése, regisztrálása, az felhasználói felület inicializálása,...
    /// </summary>
    public class PethicalBootStrapper<TShell> : UnityBootstrapper where TShell : DependencyObject, new()
    {

        public PethicalBootStrapper() : base()
        {

        }

        /// <summary>
        /// Létrehozza az alkalmazás fő ablakát (Shell)
        /// </summary>
        /// <returns>A főablak</returns>
        protected override System.Windows.DependencyObject CreateShell()
        {
            DependencyObject window = new TShell();
            return window;                
        }

        /// <summary>
        /// Inicializálja a fő ablakot
        /// </summary>
        protected override void InitializeShell()
        {
            base.InitializeShell();
        }

        public override void Run(bool runWithDefaultConfiguration)
        {
            base.Run(runWithDefaultConfiguration);            
//            Shell = (DependencyObject)ServiceLocator.Current.GetInstance<IShellWindow>();
//            Application.Current.MainWindow = (Window) Shell;
            Application.Current.MainWindow.Show();
        }

        /// <summary>
        /// Létrehozza a modul katalógust
        /// Alapból egy <see cref="DirectoryModuleCatalog"/> katalógust hoz létre
        /// </summary>
        /// <returns>A katalógus</returns>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            if (Directory.Exists("./modules"))
            {
                return new PrioritizedDirectoryModuleCatalog() { ModulePath = @".\modules" };                
            }
            else return base.CreateModuleCatalog();
            
        }

        /// <summary>
        /// Inicializálja a modulokat
        /// </summary>
        protected override void InitializeModules()
        {
            base.InitializeModules();
            // IModuleManager manager = ServiceLocator.Current.GetInstance<IModuleManager>();
            /*
             Később kellhet.
             manager.LoadModule("HelloWorld");            
            */
        }

        /// <summary>
        /// A modulok katalógusát állítja be. Beregisztrálja az összes
        /// használatban lévő modult a modulkönyvtárba
        /// </summary>
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            PrioritizedDirectoryModuleCatalog moduleCatalog = (PrioritizedDirectoryModuleCatalog)this.ModuleCatalog;
        }

        protected virtual void RegisterILogger()
        {
            RegisterTypeIfMissing(typeof(ILogger), typeof(Log4NetLogger), true);
        }

        protected override Microsoft.Practices.Prism.Logging.ILoggerFacade CreateLogger()
        {
            return new Log4NetLogger();
        }


        /// <summary>
        /// A szerviz katalógusba regisztrálja be a szolgáltatás felületeket és a megvalósításaikat
        /// </summary>
        protected override void ConfigureContainer()
        {
            
            Logger.Log("ConfigureContainer", Category.Debug, Priority.Low);

            base.ConfigureContainer();

            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(Container));      
     
            Logger.Log("Create application logger", Category.Debug, Priority.Low);            
            RegisterILogger();
            Logger.Log("Register application types", Category.Debug, Priority.Low);
            RegisterTypes();
            ServiceLocator.Current.GetInstance<ILogger>().Debug("All types registered");

            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<ExceptionEvent>().Subscribe((e) =>
            {
                ServiceLocator.Current.GetInstance<ILogger>().Error(e.Message, e);
                ServiceLocator.Current.GetInstance<IErrorHandler>().HandleException(e);
            });

            foreach (var reg in Container.Registrations)
            {
                ServiceLocator.Current.GetInstance<ILogger>().DebugFormat("{0} => {1}", reg.RegisteredType.Name, reg.MappedToType.Name);
            }

            ServiceLocator.Current.GetInstance<IRegionManager>().RegisterViewWithRegion("MenuRegion", typeof(MainMenu));
        }

        protected virtual void RegisterTypes()
        {
            RegisterTypeIfMissing(typeof(ISession), typeof(BaseSession), true);
            RegisterTypeIfMissing(typeof(IErrorSender), typeof(RedmineErrorSender), true);
            RegisterTypeIfMissing(typeof(IErrorReporter), typeof(DialogErrorReporter), true);
            RegisterTypeIfMissing(typeof(IErrorHandler), typeof(BasicErrorHandler), true);
//            RegisterTypeIfMissing(typeof(IEventAggregator), typeof(EventAggregator), true);
            RegisterTypeIfMissing(typeof(ICommandContainer), typeof(CommandContainer), true);
        }

    }
}

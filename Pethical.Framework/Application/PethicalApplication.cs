using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using Pethical.Framework.Messaging;
using Pethical.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Pethical.Framework
{
    public class PethicalApplicationBase<TShell, TBootStrapper> where TShell : DependencyObject, new()
                                                            where TBootStrapper : PethicalBootStrapper<TShell>, new()
    {
        public PethicalApplicationBase()
        {
            Thread.CurrentThread.Name = "Main Thread";

            TBootStrapper bootstrapper = new TBootStrapper();            
            bootstrapper.Run();

            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionEventHandler;

            Application.Current.Exit += OnExit;
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            ILogger logger = ServiceFinder.GetInstance<ILogger>();
            if(logger!=null)
                logger.InfoFormat("Application exited with code {0}", e.ApplicationExitCode); 
        }

        

        private void UnhandledExceptionEventHandler(object sender, UnhandledExceptionEventArgs e)
        {
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<ExceptionEvent>().Publish(e.ExceptionObject as Exception);            
        }

        public static void Start()
        {
            new PethicalApplicationBase<TShell, TBootStrapper>();
        }
    }

    public class PethicalGenericApplication<TShell> : PethicalApplicationBase<TShell, PethicalBootStrapper<TShell>> where TShell : DependencyObject, new()
    {

    }

    public class PethicalApplication<TShell> where TShell : DependencyObject, new()
    {
        private PethicalApplication()
        {
            
        }

        public static void Start()
        {
            PethicalApplicationBase<TShell, PethicalBootStrapper<TShell>>.Start();
        }

    }
}

using System;
using Microsoft.Practices.ServiceLocation;

namespace Pethical.Framework.Services.ErrorHandling
{
    /// <summary>
    /// Alapvető hibakezelő osztály amely származtatható, ha szükséges
    /// Hiba keletkezése esetén azt reportolja a felhasználónak, majd, ha szükséges
    /// és van rá élő szervíz, elküldi a fejlesztőknek
    /// </summary>
    public class BasicErrorHandler : ErrorHandlerBase
    {
        /// <summary>
        /// Hiba keletkezése esetén azt reportolja a felhasználónak, majd, ha szükséges
        /// és van rá élő szervíz, elküldi a fejlesztőknek
        /// </summary>
        /// <param name="e">A hibát leíró kivétel</param>
        public override void HandleException(Exception e)
        {
            IErrorReporter errorReporter = ServiceLocator.Current.GetInstance<IErrorReporter>();
            if (errorReporter == null) return;
            if (errorReporter.ReportException(e))
            {
                IErrorSender errorSender = ServiceLocator.Current.GetInstance<IErrorSender>();
                if (errorSender == null) return;
                errorSender.SendException(e);
            }
        }
    }
}

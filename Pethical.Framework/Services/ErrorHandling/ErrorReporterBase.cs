using System;

namespace Pethical.Framework.Services.ErrorHandling
{
    /// <summary>
    /// Alap osztály a hibák reportolásához.
    /// </summary>
    public abstract class ErrorReporterBase : IErrorReporter
    {
        /// <summary>
        /// Feladata, hogy jelezze a hibát a felhasználó felé és eldöntse, szükséges-e küldeni
        /// a fejlesztőknek
        /// </summary>
        /// <param name="e">A hibát leíró kivétel</param>
        /// <returns>True, ha a hibát el kell küldeni a fejlesztőknek, egyébként false</returns>
        abstract public bool ReportException(Exception e);
    }
}

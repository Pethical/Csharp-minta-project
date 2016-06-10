using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pethical.Framework.Services.ErrorHandling
{
    /// <summary>
    /// A feladata a különböző fellépő exceptionok, hibák jelentése a felhasználó felé
    /// </summary>
    public interface IErrorReporter
    {
        /// <summary>
        /// Hiba jelentését végzi
        /// </summary>
        /// <param name="e">A hibát leíró kivétel</param>
        /// <returns>A visszatérési értéke a felhasználónak feltett kérdésre a válasz. Normális
        /// esetben az, hogy küldjük-e el a hibát a fejlesztőknek.
        /// </returns>
        bool ReportException(Exception e);
    }
}

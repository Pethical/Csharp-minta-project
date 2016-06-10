using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pethical.Framework.Services.ErrorHandling
{
    /// <summary>
    /// Általános hibakezelő inteface.
    /// Az implementációban lehetőség van a kapott exception kezelésére,
    /// jelentésére, ...
    /// </summary>
    public interface IErrorHandler
    {
        /// <summary>
        /// Ezt a rutin hívódik meg egy exception kiváltódása esetén
        /// </summary>
        /// <param name="e">A kiváltódott kivétel</param>
        void HandleException(Exception e);
    }
}

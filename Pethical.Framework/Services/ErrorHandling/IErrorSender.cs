using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pethical.Framework.Services.ErrorHandling
{
    /// <summary>
    /// Az interface definálja a hibák beküldésének módját
    /// </summary>
    public interface IErrorSender
    {
        /// <summary>
        /// A hiba beküldését megvalósító rutin
        /// </summary>
        /// <param name="e">Az exception mely a hibát írja le</param>
        void SendException(Exception e);
    }
}

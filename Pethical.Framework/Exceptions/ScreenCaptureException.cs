using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pethical.Framework
{
    /// <summary>
    /// Abban az esetben áll elő, ha nem sikerült valamilyen okból a képernyőkép
    /// elkészítése
    /// </summary>
    public class ScreenCaptureException : Exception
    {
        /// <summary>
        /// Új példányt hoz létre a ScreenCaptureException kivételből
        /// </summary>
        /// <param name="message">A hibaüzenet szövege</param>
        /// <param name="innerException">A belső kivétel</param>
        public ScreenCaptureException(string message, Exception innerException)
            : base(message, innerException)
        { }

        /// <summary>
        /// Új példányt hoz létre a ScreenCaptureException kivételből
        /// </summary>
        /// <param name="message">A hibaüzenet szövege</param>
        public ScreenCaptureException(string message)
            : base(message)
        { }

    }

}

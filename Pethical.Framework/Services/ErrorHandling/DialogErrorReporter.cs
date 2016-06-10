using System;

namespace Pethical.Framework.Services.ErrorHandling
{
    /// <summary>
    /// Egy dialógussal jelzi a felhasználó felé a hibát, és megkérdezi,
    /// hogy küldjön-e jelentést a program a fejlesztőknek
    /// </summary>
    public class DialogErrorReporter : ErrorReporterBase
    {
        /// <summary>
        /// Megjeleníti a hibaüzenetet és megkérdezi, küldjünk-e jelentést
        /// a fejlesztőknek
        /// </summary>
        /// <param name="e">A hibát leíró kivétel</param>
        /// <returns>A felhasználó válasza, true, ha küldjünk hibát, egyéb esetben false.</returns>
        public override bool ReportException(Exception e)
        {
            ErrorWindow errorwindow = new ErrorWindow(e);
            errorwindow.ShowDialog();
            if (errorwindow.DialogResult == true)
            {
                return true;
            }
            return false;
        }
    }
}

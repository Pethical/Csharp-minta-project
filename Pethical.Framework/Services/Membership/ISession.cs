using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pethical.Framework.Services.Membership
{
    /// <summary>
    /// Ez az interface a felhasználói munkamenetet írja le.
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// A munkamenet egyedi azonosítója
        /// </summary>
        Guid? SessionId { get; set; }
        
        /// <summary>
        /// Mikor kezdődött a session
        /// </summary>
        DateTime Started { get; set; }

        /// <summary>
        /// Az utolsó tevékenység ideje
        /// </summary>
        DateTime LastTouched { get; set; }

        /// <summary>
        /// A felhasználó akié a session, vagy null, ha jelen pillanatban nincs belépett
        /// felhasználó (Anonymus session)
        /// </summary>
        IUser User { get; set; }

        /// <summary>
        /// Lezárja a sessiont.
        /// A megvalósításnak le kell zárnia a munkamenetet, kiléptetni a
        /// felhasználót és gondoskodni róla, hogy az aktuális session ne legyen újra
        /// használva.
        /// </summary>
        void Close();

        /// <summary>
        /// Új session-t generál a régi alapján.
        /// </summary>
        /// <returns></returns>
        ISession RegenerateSession();

    }
}

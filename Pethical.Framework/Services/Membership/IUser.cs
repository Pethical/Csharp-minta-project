using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pethical.Framework.Services.Membership
{
    /// <summary>
    /// Azokat a mezőket írja le, amelyekkel egy felhasználónka mindenképpen
    /// rendelkeznie kell.
    /// Főként az autentikációnál használt ez a felület
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// A felhasználó login neve
        /// </summary>
        string Login { get; set; }

        /// <summary>
        /// A felhasználó jelszava (Lehetőleg kódolva)
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Aktív-e a felhasználó
        /// </summary>
        bool Active { get; set; }

        /// <summary>
        /// Milyen jogosultságokkal (role) rendelkezik
        /// </summary>
        IRoleRepository<IRole> Roles { get; set; }
    }
}

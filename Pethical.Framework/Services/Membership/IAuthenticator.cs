using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pethical.Framework.Services.Membership
{
    /// <summary>
    /// Autentikálást végző felület
    /// </summary>
    public interface IAuthenticator<T, T1> where T : IUser where T1 : IRole
    {
        /// <summary>
        /// A jogosultságokat tartalmazó gyűjtő
        /// </summary>
        IRoleRepository<T1> RoleRepository { get; set; }

        /// <summary>
        /// A felhasználókat tartalmazó gyűjtő
        /// </summary>
        IUserRepository<T> UserRepository { get; set; }

        /// <summary>
        /// Az aktuális felhasználót adja vissza, vagy null-t, ha nincs
        /// </summary>
        T CurrentUser { get; set; }

        /// <summary>
        /// Felhasználót léptet be
        /// </summary>
        /// <param name="login">A felhasználó neve</param>
        /// <param name="password">A felhasználó jelszava</param>
        /// <returns>A felhasználó, vagy null, ha nem volt sikeres a belépés</returns>
        T Authenticate(string login, string password);

        /// <summary>
        /// Hash-t készít a jelszóból
        /// </summary>
        /// <param name="password">A plain text jelszó</param>
        /// <returns>A jelszó hash</returns>
        string HashPassword(string password);
    }
}

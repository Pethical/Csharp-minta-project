using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pethical.Framework.Services.Membership
{
    /// <summary>
    /// A felület a felhasználók kezelését definiálja
    /// </summary>
    public interface IUserRepository<T> : IList<T> where T : IUser
    {   
        /// <summary>
        /// Felhasználót töröl
        /// </summary>
        /// <param name="user">A felhasználót reprezentáló objektum</param>
        /// <returns>True, ha sikerült a mentés, egyéb esetben false</returns>
        bool DeleteUser(T user);

        /// <summary>
        /// Elmenti a felhasználót
        /// </summary>
        /// <param name="user">A felhasználót leíró objektum amelyet menteni kell</param>
        /// <returns>Az elmentett felhasználót leíró objektum</returns>
        T SaveUser(T user);
    }
}

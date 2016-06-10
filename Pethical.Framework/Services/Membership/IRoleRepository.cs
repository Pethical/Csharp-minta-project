using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pethical.Framework.Services.Membership
{
    /// <summary>
    /// Role kezelés felülete
    /// </summary>
    /// <typeparam name="T">IRole-t implementáló Role tipus</typeparam>
    public interface IRoleRepository<T> : IList<T> where T : IRole
    {
        /// <summary>
        /// Felhasználó ad a role-hoz
        /// </summary>
        /// <param name="user">A felhasználó</param>
        /// <param name="Role">A role</param>
        /// <returns>True, ha sikeres volt, egyéb esetben false</returns>
        bool AddUserToRole(IUser user, T Role);

        /// <summary>
        /// Felhasználó ad a role-hoz
        /// </summary>
        /// <param name="loginname">A felhasználó login neve</param>
        /// <param name="rolename">A role neve</param>
        /// <returns>True, ha sikeres volt, egyéb esetben false</returns>
        bool AddUserToRole(string loginname, string rolename);

        /// <summary>
        /// Kiveszi a felhasználót a role-ból
        /// </summary>
        /// <param name="user">A felhasználó</param>
        /// <param name="Role">A role</param>
        /// <returns>True, ha sikeres volt, egyéb esetben false</returns>
        bool RemoveUserFromRole(IUser user, T Role);
        /// <summary>
        /// Kiveszi a felhasználót a role-ból
        /// </summary>
        /// <param name="loginname">A felhasználó neve</param>
        /// <param name="rolename">A role neve</param>
        /// <returns>True, ha sikeres volt, egyéb esetben false</returns>
        bool RemoveUserFromRole(string loginname, string rolename);

        /// <summary>
        /// Megmodja, hogy az adott felhasználó benne van-e a role-ban
        /// </summary>
        /// <param name="user">A felhasználó</param>
        /// <param name="role">A role</param>
        /// <returns>True, ha benne van, egyébként false</returns>
        bool UserInRole(IUser user, T role);

        /// <summary>
        /// Megmodja, hogy az adott felhasználó benne van-e a role-ban
        /// </summary>
        /// <param name="loginname">A felhasználó neve</param>
        /// <param name="rolename">A role neve</param>
        /// <returns>True, ha benne van, egyébként false</returns>
        bool UserInRole(string loginname, string rolename);

        /// <summary>
        /// Elmenti az új vagy módosított Role-t
        /// </summary>
        /// <param name="role">A role objektum</param>
        /// <returns>Az elmentett role</returns>
        T CreateRole(T role);

        /// <summary>
        /// Törli a Role-t
        /// </summary>
        /// <param name="role">A role objektum amit törölni szeretnénk</param>
        /// <returns></returns>
        bool DeleteRole(T role);

    }
}

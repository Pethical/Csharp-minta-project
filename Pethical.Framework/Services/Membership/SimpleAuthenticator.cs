using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pethical.Framework.Services.Membership
{
    /// <summary>
    /// Egyszerű authentikációs class
    /// </summary>
    public class SimpleAuthenticator : AuthenticatorBase
    {
        /// <summary>
        /// Alapértelmezett konstruktor
        /// </summary>
        /// <param name="users">A felhasználókat tartalmazó gyűjtemény</param>
        /// <param name="roles">A jogokat tartalmazó gyűjtemény</param>
        public SimpleAuthenticator(IUserRepository<IUser> users, IRoleRepository<IRole> roles)
        {
            UserRepository = users;
            RoleRepository = roles;
        }

        /// <summary>
        /// A jogokat tartalmazó gyűjtemény
        /// </summary>
        public override IUserRepository<IUser> UserRepository
        {
            get;
            set;
        }

        /// <summary>
        /// A felhasználókat tartalmazó gyűjtemény
        /// </summary>
        public override IRoleRepository<IRole> RoleRepository
        {
            get;
            set;
        }
    }
}

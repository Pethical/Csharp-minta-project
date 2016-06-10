using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pethical.Framework.Services.Membership
{
    /// <summary>
    /// Role-ok (jogosultsági csoportok) felülete
    /// </summary>
    public interface IRole
    {
        /// <summary>
        /// A role neve
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// A role leírása
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Aktív-e a role
        /// </summary>
        bool Active { get; set; }

        /// <summary>
        /// Milyen felhasználók tartoznak a Role-ba
        /// </summary>
        IUserRepository<IUser> Members { get; set; }
    }
}

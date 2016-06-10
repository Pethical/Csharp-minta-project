using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Prism.Events;
using Pethical.Framework.Messaging;
using Pethical.Framework.Services.Membership;
using System.Security.Cryptography;

namespace Pethical.Framework.Services.Membership
{
    /// <summary>
    /// Az osztály az authentikáció alapjául szolgált.
    /// <see cref="NotificationObject"/>-ből származik és megvalósítja a <see cref="IAuthenticator{IUser, IRole}"/> interface-t
    /// Az aktuális felhasználó változása esetén pedig publikálja a <see cref="CurrentUserChanged{T}" /> eseményt
    /// </summary>
    public abstract class AuthenticatorBase : NotificationObject, IAuthenticator<IUser, IRole>
    {
        private IUser _currentUser;        

        /// <summary>
        /// Az aktuális felhasználó aki be van lépve a rendszerbe
        /// </summary>
        public IUser CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                if (_currentUser != value)
                {
                    _currentUser = value;
                    RaisePropertyChanged(() => CurrentUser);
                    ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<CurrentUserChanged<IUser>>().Publish(_currentUser);
                }
            }
        }

        /// <summary>
        /// Az authentikációt végző rutin
        /// </summary>
        /// <param name="login">A felhasználó neve</param>
        /// <param name="password">A felhasználó jelszava</param>
        /// <returns>Sikeres autentikáció esetén a felhasználót leíró objektum
        /// egyéb esetben null
        /// </returns>
        public virtual IUser Authenticate(string login, string password)
        {
            var loginUserCollection = UserRepository.Where<IUser>(p => p.Login == login 
                                                                       && p.Password == HashPassword(password)
                                                                       && p.Active == true);
            if(loginUserCollection!=null && loginUserCollection.Count<IUser>() != 0)
            {
                CurrentUser = loginUserCollection.FirstOrDefault<IUser>();
                return CurrentUser;
            }
            return null;
        }

        /// <inheriteDoc />
        public abstract IUserRepository<IUser> UserRepository { get; set; }

        /// <inheriteDoc />
        public abstract IRoleRepository<IRole> RoleRepository { get; set; }

        /// <inheriteDoc />
        public virtual string HashPassword(string password)
        {
            HashAlgorithm algorithm = SHA1.Create();
            StringBuilder sb = new StringBuilder();
            foreach (byte b in algorithm.ComputeHash(Encoding.UTF8.GetBytes(password)))
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}

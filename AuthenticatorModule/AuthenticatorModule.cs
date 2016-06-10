using Microsoft.Practices.Prism.Events;
using Pethical.Framework.Messaging;
using Pethical.Framework.Modularization;
using Pethical.Framework.Services.Membership;
using Pethical.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AuthenticatorMinta
{
    [Priority(int.MinValue)]
    public class AuthenticatorModule : Module
    {

        public override string Name
        {
            get
            {
                return "Authenticator";
            }
        }

        protected override void InitializeModule()
        {
            RegisterType(typeof(IAuthenticator<IUser, IRole>), typeof(Authenticator));
        }
    }


    public class Authenticator : SimpleAuthenticator
    {
        
        private static object authLock = new object();

        public Authenticator(): base(null, null)
        {
            UserRepository = new UserRepository();
            RoleRepository = new RoleRepository();
            
            RoleRepository.Add(new Role() { Name = "login", Active = true });
            RoleRepository.Add(new Role() { Name = "admin", Active = true });
            User user = new User() { Active = true, Login = "admin", Password = "admin" };
            user.Roles.Add(RoleRepository.FirstOrDefault(p => p.Name == "admin"));
            UserRepository.Add(user);

            ServiceFinder.GetInstance<IEventAggregator>().GetEvent<AuthenticationNeeded>().Subscribe((auser) =>
            {
                lock (authLock)
                {
                    IAuthenticator<IUser, IRole> authenticator = ServiceFinder.GetInstance<IAuthenticator<IUser, IRole>>();
                    if ((authenticator != null) && (authenticator.CurrentUser == null))
                        authenticator.Authenticate(null, null);
                }
            });
        }

        public override IUser Authenticate(string login, string password)
        {            
            string username, pass;
            IUser user = null;

            ServiceFinder.GetInstance<ILogger>().DebugFormat("User authentication with {0}", GetType().Name);

            do
            {
                if (LoginWindow.Login(out username, out pass))
                {
                    user = base.Authenticate(username, pass);
                    if (user == null)
                    {
                        ServiceFinder.GetInstance<ILogger>().WarnFormat("Authentication failed {0}", username);
                    }
                }
                else { break; }
            } while (user == null);
            return user;
        }

        public override string HashPassword(string password)
        {
            return password;
        }
    }

    public class User : IUser
    {

        public string Login { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public IRoleRepository<IRole> Roles { get; set; }
        public User()
        {
            Roles = new RoleRepository();
        }
    }

    public class Role : IRole{

        public string Name { get; set; }

        public string Description{ get; set; }

        public bool Active{ get; set; }

        public IUserRepository<IUser> Members { get; set; }
    }

    public class UserRepository : List<IUser>, IUserRepository<IUser>
    {
        public bool DeleteUser(IUser user)
        {
            return Remove(user);
        }

        public IUser SaveUser(IUser user)
        {
            Add(user);
            return user;
        }
    }

    public class RoleRepository : List<IRole>, IRoleRepository<IRole>
    {
        public bool AddUserToRole(IUser user, IRole Role)
        {
            throw new NotImplementedException();
        }

        public bool AddUserToRole(string loginname, string rolename)
        {
            throw new NotImplementedException();
        }

        public bool RemoveUserFromRole(IUser user, IRole Role)
        {
            throw new NotImplementedException();
        }

        public bool RemoveUserFromRole(string loginname, string rolename)
        {
            throw new NotImplementedException();
        }

        public bool UserInRole(IUser user, IRole role)
        {
            return UserInRole(user.Login, role.Name);
        }

        public bool UserInRole(string loginname, string rolename)
        {
            return this.Where(p => p.Name == rolename).Where(o => o.Members.Count(q => q.Login == loginname) > 0).Count() > 0;
        }

        public IRole CreateRole(IRole role)
        {
            return new Role();
        }

        public bool DeleteRole(IRole role)
        {
            return Remove(role);
        }
    }



}

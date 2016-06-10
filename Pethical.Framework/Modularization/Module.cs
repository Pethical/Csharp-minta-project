using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Pethical.Framework.Messaging;
using Pethical.Framework.Services.Membership;
using Pethical.Framework.UI;
using Pethical.Framework.Utils;
using Pethical.Framework.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pethical.Framework.Modularization
{

    /// <summary>
    ///  Valós felhasználó szükséges a használathoz
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ValidUserRequiredAttribute : Attribute
    {
        readonly List<string> _roles;

        public ValidUserRequiredAttribute(params string[] roles) : this()
        {
            _roles = roles.ToList();
        }

        public ValidUserRequiredAttribute()
        {
            
        }

        public bool RolesRequired
        {
            get
            {
                return _roles != null && _roles.Count > 0;
            }
        }

        public List<string> Roles
        {
            get
            {
                return _roles;
            }
        }
    }
            
    public abstract class Module : IModule
    {

    #region Props

        public virtual string Name
        {
            get
            {
                return GetType().FullName;
            }
        }

        public virtual string Description
        {
            get
            {
                return string.Empty;
            }
        }

        private List<string> _requiredRoles;

        public List<string> RequiredRoles
        {
            get
            {
                return _requiredRoles;
            }
            set
            {
                _requiredRoles = value;
            }
        }

        private bool _requireValidUser = false;

        public virtual bool RequireValidUser
        {
            get
            {
                return _requireValidUser;
            }
            set
            {
                _requireValidUser = value;
            }
        }

        public ISession Session
        {
            get
            {
                return GetInstance<ISession>();
            }
        }

        public IUser CurrentUser
        {
            get
            {
                ISession session = Session;
                return session == null ? null : session.User;
            }
        }

        public ILogger Log
        {
            get
            {
                return GetInstance<ILogger>();
            }
        }

        public IRegionManager RegionManager
        {
            get
            {
                return GetInstance<IRegionManager>();
            }
        }

        public ICommandContainer Commands
        {
            get
            {
                return GetInstance<ICommandContainer>();
            }
        }

    #endregion

        protected abstract void InitializeModule();
        public virtual void UnInitializeModule()
        {

        }
        
        protected virtual void AfterInitialize() { }
        protected virtual void BeforeInitialize() { }        

        public void Initialize()
        {
            Log.DebugFormat("Initializing module: {0}", Name);

            if (_requireValidUser && !CheckUser())
            {
                Log.DebugFormat("{0} requires valid user and no one user logged in", Name);
                RegisterEventHandler<CurrentUserChanged<IUser>, IUser>(OnUserAuthenticated);
                GetInstance<IEventAggregator>().GetEvent<AuthenticationNeeded>().Publish(null);
                return;
            }
            else if (!_requireValidUser)
                InitializeModule();
            else OnUserAuthenticated(CurrentUser);
        }

        private void OnUserAuthenticated(IUser user)
        {
            Log.InfoFormat("{0} logged in", user.Login);

            foreach (string role in RequiredRoles)
            {
                if ((user.Roles == null) || user.Roles.Count(p => p.Name == role) == 0)
                {
                    Log.InfoFormat("{0} hasn't {1} role, aborting module {2}", user.Login, role, Name);
                    return;
                }
            }
            
            ServiceFinder.GetInstance<IEventAggregator>().GetEvent<CurrentUserChanged<IUser>>().Unsubscribe(OnUserAuthenticated);

            InitializeModule();
        }        

        private bool CheckUser()
        {
            ISession session = GetInstance<ISession>();
            if (session == null) Log.Warn("No Session registered!");
            IAuthenticator<IUser, IRole> authenticator = ServiceFinder.GetInstance<IAuthenticator<IUser, IRole>>();
            return (authenticator != null) && authenticator.CurrentUser != null;
            // session != null && session.User != null;
        }

        public Module()
        {
            Log.DebugFormat("{0} Loaded", Name);

            RequiredRoles = new List<string>();

            ValidUserRequiredAttribute[] validUserRequires = (ValidUserRequiredAttribute[])GetType().GetCustomAttributes(typeof(ValidUserRequiredAttribute), true);
            
            if (validUserRequires != null && validUserRequires.Count() > 0)
            {
                _requireValidUser = true;
                if (validUserRequires.Count() > 0)
                {
                    if(validUserRequires[0].RolesRequired)
                    {
                        foreach (string role in validUserRequires[0].Roles)
                        {                            
                            RequiredRoles.Add(role);                            
                        }
                    }                    
                }
                
            }
        }

        public void RegisterEventHandler<TEvent, TPayload>(Action<TPayload> action) where TEvent : CompositePresentationEvent<TPayload>, new()
        {
            GetInstance<IEventAggregator>().GetEvent<TEvent>().Subscribe(action);
        }

        protected void RegisterType(Type fromType, Type toType)
        {            
            GetInstance<IUnityContainer>().RegisterType(fromType, toType, new ContainerControlledLifetimeManager());
            Log.DebugFormat("{0} module mapped type {1} to type {2}", Name, fromType.Name, toType.Name);
        }

        protected TService GetInstance<TService>()
        {
            return ServiceFinder.GetInstance<TService>();
        }

        public void RegisterView(string region, Type view)
        {
            RegionManager.RegisterViewWithRegion(region, view);
            Log.DebugFormat("{2} module registered {0} view to {1} region", view.FullName, region, Name);
        }

        public void RegisterView<TView>(string region)
        {
            RegisterView(region, typeof(TView));
        }

        public void RegisterCommand(IUICommand command)
        {
            Commands.Commands.Add(command);
        }

        public IUICommand RegisterEventCommand<TEvent>(string text) where TEvent : CompositePresentationEvent<object>, new()
        {
            EventCommand<TEvent> command = new EventCommand<TEvent>(text);
            Commands.Commands.Add(command);
            return command;
        }

        public IUICommand RegisterEventCommand<TEvent, TPayload>(string text, TPayload payload) where TEvent : CompositePresentationEvent<TPayload>, new()                                                                                          
        {
            EventCommand<TEvent, TPayload> command = new EventCommand<TEvent, TPayload>(text);
            Commands.Commands.Add(command);
            return command;
       }        

    }
}

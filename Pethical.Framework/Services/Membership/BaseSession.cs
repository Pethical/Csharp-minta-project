using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Prism.Events;
using Pethical.Framework.Messaging;
using Pethical.Framework.Services.Membership;

namespace Pethical.Framework.Membership
{
    /// <summary>
    /// Alapvető session osztály
    /// </summary>
    public class BaseSession : ISession
    {
        private Guid? _sessionId;
        private DateTime _started;
        private DateTime _lasttouched;
        private IUser _user;

        /// <summary>
        /// Az munkamenet azonosítója
        /// </summary>
        public Guid? SessionId
        {
            get
            {
                return _sessionId;
            }
            set
            {
                _sessionId = value;
            }
        }

        /// <summary>
        /// Az időpont amikor a munkamenet létrejött
        /// </summary>
        public DateTime Started
        {
            get
            {
                return _started;
            }
            set
            {
                _started = value;
            }
        }

        /// <summary>
        /// Az utolsó interakció ideje
        /// </summary>
        public DateTime LastTouched
        {
            get
            {
                return _lasttouched;
            }
            set
            {
                _lasttouched = value;
            }
        }

        /// <summary>
        /// A munkamenethez tartozó felhasználó.
        /// Ha nincs belépve felhasználó <c>null</c> értéket vesz fel
        /// </summary>
        public IUser User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
            }
        }

        /// <summary>
        /// Lezárja a munkamenetet és az aktuális felhasználót is törli (kilépteti a felhasználót)
        /// </summary>
        public void Close()
        {
            _sessionId = null;
            _user = null;
        }

        /// <summary>
        /// Új példányt hoz létre a sessionből (Új sessiont indít)
        /// </summary>
        public BaseSession()
        {
            SessionId   = new Guid();
            Started     = DateTime.Now;
            LastTouched = DateTime.Now;
            User = null;
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<SessionCreated<ISession>>().Publish(this);
        }
        
        /// <summary>
        /// Új munkamenet azonosítót generál (Új munkamenetet indít)
        /// </summary>
        /// <returns>Az új munkamenet</returns>
        public virtual ISession RegenerateSession()
        {
            return new BaseSession();
        }


    }
}

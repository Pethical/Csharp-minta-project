using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;
using Pethical.Framework.Services.Membership;

namespace Pethical.Framework.Messaging
{
    /// <summary>
    /// Alap osztály az üzenetek küldéséhez
    /// </summary>
    /// <typeparam name="T">Bármilyen típus lehet. A küldendő üzenet tipusa</typeparam>
    public class RoutedEvent<T> : CompositePresentationEvent<T>
    {
    }

    /// <summary>
    /// Az üzenet azt jelzi, hogy valamilyen kivétel jelentkezett a rendszerben.
    /// </summary>
    /// <remarks>Ha egy kivételt nem kapunk el, akkor a fő alkalmazás elkapja, és ezt az üzenetet fogja küldeni</remarks>
    /// <example><code lang="cs">
    ///     try
    ///     {
    ///        ...
    ///     }
    ///     catch (Exception e)
    ///     {
    ///         ServiceLocator.Current.GetInstance&lt;IEventAggregator&gt;().GetEvent&lt;ExceptionEvent&gt;().Publish(e);
    ///     }
    /// </code></example>
    public class ExceptionEvent : RoutedEvent<Exception>
    {

    }

    /// <summary>
    /// Sima szöveg alapú üzenet küldésére használható tipus
    /// </summary>
    public class StringEvent : RoutedEvent<String>
    {

    }

    public class LogInfoEvent : StringEvent
    {

    }

    /// <summary>
    /// Az üzenet azt jelzi, hogy az aktuális felhasználó megváltozott
    /// (A felhasználó kilépett, belépett)
    /// </summary>
    /// <typeparam name="T">A felhasználót leíró tipus</typeparam>        
    public class CurrentUserChanged<T> : RoutedEvent<T> where T : IUser
    {

    }

    public class AuthenticationNeeded : RoutedEvent<IUser>
    {

    }

    /// <summary>
    /// Az üzenet azt jelzi, hogy egy munkamenet elindult
    /// </summary>
    /// <typeparam name="T">A munkamenetet leíró tipus</typeparam>        
    public class SessionCreated<T> : RoutedEvent<T> where T : ISession
    {

    }

    /// <summary>
    /// Az üzenet azt jelzi, hogy az alkalmazás befelyezése folyamatban van, azaz 
    /// a szükséges mentéseket, kapcsolatok bontását, stb. el kell végezni.
    /// Az üzenet tartalma egy <b>bool</b> tipus, amely a hibát jelzi, azaz,
    /// ha az alkalmazás hiba miatt áll le, az értéke true, egyéb esetben false
    /// </summary>
    public class ApplicationShutdown : RoutedEvent<bool>
    {

    }

}

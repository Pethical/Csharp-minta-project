using System;

namespace Pethical.Framework.Services.ErrorHandling
{
    /// <summary>
    /// Alap abstract osztály a hibák kezelésére. Ebből származtatjuk
    /// a hibakezelésre hivatott osztályunkat. Alapértelmezett működés 
    /// szerint az osztály feladata a nem elkapott, nem lekezelt exception-ok
    /// kezelése.
    /// </summary>
    /// <remarks>
    /// A származtatott osztály felelőssége, hogy az adott hibával mit tesz, így
    /// a hiba jelentése is a felhasználó felé, vagy a küldése.
    /// </remarks>
    public abstract class ErrorHandlerBase : IErrorHandler
    {
        /// <summary>
        /// Ez a metódus gondoskodik a keletkezett hibák lekezeléséről.
        /// </summary>
        /// <param name="e">A keletkezett hibát leíró exception</param>
        abstract public void HandleException(Exception e);
    }
}

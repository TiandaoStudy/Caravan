using System;
using Finsa.CodeServices.Clock;

namespace Finsa.Caravan.Common
{
    /// <summary>
    ///   Alcuni servizi essenziali usati all'interno di Caravan. Classe introdotta per
    ///   retrocompatibilità verso alcuni componenti che, oramai, non riescono più ad essere
    ///   adattati all'uso di Ninject.
    /// </summary>
    public static class ServiceProvider
    {
        /// <summary>
        ///   L'orologio usato di default per il calcolo dell'ora corrente.
        /// </summary>
        public static IClock Clock { get; set; } = new SystemClock();

        /// <summary>
        ///   La funzione usata per gestire l'ora corrente all'interno della sorgente dati.
        ///   Di default, usa <see cref="Clock"/> e la proprietà <see cref="IClock.UtcNow"/>.
        /// </summary>
        public static Func<DateTime> CurrentDateTime { get; set; } = () => Clock.UtcNow;
    }
}

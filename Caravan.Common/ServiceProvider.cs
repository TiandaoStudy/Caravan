using System;
using Finsa.CodeServices.Clock;
using NLog.Targets;

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

        /// <summary>
        ///   Il log di emergenza, basato rigorosamente su file system, che viene usato da Caravan quando il log normale, per qualche ragione, non sta funzionando. 
        /// 
        ///   Nel log di emergenza vengono solo registrati gli errori che stanno indicando il mal funzionamento del log, non i normali messaggi.
        /// </summary>
        public static FileTarget EmergencyLog { get; set; } = new FileTarget
        {
            // Basic
            Encoding = System.Text.Encoding.UTF8,
            Header = "### CARAVAN EMERGENCY LOG ###",

            // Archiving
            ArchiveOldFileOnStartup = true,
            EnableArchiveFileCompression = true,
            ArchiveNumbering = ArchiveNumberingMode.DateAndSequence,
            MaxArchiveFiles = CommonConfiguration.Instance.Logging_EmergencyLog_MaxArchiveFiles,

            
        };
    }
}

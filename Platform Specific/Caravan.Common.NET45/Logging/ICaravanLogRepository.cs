// Copyright 2015-2025 Finsa S.p.A. <finsa@finsa.it>
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except
// in compliance with the License. You may obtain a copy of the License at:
// 
// "http://www.apache.org/licenses/LICENSE-2.0"
// 
// Unless required by applicable law or agreed to in writing, software distributed under the License
// is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
// or implied. See the License for the specific language governing permissions and limitations under
// the License.

using Common.Logging;
using Finsa.Caravan.Common.Logging.Models;
using Finsa.CodeServices.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace Finsa.Caravan.Common.Logging
{
    /// <summary>
    ///   Handles logging and log settings.
    /// </summary>
    public interface ICaravanLogRepository : IDisposable
    {
        #region Entries

        /// <summary>
        ///   Aggiunge all'applicazione data la voce di log passata come parametro.
        /// </summary>
        /// <param name="appName">Il nome dell'applicazione Caravan.</param>
        /// <param name="logEntry">La voce di log da inserire.</param>
        /// <returns>Il risultato dell'operazione di log.</returns>
        Task<LogResult> AddEntryAsync(string appName, LogEntry logEntry);

        /// <summary>
        ///   Aggiunge all'applicazione data le voci di log passate come parametro.
        /// </summary>
        /// <param name="appName">Il nome dell'applicazione Caravan.</param>
        /// <param name="logEntries">Le voci di log da inserire.</param>
        /// <returns>Il risultato dell'operazione di log.</returns>
        Task<LogResult> AddEntriesAsync(string appName, IEnumerable<LogEntry> logEntries);

        /// <summary>
        ///   Applica le impostazioni di log e pulisce i log più vecchi, quelli che superano il
        ///   limite di cardinalità, etc etc.
        /// </summary>
        Task CleanUpEntriesAsync();

        /// <summary>
        ///   Applica le impostazioni di log e pulisce i log più vecchi, quelli che superano il
        ///   limite di cardinalità, etc etc, ma solo per l'applicazione data.
        /// </summary>
        Task CleanUpEntriesAsync(string appName);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <returns></returns>
        IList<LogEntry> GetEntries();

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"><paramref name="appName"/> is null or empty.</exception>
        IList<LogEntry> GetEntries(string appName);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logLevel">The log type: INFO, DEBUG, etc etc.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///   <paramref name="logLevel"/> is not a valid <see cref="global::Common.Logging.LogLevel"/>.
        /// </exception>
        IList<LogEntry> GetEntries(LogLevel logLevel);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <param name="logLevel">The log type: INFO, DEBUG, etc etc.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///   <paramref name="appName"/> is null or empty. <paramref name="logLevel"/> is not a
        ///   valid <see cref="global::Common.Logging.LogLevel"/>.
        /// </exception>
        IList<LogEntry> GetEntries(string appName, LogLevel logLevel);

        /// <summary>
        ///   Interroga direttamente la sorgente dati, tramite i dati opzionali passati in input.
        /// </summary>
        /// <param name="logEntryQuery">I parametri con cui eseguire la query.</param>
        /// <returns>Tutte le righe che rispettano i parametri passati come argomento.</returns>
        IList<LogEntry> QueryEntries(LogEntryQuery logEntryQuery);

        [Pure]
        Option<LogEntry> GetEntry(string appName, long logId);

        /// <summary>
        ///   Removes a log entry. Use it to delete logs that contain sensitive information!
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <param name="logId"></param>
        /// <exception cref="ArgumentException"><paramref name="appName"/> is null or empty.</exception>
        void RemoveEntry(string appName, int logId);

        #endregion Entries

        #region Settings

        /// <summary>
        ///   TODO
        /// </summary>
        /// <returns></returns>
        IList<LogSetting> GetSettings();

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"><paramref name="appName"/> is null or empty.</exception>
        IList<LogSetting> GetSettings(string appName);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logLevel">The log type: INFO, DEBUG, etc etc.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///   <paramref name="logLevel"/> is not a valid <see cref="global::Common.Logging.LogLevel"/>.
        /// </exception>
        IList<LogSetting> GetSettings(LogLevel logLevel);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <param name="logLevel">The log type: INFO, DEBUG, etc etc.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///   <paramref name="appName"/> is null or empty. <paramref name="logLevel"/> is not a
        ///   valid <see cref="global::Common.Logging.LogLevel"/>.
        /// </exception>
        LogSetting GetSettings(string appName, LogLevel logLevel);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <param name="logLevel">The log type: INFO, DEBUG, etc etc.</param>
        /// <param name="setting"></param>
        void AddSetting(string appName, LogLevel logLevel, LogSetting setting);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <param name="logLevel">The log type: INFO, DEBUG, etc etc.</param>
        /// <param name="setting"></param>
        void UpdateSetting(string appName, LogLevel logLevel, LogSetting setting);

        /// <summary>
        ///   Removes a setting with a specificed log type in a specified application.
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <param name="logLevel">The log type: INFO, DEBUG, etc etc.</param>
        void RemoveSetting(string appName, LogLevel logLevel);

        #endregion Settings
    }
}

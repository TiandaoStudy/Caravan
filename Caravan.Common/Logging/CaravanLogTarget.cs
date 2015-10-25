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

using AutoMapper;
using Finsa.Caravan.Common.Logging.Models;
using Ninject;
using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using LogLevel = Common.Logging.LogLevel;

namespace Finsa.Caravan.Common.Logging
{
    /// <summary>
    ///   Target per il log di Caravan su database.
    /// </summary>
    [Target("CaravanLog")]
    public sealed class CaravanLogTarget : Target
    {
        private static readonly SimpleLayout DefaultUserLogin = new SimpleLayout("${identity:name=true:lowercase=true}");
        private static readonly SimpleLayout DefaultCodeUnit = new SimpleLayout("${callsite:className=true:methodName=false:lowercase=true}");
        private static readonly SimpleLayout DefaultFunction = new SimpleLayout("${callsite:className=false:methodName=true:lowercase=true}");

        /// <summary>
        ///   Il layout da applicare per mostrare l'utente loggato che ha prodotto il messaggio.
        /// </summary>
        [RequiredParameter]
        public Layout UserLogin { get; set; } = DefaultUserLogin;

        /// <summary>
        ///   Il layout da applicare per ottenere la classe, o il modulo, da cui è partito il messaggio.
        /// </summary>
        [RequiredParameter]
        public Layout CodeUnit { get; set; } = DefaultCodeUnit;

        /// <summary>
        ///   Il layout da applicare per ottenere la funzione, o la procedura, da cui è partito il messaggio.
        /// </summary>
        [RequiredParameter]
        public Layout Function { get; set; } = DefaultFunction;

        /// <summary>
        ///   Il layout da applicare per ottenere il contesto da dove è partito il messaggio.
        /// </summary>
        public Layout Context { get; set; }

        /// <summary>
        ///   Writes logging event to the log target classes.
        /// </summary>
        /// <param name="logEvent">Logging event to be written out.</param>
        protected async override void Write(LogEventInfo logEvent)
        {
            try
            {
                var logEntry = ToLogEntry(logEvent);
                var result = await CaravanServiceProvider.LogRepository.AddEntryAsync(CaravanCommonConfiguration.Instance.AppName, logEntry);

                if (!result.Succeeded)
                {
                    throw result.Exception;
                }
            }
            catch (Exception ex)
            {
                // Uso il log di emergenza nel caso ci siano stati errori.
                UseEmergencyLog(ex, logEvent.FormattedMessage);
            }
        }

        protected async override void Write(AsyncLogEventInfo asyncLogEvent)
        {
            try
            {
                var logEntry = ToLogEntry(asyncLogEvent.LogEvent);
                var result = await CaravanServiceProvider.LogRepository.AddEntryAsync(CaravanCommonConfiguration.Instance.AppName, logEntry);

                if (!result.Succeeded)
                {
                    throw result.Exception;
                }
            }
            catch (Exception ex)
            {
                // Uso il log di emergenza nel caso ci siano stati errori.
                UseEmergencyLog(ex, asyncLogEvent.LogEvent.FormattedMessage);
            }
        }

        protected async override void Write(AsyncLogEventInfo[] asyncLogEvents)
        {
            try
            {
                var logEntries = asyncLogEvents.Select(le => ToLogEntry(le.LogEvent));
                var result = await CaravanServiceProvider.LogRepository.AddEntriesAsync(CaravanCommonConfiguration.Instance.AppName, logEntries);

                if (!result.Succeeded)
                {
                    throw result.Exception;
                }
            }
            catch (Exception ex)
            {
                // Uso il log di emergenza nel caso ci siano stati errori.
                UseEmergencyLog(ex, ex.Message);
            }
        }

        protected override void FlushAsync(AsyncContinuation asyncContinuation)
        {
            // Nulla da fare...
        }

        private LogEntry ToLogEntry(LogEventInfo logEvent)
        {
            LogEntry logEntry;

            var caravanLogEvent = logEvent as CaravanLogger.LogEventInfo;
            if (caravanLogEvent != null)
            {
                // Innanzitutto, cerco di vedere se si tratta di un evento di log prodotto da
                // Caravan, cosa che dovrebbe avvenire nel 100% dei casi. Se si, applico un
                // meccanismo di log molto più efficiente, avendo già preparato il messaggio in precedenza.
                logEntry = caravanLogEvent.LogEntry;
            }
            else
            {
                // Se non lo è, cerco di generare comunque un messaggio abbozzato.
                logEntry = CaravanLogger.ToLogEntry(Mapper.Map<LogLevel>(logEvent.Level), new LogMessage
                {
                    ShortMessage = logEvent.FormattedMessage?.Trim(),
                    Exception = logEvent.Exception
                });
            }

            // Arricchisco la voce di log con ulteriori informazioni.
            logEntry.UserLogin = UserLogin.Render(logEvent);
            logEntry.CodeUnit = CodeUnit.Render(logEvent);
            logEntry.Function = Function.Render(logEvent);
            logEntry.Context = Context?.Render(logEvent) ?? logEntry.Context;

            return logEntry;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void UseEmergencyLog(Exception ex, string logMessage)
        {
            try
            {
                // Devo loggare immediatamente l'eccezione che è stata ricevuta. Cerco di salvare
                // comunque il messaggio di log appena emesso.
                CaravanServiceProvider.EmergencyLog.Error($"Internal error while logging [{logMessage}]", ex);
            }
            catch
            {
                // Se anche qui da errore, mi arrendo :(
            }
        }
    }
}

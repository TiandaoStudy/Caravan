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

using Finsa.Caravan.Common.Logging.Models;
using System;
using System.Activities;
using System.Collections.Generic;
using LL = Common.Logging.LogLevel;

namespace Finsa.Caravan.Common.Logging.Activities
{
    /// <summary>
    ///   Attività base per il log.
    /// </summary>
    /// <typeparam name="TLogActivity">Il tipo concreto che la implementerà.</typeparam>
    public abstract class AbstractLogActivity<TLogActivity> : CodeActivity
        where TLogActivity : AbstractLogActivity<TLogActivity>
    {
        /// <summary>
        ///   Il log su cui l'attività scriverà i propri messaggi.
        /// </summary>
        protected ICaravanLog Log { get; } = CaravanServiceProvider.FetchLog<TLogActivity>();

        #region Arguments

        /// <summary>
        ///   Eventuali parametri extra, opzionali.
        /// </summary>
        public InArgument<IList<KeyValuePair<string, object>>> Arguments { get; set; }

        /// <summary>
        ///   Il contesto, opzionale.
        /// </summary>
        public InArgument<string> Context { get; set; }

        /// <summary>
        ///   The exception that should be logged. It may not be specified.
        /// </summary>
        public InArgument<Exception> Exception { get; set; }

        /// <summary>
        ///   Il livello del log. Questo argomento è obbligatorio.
        /// </summary>
        [RequiredArgument]
        public InArgument<LL> LogLevel { get; set; }

        /// <summary>
        ///   Il messaggio lungo, opzionale.
        /// </summary>
        public InArgument<string> LongMessage { get; set; }

        /// <summary>
        ///   Il messaggio breve, opzionale. Questo parametro è obbligatorio nel caso la proprietà
        ///   <see cref="Exception"/> sia non valorizzata.
        /// </summary>
        public InArgument<string> ShortMessage { get; set; }

        #endregion Arguments

        /// <summary>
        ///   Scrive un messaggio nel log.
        /// </summary>
        /// <param name="context">Contesto dell'attività.</param>
        protected override void Execute(CodeActivityContext context)
        {
            var logLevel = LogLevel.Get(context);

            var isEnabled = false;
            switch (logLevel)
            {
                case LL.Trace:
                    isEnabled = Log.IsTraceEnabled;
                    break;

                case LL.Debug:
                    isEnabled = Log.IsDebugEnabled;
                    break;

                case LL.Info:
                    isEnabled = Log.IsInfoEnabled;
                    break;

                case LL.Warn:
                    isEnabled = Log.IsWarnEnabled;
                    break;

                case LL.Error:
                    isEnabled = Log.IsErrorEnabled;
                    break;

                case LL.Fatal:
                    isEnabled = Log.IsFatalEnabled;
                    break;
            }
            if (!isEnabled)
            {
                return;
            }

            // Preparazione del messaggio di log, condizionato dalla presenza di un'eccezione.
            LogMessage logMessage;
            var exception = Exception.Get(context);
            if (exception == null)
            {
                logMessage = new LogMessage
                {
                    ShortMessage = ShortMessage.Get(context),
                    LongMessage = LongMessage.Get(context),
                    Context = Context.Get(context),
                    Arguments = Arguments.Get(context)
                };
            }
            else
            {
                logMessage = new LogMessage
                {
                    Exception = exception,
                    Context = Context.Get(context),
                    Arguments = Arguments.Get(context)
                };
            }

            switch (logLevel)
            {
                case LL.Trace:
                    Log.Trace(logMessage);
                    break;

                case LL.Debug:
                    Log.Debug(logMessage);
                    break;

                case LL.Info:
                    Log.Info(logMessage);
                    break;

                case LL.Warn:
                    Log.Warn(logMessage);
                    break;

                case LL.Error:
                    Log.Error(logMessage);
                    break;

                case LL.Fatal:
                    Log.Fatal(logMessage);
                    break;
            }
        }
    }
}

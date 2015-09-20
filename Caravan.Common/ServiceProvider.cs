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

using System;
using Finsa.CodeServices.Clock;
using NLog.Targets;
using System.IO;
using Common.Logging;

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

        public static ILog EmLog { get; } = LogManager.GetLogger(typeof(ServiceProvider));

        /// <summary>
        ///   Il log di emergenza, basato rigorosamente su file system, che viene usato da Caravan quando il log normale, per qualche ragione, non sta funzionando. 
        /// 
        ///   Nel log di emergenza vengono solo registrati gli errori che stanno indicando il mal funzionamento del log, non i normali messaggi.
        /// </summary>
        public static FileTarget EmergencyLog { get; set; } = new FileTarget
        {
            // Basic
            AutoFlush = false,
            CreateDirs = true,
            Encoding = System.Text.Encoding.UTF8,
            FileName = CommonConfiguration.Instance.Logging_EmergencyLog_FileName,
            ForceManaged = false,
            Header = "### CARAVAN EMERGENCY LOG ###" + Environment.NewLine,
            Name = "caravan-emergency",

            // Archiving
            ArchiveAboveSize = CommonConfiguration.Instance.Logging_EmergencyLog_ArchiveAboveSizeInKB * 1024,
            ArchiveNumbering = ArchiveNumberingMode.DateAndSequence,
            ArchiveOldFileOnStartup = false,
            EnableArchiveFileCompression = true,
            MaxArchiveFiles = CommonConfiguration.Instance.Logging_EmergencyLog_MaxArchiveFiles,
            
            // Concurrency handling
            ConcurrentWrites = true,
            ConcurrentWriteAttempts = 10,
            ConcurrentWriteAttemptDelay = 3 // Milliseconds
        };
    }
}

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

using Finsa.Caravan.Common.BusinessModeling;
using PommaLabs.Thrower;
using System.Threading.Tasks;

namespace Finsa.Caravan.Common.Logging.Workflows
{
    /// <summary>
    ///   Flusso di lavoro che pulisce il repository dove sono memorizzati i log.
    /// </summary>
    public sealed class CleanUpLogRepository : Workflow<CleanUpLogRepository, CleanUpLogRepository.Input, CleanUpLogRepository.Output>
    {
        #region Dependency handling

        readonly ICaravanLogRepository _logRepository;

        /// <summary>
        ///   Inizializza le varie dipendenze.
        /// </summary>
        /// <param name="log">Il log.</param>
        /// <param name="logRepository">Il repository dei log.</param>
        public CleanUpLogRepository(ICaravanLog log, ICaravanLogRepository logRepository)
            : base(log)
        {
            RaiseArgumentNullException.IfIsNull(logRepository, nameof(logRepository));
            _logRepository = logRepository;
        }

        #endregion Dependency handling

        /// <summary>
        ///   Input del flusso di lavoro.
        /// </summary>
        public sealed class Input : IWorkflowInput
        {
        }

        /// <summary>
        ///   Output del flusso di lavoro.
        /// </summary>
        public sealed class Output : IWorkflowOutput
        {
        }

        /// <summary>
        ///   Il metodo che contiene la logica del flusso di lavoro.
        /// </summary>
        /// <param name="input">Istanza della classe di input.</param>
        /// <returns>Un task che restituirà un'istanza della classe di output.</returns>
        protected async override Task<Output> RunAsync(Input input)
        {
            await _logRepository.CleanUpEntriesAsync();
            return new Output();
        }
    }
}

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

using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Logging.Models;
using Finsa.CodeServices.Common;
using Finsa.CodeServices.Serialization;
using Ninject;
using PommaLabs.Thrower;
using System;
using System.Threading.Tasks;

namespace Finsa.Caravan.Common.BusinessModeling
{
    /// <summary>
    ///   Rappresenta un flusso di lavoro, un'unità logica che dovrebbe corrispondere a un processo
    ///   di business.
    /// </summary>
    /// <typeparam name="TWorkflow">La classe che implementa il flusso di lavoro.</typeparam>
    /// <typeparam name="TInput">La classe che rappresenta l'input del flusso di lavoro.</typeparam>
    /// <typeparam name="TOutput">La classe che rappresenta l'output del flusso di lavoro.</typeparam>
    public abstract class Workflow<TWorkflow, TInput, TOutput>
        where TWorkflow : Workflow<TWorkflow, TInput, TOutput>
        where TInput : class, IWorkflowInput
        where TOutput : class, IWorkflowOutput
    {
        const string WorkflowIdVariable = "workflow_id";

        /// <summary>
        ///   Imposta le proprietà radice del flusso di lavoro, tra cui il log e l'identificativo dell'istanza.
        /// </summary>
        /// <param name="log">Il log che sarà usato dall'istanza.</param>
        protected Workflow(ICaravanLog log)
        {
            RaiseArgumentNullException.IfIsNull(log, nameof(log));
            Log = log;
            WorkflowId = UniqueIdGenerator.NewBase32("-");
        }

        /// <summary>
        ///   Attiva il flusso di lavoro in modo sincrono.
        /// </summary>
        /// <param name="input">Istanza della classe di input.</param>
        /// <returns></returns>
        public static TOutput Invoke(TInput input)
        {
            try
            {
                return InvokeAsync(input).Result;
            }
            catch (AggregateException aggregate) when (aggregate.InnerExceptions.Count == 1)
            {
                // Se si tratta di un aggregato di eccezioni, cerco di capire quante siano. Se è
                // soltanto una, allora la rilancio, di modo che al di fuori arrivi direttamente
                // quell'eccezione e non l'aggregato. Altrimenti, non posso fare nulla e quindi uso
                // il filtro sopra per non toccare minimamente lo stacktrace.
                throw aggregate.InnerExceptions[0];
            }

            // Lascio correrre eventuali altre eccezioni, va bene così.
        }

        /// <summary>
        ///   Attiva il flusso di lavoro in modo asincrono.
        /// </summary>
        /// <param name="input">Istanza della classe di input.</param>
        /// <returns>Un task che restituirà un'istanza della classe di output.</returns>
        public static async Task<TOutput> InvokeAsync(TInput input)
        {
            TWorkflow workflow;

            // Separo la creazione del flusso dal resto della chiamata. Questo mi garantisce che,
            // nei log successivi, posso usare il GUID del flusso per identificare i messaggi.
            try
            {
                // Creo una nuova istanza del flusso, di modo che le venga anche assegnato un ID.
                // L'ID è comodo per loggare precisamente quale flusso abbia generato un dato messaggio.
                workflow = CaravanServiceProvider.NinjectKernel.Get<TWorkflow>();
            }
            catch (Exception ex)
            {
                // Devo registrare precisamente l'eccezione. TODO Produrre anche un alert? Non uso
                // l'istanza presente su workflow, perché probabilmente non esiste.
                CaravanServiceProvider.EmergencyLog.Fatal($"Creating an instance of workflow {typeof(TWorkflow).Name}", ex);

                // TODO Per ora, la rilancio.
                throw;
            }

            // Recupero l'ID del flusso padre.
            var parentWorkflowId = workflow.Log.ThreadVariablesContext.Get(WorkflowIdVariable);

            // Qui sotto eseguo veramente la chiamata al flusso.
            try
            {
                // Imposto l'ID del flusso tra le variabili legate al thread, sostituendo quello del padre.
                workflow.Log.ThreadVariablesContext.Set(WorkflowIdVariable, workflow.WorkflowId);

                // Registro l'ingresso all'interno del flusso.
                workflow.Log.Trace(new LogMessage
                {
                    ShortMessage = $"Workflow {typeof(TWorkflow).Name}@{workflow.WorkflowId} started",
                    LongMessage = input.ToJsonString(LogMessage.ReadableJsonSettings),
                    Context = "Workflow start - Input is logged into the long message"
                });

                // Eseguo il flusso e ne registro l'output.
                var output = await workflow.RunAsync(input);

                // Registro l'ingresso all'interno del flusso.
                workflow.Log.Trace(new LogMessage
                {
                    ShortMessage = $"Workflow {typeof(TWorkflow).Name}@{workflow.WorkflowId} ended",
                    LongMessage = output.ToJsonString(LogMessage.ReadableJsonSettings),
                    Context = "Workflow end - Output is logged into the long message"
                });

                return output;
            }
            catch (Exception ex) when (workflow.Log.Rethrowing(new LogMessage { Context = "Handling exception inside the workflow invoker", Exception = ex }))
            {
                // Non passerà mai di qui, il metodo di log restituisce sempre FALSE.
                return null;
            }
            finally
            {
                // In ogni caso, devo reimpostare tutte le variabili del thread a come le ho trovate.
                workflow.Log.ThreadVariablesContext.Set(WorkflowIdVariable, parentWorkflowId);
            }
        }

        /// <summary>
        ///   Il log usato dal flusso di lavoro.
        /// </summary>
        public ICaravanLog Log { get; }

        /// <summary>
        ///   Codice che identifica l'istanza del flusso di lavoro.
        /// </summary>
        public string WorkflowId { get; }

        /// <summary>
        ///   Il metodo che conterrà la logica del flusso di lavoro.
        /// </summary>
        /// <param name="input">Istanza della classe di input.</param>
        /// <returns>Un task che restituirà un'istanza della classe di output.</returns>
        protected abstract Task<TOutput> RunAsync(TInput input);
    }

    /// <summary>
    ///   Input dei flussi di lavoro.
    /// </summary>
    public interface IWorkflowInput
    {
    }

    /// <summary>
    ///   Output dei flussi di lavoro.
    /// </summary>
    public interface IWorkflowOutput
    {
    }
}

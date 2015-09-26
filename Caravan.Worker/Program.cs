using Finsa.Caravan.Worker.Tasks;
using System;
using System.Linq;

namespace Finsa.Caravan.Worker
{
    static class Program
    {
        static void Main(string[] args)
        {
            LoadTaskByName(nameof(CleanUpLogTable)).RunAsync().Wait();
        }

        static IAsyncTask LoadTaskByName(string taskName)
        {
            var taskType = (from t in typeof(Program).Assembly.GetTypes()
                            where string.Equals(t.Name, taskName, StringComparison.InvariantCultureIgnoreCase)
                            where t.GetInterface(typeof(IAsyncTask).Name) != null
                            select t).FirstOrDefault();

            if (taskType == null)
            {
                throw new InvalidOperationException($"No task can be mapped to given task name '{taskName}'");
            }

            return Activator.CreateInstance(taskType) as IAsyncTask;
        }
    }
}

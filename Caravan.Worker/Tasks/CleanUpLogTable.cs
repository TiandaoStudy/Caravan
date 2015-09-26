using Finsa.Caravan.DataAccess;
using System.Threading.Tasks;

namespace Finsa.Caravan.Worker.Tasks
{
    sealed class CleanUpLogTable : IAsyncTask
    {
        public async Task RunAsync()
        {
            await CaravanDataSource.Logger.CleanUpEntriesAsync();
        }
    }
}

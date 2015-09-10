using Finsa.Caravan.DataAccess;

namespace Finsa.Caravan.Worker.Tasks
{
    sealed class CleanUpLogTable : ITask
    {
        public void Run()
        {
            CaravanDataSource.Logger.CleanUpEntries();
        }
    }
}

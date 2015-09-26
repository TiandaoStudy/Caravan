using System.Threading.Tasks;

namespace Finsa.Caravan.Worker.Tasks
{
    interface IAsyncTask
    {
        Task RunAsync();
    }
}

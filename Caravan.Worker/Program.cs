using Finsa.Caravan.Common.Logging.Workflows;

namespace Caravan.Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            CleanUpLogRepository.Invoke(null);
        }
    }
}

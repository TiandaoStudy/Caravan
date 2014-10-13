using Nancy;

namespace Finsa.Caravan.RestService.Core
{
   public abstract class CaravanModule : NancyModule
   {
      protected CaravanModule()
      {
      }

      protected CaravanModule(string modulePath) : base(modulePath)
      {
      }
   }
}
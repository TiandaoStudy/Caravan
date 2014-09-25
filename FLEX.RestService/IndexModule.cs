using Nancy;

namespace FLEX.RestService
{
   public sealed class IndexModule : NancyModule
   {
      public IndexModule()
      {
         Get["/"] = parameters => View["index"];
      }
   }
}
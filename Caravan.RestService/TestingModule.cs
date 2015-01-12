using Finsa.Caravan.DataAccess;
using Finsa.Caravan.RestService.Core;
using Finsa.Caravan.RestService.Properties;

namespace Finsa.Caravan.RestService
{
   public sealed class TestingModule : CustomModule
   {
      private const string TestingNotAllowed = "Unit testing is not allowed. If you need it, please enable it in the REST service Web.config";

      public TestingModule() : base("testing")
      {
         Post["/clearAllTablesUseOnlyInsideUnitTestsPlease"] = _ =>
         {
            if (!Settings.Default.AllowUnitTesting)
            {
               return TestingNotAllowed;
            }
            Db.ClearAllTablesUseOnlyInsideUnitTestsPlease();
            return "Tables cleared!";
         };
      }
   }
}
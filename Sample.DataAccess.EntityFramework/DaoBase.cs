using System.Configuration;

namespace Sample.DataAccess.EntityFramework
{
   public abstract class DaoBase
   {
      protected static string ConnectionString
      {
         get { return ConfigurationManager.ConnectionStrings["NorthwindConnection"].ConnectionString; }
      }
   }
}
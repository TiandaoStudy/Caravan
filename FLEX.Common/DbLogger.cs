using System;
using System.IO;
using System.Text;
using FLEX.Common.Data;

namespace FLEX.Common
{
   public static class DbLogger
   {
      private static readonly IDbLogger CachedInstance = ServiceLocator.Load<IDbLogger>(Configuration.Instance.DbLoggerTypeInfo);

      public static IDbLogger Instance
      {
         get { return CachedInstance; }
      }
   }
}

using System;
using System.Web.UI;

namespace FLEX.Common.Web
{
   public interface IErrorManager
   {
      /// <summary>
      /// 
      /// </summary>
      /// <param name="ex"></param>
      /// <param name="onPage"></param>
      /// <returns></returns>
      Exception ElaborateException(Exception ex, Page onPage);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="ex"></param>
      /// <param name="onPage"></param>
      void LogException(Exception ex, Page onPage);
   }
}

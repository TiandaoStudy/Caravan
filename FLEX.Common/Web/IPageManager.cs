using System.Collections.Generic;

namespace FLEX.Common.Web
{
   /// <summary>
   /// 
   /// </summary>
   public interface IPageManager
   {
      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      IList<Pair<string, string>> GetFooterInfo();
   }
}

using System.Web.UI;
using FLEX.Common.Web;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Pages
// ReSharper restore CheckNamespace
{
   public abstract class PageBaseListAndSearch : PageBase
   {
      private readonly SearchCriteria _searchCriteria = new SearchCriteria();

      protected SearchCriteria SearchCriteria
      {
         get { return _searchCriteria; }
      }
   }
}
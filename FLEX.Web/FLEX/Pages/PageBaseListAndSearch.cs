using System;
using System.Diagnostics;
using System.Web.SessionState;
using FLEX.Common.Web;
using FLEX.Web.UserControls.Ajax;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Pages
// ReSharper restore CheckNamespace
{
   public abstract class PageBaseListAndSearch : PageBase
   {
      protected readonly SearchCriteria SearchCriteria = new SearchCriteria();

      //protected SearchCriteria SearchCriteria { get; private set; }

      //private const string StoreSearchCriteriaTag = "_STORE_SEARCH_CRITERIA";

      //private const string SearchCriteriaTag = "_SEARCH_CRITERIA";
      //protected readonly string PageId;

      //protected PageBaseListAndSearch(string newPageId)
      //{
      //   Load += Page_Load;
      //   PageId = newPageId;
      //}

      //private void Page_Load(object sender, EventArgs e)
      //{
      //   try
      //   {
      //      LoadSearchCriteria();
      //   }
      //   catch (Exception ex)
      //   {
      //      ErrorHandler.CatchException(ex, ErrorLocation.PageEvent);
      //   }
      //}

      //#region "Search Criteria"

      //protected abstract void FillSearchCriteria();

      ///// <summary>
      /////   Inizializza i criteri di ricerca.
      ///// </summary>
      ///// <param name="criteria"></param>
      ///// <remarks></remarks>
      //protected abstract void RegisterSearchCriteria(SearchCriteria criteria);

      //private void LoadSearchCriteria()
      //{
      //   dynamic storeSearchCriteriaObj = Session(PageId + StoreSearchCriteriaTag);

      //   var storeSearchCriteria = default(SearchCriteriaStorage);
      //   if (storeSearchCriteriaObj == null)
      //   {
      //      storeSearchCriteria = SearchCriteriaStorage.Discard;
      //   }
      //   else
      //   {
      //      storeSearchCriteria = (SearchCriteriaStorage) storeSearchCriteriaObj;
      //   }

      //   dynamic searchCriteriaObj = Session(PageId + SearchCriteriaTag);

      //   if (storeSearchCriteria == SearchCriteriaStorage.Discard || searchCriteriaObj == null)
      //   {
      //      SearchCriteria = new SearchCriteria();
      //   }
      //   else
      //   {
      //      SearchCriteria = (SearchCriteria) searchCriteriaObj;
      //      Session(PageId + StoreSearchCriteriaTag) = SearchCriteriaStorage.Discard;
      //   }

      //   Debug.Assert(SearchCriteria != null);
      //   Debug.Assert(SearchCriteria is SearchCriteria);

      //   Session[PageId + SearchCriteriaTag] = SearchCriteria;
      //   if (!IsPostBack)
      //   {
      //      FillSearchCriteria();
      //   }
      //   RegisterSearchCriteria(SearchCriteria);
      //}

      //public static void StoreSearchCriteriaForPage(HttpSessionState session, string pageIdToBeKept)
      //{
      //   session[pageIdToBeKept + StoreSearchCriteriaTag] = SearchCriteriaStorage.Keep;
      //}

      //#endregion

      ///// <summary>
      ///// 
      ///// </summary>
      //protected enum SearchCriteriaStorage
      //{
      //   /// <summary>
      //   /// 
      //   /// </summary>
      //   Keep,

      //   /// <summary>
      //   /// 
      //   /// </summary>
      //   Discard
      //}
   }
}
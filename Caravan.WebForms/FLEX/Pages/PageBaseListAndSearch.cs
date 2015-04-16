using System;
using System.Diagnostics;
using System.Web.SessionState;
using Finsa.Caravan.Common.WebForms;
using FLEX.Web.UserControls.Ajax;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Pages
// ReSharper restore CheckNamespace
{
   public abstract class PageBaseListAndSearch : PageBase
   {
      #region Constants

      private const string StoreSearchCriteriaTag = "STORE_SEARCH_CRITERIA_FOR_";
      private const string SearchCriteriaTag = "SEARCH_CRITERIA_FOR_";

      #endregion

      #region Instance Fields

      private SearchCriteria _searchCriteria;

      #endregion

      protected override void OnLoad(EventArgs args)
      {
         try
         {
            base.OnLoad(args);
            LoadSearchCriteria();
         }
         catch (Exception ex)
         {
            ErrorHandler.CatchException(ex, ErrorLocation.PageEvent);
         }
      }

      #region Search Criteria

      /// <summary>
      /// 
      /// </summary>
      protected SearchCriteria SearchCriteria
      {
         get
         {
            return _searchCriteria ?? LoadSearchCriteria();
         }
      }

      /// <summary>
      /// 
      /// </summary>
      protected abstract void FillSearchCriteria();

      /// <summary>
      ///   Inizializza i criteri di ricerca.
      /// </summary>
      /// <param name="criteria"></param>
      /// <remarks></remarks>
      protected abstract void RegisterSearchCriteria(SearchCriteria criteria);    

      /// <summary>
      /// 
      /// </summary>
      /// <param name="session"></param>
      /// <param name="pageIdToBeKept"></param>
      public static void StoreSearchCriteriaForPage(HttpSessionState session, string pageIdToBeKept)
      {
         session[StoreSearchCriteriaTag + pageIdToBeKept] = SearchCriteriaStorage.Keep;
      }

      #endregion

      #region Private Methods

      private SearchCriteria LoadSearchCriteria()
      {
         // Do not initialize criteria again, if they are ready.
         if (_searchCriteria != null)
         {
            return _searchCriteria;
         }

         // Locally cached for performance reasons.
         var flexID = FlexID;
         
         var storeSearchCriteriaObj = Session[StoreSearchCriteriaTag + flexID];

         SearchCriteriaStorage storeSearchCriteria;
         if (storeSearchCriteriaObj == null)
         {
            storeSearchCriteria = SearchCriteriaStorage.Discard;
         }
         else
         {
            storeSearchCriteria = (SearchCriteriaStorage) storeSearchCriteriaObj;
         }

         var searchCriteriaObj = Session[SearchCriteriaTag + flexID];

         if (storeSearchCriteria == SearchCriteriaStorage.Discard || searchCriteriaObj == null)
         {
            _searchCriteria = new SearchCriteria();
         }
         else
         {
            _searchCriteria = (SearchCriteria) searchCriteriaObj;
            Session[StoreSearchCriteriaTag + flexID] = SearchCriteriaStorage.Discard;
         }

         Debug.Assert(SearchCriteria != null);

         Session[SearchCriteriaTag + flexID] = SearchCriteria;
         if (!IsPostBack)
         {
            FillSearchCriteria();
         }
         RegisterSearchCriteria(SearchCriteria);

         return _searchCriteria;
      }

      #endregion

      /// <summary>
      /// 
      /// </summary>
      protected enum SearchCriteriaStorage : byte
      {
         /// <summary>
         /// 
         /// </summary>
         Discard = 0,

         /// <summary>
         /// 
         /// </summary>
         Keep = 1
      }
   }
}
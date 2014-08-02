﻿using System;
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
      #region Constants

      private const string StoreSearchCriteriaTag = "STORE_SEARCH_CRITERIA_FOR_";
      private const string SearchCriteriaTag = "SEARCH_CRITERIA_FOR_";

      #endregion

      protected PageBaseListAndSearch()
      {
         Load += Page_Load;
      }

      private void Page_Load(object sender, EventArgs e)
      {
         try
         {
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
      protected SearchCriteria SearchCriteria { get; private set; }

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

      private void LoadSearchCriteria()
      {
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
            SearchCriteria = new SearchCriteria();
         }
         else
         {
            SearchCriteria = (SearchCriteria) searchCriteriaObj;
            Session[StoreSearchCriteriaTag + flexID] = SearchCriteriaStorage.Discard;
         }

         Debug.Assert(SearchCriteria != null);

         Session[SearchCriteriaTag + flexID] = SearchCriteria;
         if (!IsPostBack)
         {
            FillSearchCriteria();
         }
         RegisterSearchCriteria(SearchCriteria);
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
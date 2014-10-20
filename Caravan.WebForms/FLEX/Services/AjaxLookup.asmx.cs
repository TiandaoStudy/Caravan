using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Xml;
using Dapper;
using Finsa.Caravan;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.Diagnostics;
using Finsa.Caravan.Extensions;
using Finsa.Caravan.Text;
using FLEX.Web.XmlSettings.AjaxLookup;
using FLEX.WebForms;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Services
// ReSharper restore CheckNamespace
{
   /// <summary>
   ///   Summary description for AjaxLookup
   /// </summary>
   [WebService(Namespace = "http://flex.finsa.com/")]
   [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
   [ToolboxItem(false)]
   [ScriptService]
   public class AjaxLookup : WebService
   {
      private const string DefaultResultCount = "10";
      private const string QueryFilterToken = "{:QueryFilter:}";
      private const string ResultCountToken = "{:ResultCount:}";
      private const string TokenEnd = ":}";
      private const string TokenStart = "{:";
      private const string UserQueryToken = "{:UserQuery:}";

      [WebMethod]
      public List<Result> Lookup(string xmlLookup, string lookupBy, string userQuery, string queryFilter)
      {
         try
         {
            return RetrieveData(xmlLookup, lookupBy, userQuery, queryFilter);
         }
         catch (Exception exc)
         {
            Db.Logger.LogError<AjaxLookup>(exc);
            return new List<Result> {new Result("ERR", "Service Error", exc.Message)};
         }
      }

      public static List<Result> RetrieveData(string xmlLookup, string lookupBy, string userQuery, string queryFilter)
      {
         var lookupData = LoadAjaxLookupData(xmlLookup, lookupBy);
         var lookupQuery = ProcessQuery(lookupData, userQuery, queryFilter);
         using (var resultData = Db.Manager.OpenConnection().Query(lookupQuery).ToDataTable())
         {
            return ProcessData(lookupData, resultData);
         }
      }

      #region AjaxLookup XML Handlers

      private static AjaxLookupDataLookupBy LoadAjaxLookupData(string xmlLookup, string lookupBy)
      {
         // At first, we create the relative path for our XML.
         var xmlPath = Path.Combine(WebSettings.AjaxLookup_XmlPath, xmlLookup + Constants.XmlExtension);

         // And then we make it absolute to our server.
         xmlPath = HttpContext.Current.Server.MapPath(xmlPath);

         // If cache contains an instance of the lookup data, we return it.
         AjaxLookupDataLookupBy lookupData;
         if (CacheManager.TryRetrieveValueForKey(out lookupData, xmlPath, lookupBy))
         {
            return lookupData;
         }

         using (var xmlStream = File.OpenRead(xmlPath))
         {
            try
            {
               lookupData = AjaxLookupData.DeserializeFrom(xmlStream).LookupBy.First(l => l.SuggestionField == lookupBy);
            }
            catch (Exception ex)
            {
               throw new XmlException("MSG", ex);
            }
            CheckAjaxLookupData(lookupData);
         }

         // We store the lookup data instance inside the cache, and then we return it.
         // We must pay attention to put the type of the lookup inside the key.
         CacheManager.StoreValueForKey(lookupData, xmlPath, lookupBy);
         return lookupData;
      }

      private static void CheckAjaxLookupData(AjaxLookupDataLookupBy lookupData)
      {
         Raise<XmlException>.IfIsEmpty(lookupData.DescriptionField);
         Raise<XmlException>.IfIsEmpty(lookupData.KeyField);
         Raise<XmlException>.IfIsEmpty(lookupData.SortField);
         Raise<XmlException>.IfIsEmpty(lookupData.SuggestionField);
         Raise<XmlException>.IfIsEmpty(lookupData.LookupQuery);
      }

      #endregion

      private static string ProcessQuery(AjaxLookupDataLookupBy lookupData, string userQuery, string queryFilter)
      {
         // Since we are probably handling an SQL query, we must escape single quotes in the userQuery parameter.
         userQuery = userQuery.Replace("'", "''");
         // Then, we take the XML file and we replace all inputs inside it.
         return new FastReplacer(TokenStart, TokenEnd)
            .Append(lookupData.LookupQuery)
            .Replace(UserQueryToken, userQuery)
            .Replace(QueryFilterToken, queryFilter)
            .Replace(ResultCountToken, DefaultResultCount)
            .ToString();
      }

      private static List<Result> ProcessData(AjaxLookupDataLookupBy lookupData, DataTable resultData)
      {
         resultData.DefaultView.Sort = lookupData.SortField;
         resultData.AcceptChanges();
         var resultList = new List<Result>();
         foreach (DataRow row in resultData.Rows)
         {
            var value = row[lookupData.KeyField].ToString();
            var label = row[lookupData.SuggestionField].ToString();
            var desc = row[lookupData.DescriptionField].ToString();
            resultList.Add(new Result(value, label, desc));
         }
         return resultList;
      }

      public sealed class Result
      {
         public readonly string Value;
         public readonly string Label;
         public readonly string Descr;

         public Result(string value, string label, string descr)
         {
            Value = value;
            Label = label;
            Descr = descr;
         }
      }
   }
}
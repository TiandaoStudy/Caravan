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
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.WebForms.Properties;
using PommaLabs.Thrower;
using Finsa.CodeServices.Common.Extensions;
using Finsa.CodeServices.Common.Text;
using FLEX.Web.XmlSettings.AjaxLookup;
using PommaLabs.KVLite;

// ReSharper disable CheckNamespace This is the correct namespace, despite the file physical position.

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
        private const string CachePartition = "Caravan.WebForms.AjaxLookupsXml";
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
                CaravanDataSource.Logger.LogError<AjaxLookup>(exc);
                return new List<Result> { new Result("ERR", "Service Error", exc.Message) };
            }
        }

        public static List<Result> RetrieveData(string xmlLookup, string lookupBy, string userQuery, string queryFilter)
        {
            var lookupData = LoadAjaxLookupData(xmlLookup, lookupBy);
            var lookupQuery = ProcessQuery(lookupData, userQuery, queryFilter);
            var resultData = CaravanDataSource.Manager.OpenConnection().Query(lookupQuery).ToList();
            if (resultData.Count == 0)
            {
                return new List<Result>();
            }
            using (var resultTable = resultData.ToDataTable())
            {
                return ProcessData(lookupData, resultTable);
            }
        }

        #region AjaxLookup XML Handlers

        private static AjaxLookupDataLookupBy LoadAjaxLookupData(string xmlLookup, string lookupBy)
        {
            // At first, we create the relative path for our XML.
            var xmlPath = Path.Combine(Settings.Default.AjaxLookupsXmlPath, xmlLookup + ".xml");

            // And then we make it absolute to our server.
            xmlPath = HttpContext.Current.Server.MapPath(xmlPath);

            // If cache contains an instance of the lookup data, we return it.
            var cachedLookupData = PersistentCache.DefaultInstance.Get<AjaxLookupDataLookupBy>(CachePartition, xmlPath);
            if (cachedLookupData.HasValue)
            {
                return cachedLookupData.Value;
            }

            AjaxLookupDataLookupBy lookupData;
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

            // We store the lookup data instance inside the cache, and then we return it. We must
            // pay attention to put the type of the lookup inside the key.
            PersistentCache.DefaultInstance.AddSliding(CachePartition, xmlPath, lookupData, Settings.Default.DefaultIntervalForVolatile);
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

        #endregion AjaxLookup XML Handlers

        private static string ProcessQuery(AjaxLookupDataLookupBy lookupData, string userQuery, string queryFilter)
        {
            // Since we are probably handling an SQL query, we must escape single quotes in the
            // userQuery parameter.
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

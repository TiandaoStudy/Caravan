using System;
using System.Data;
using System.IO;
using System.Web;
using System.Xml;
using Dapper;
using Finsa.Caravan;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.Diagnostics;
using Finsa.Caravan.Extensions;
using FLEX.Web.XmlSettings.Lookup;
using PommaLabs.KVLite;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Pages
// ReSharper restore CheckNamespace
{
   public partial class Lookup : PageBase
   {
      private const string CachePartition = "Caravan.WebForms.LookupsXml";

      protected void Page_Load(object sender, EventArgs e)
      {
      }

      public static DataTable RetrieveData(string lookup)
      {
         var lookupQuery = LoadLookupData(lookup).LookupQuery;
         return DataAccess.Manager.OpenConnection().Query(lookupQuery).ToDataTable();
      }

      #region Lookup XML Handlers

      private static LookupData LoadLookupData(string lookup)
      {
         // At first, we create the relative path for our XML.
         var xmlPath = Path.Combine(WebForms.Configuration.Instance.LookupsXmlPath, lookup + Constants.XmlExtension);

         // And then we make it absolute to our server.
         xmlPath = HttpContext.Current.Server.MapPath(xmlPath);

         // If cache contains an instance of the lookup data, we return it.
         var lookupData = VolatileCache.DefaultInstance[CachePartition, xmlPath] as LookupData;
         if (lookupData != null)
         {
            return lookupData;
         }

         using (var xmlStream = File.OpenRead(xmlPath))
         {
            try
            {
               lookupData = LookupData.DeserializeFrom(xmlStream);
            }
            catch (Exception ex)
            {
               throw new XmlException("MSG", ex);
            }
            CheckLookupData(lookupData);
         }

         // We store the lookup data instance inside the cache, and then we return it.
         VolatileCache.DefaultInstance.AddSliding(CachePartition, xmlPath, lookupData, WebForms.Configuration.DefaultIntervalForVolatile);
         return lookupData;
      }

      private static void CheckLookupData(LookupData lookupData)
      {
         // TODO Add other controls...
         Raise<XmlException>.IfIsEmpty(lookupData.LookupQuery);
      }

      #endregion
   }
}
﻿using System;
using System.Data;
using System.IO;
using System.Web;
using System.Xml;
using Dapper;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.WebForms.Properties;
using FLEX.Web.XmlSettings.Lookup;
using PommaLabs;
using PommaLabs.Diagnostics;
using PommaLabs.Extensions;
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
         return Db.Manager.OpenConnection().Query(lookupQuery).ToDataTable();
      }

      #region Lookup XML Handlers

      private static LookupData LoadLookupData(string lookup)
      {
         // At first, we create the relative path for our XML.
         var xmlPath = Path.Combine(Settings.Default.LookupsXmlPath, lookup + Constants.FileExtensions.Xml);

         // And then we make it absolute to our server.
         xmlPath = HttpContext.Current.Server.MapPath(xmlPath);

         // If cache contains an instance of the lookup data, we return it.
         var lookupData = Finsa.Caravan.Common.Properties.Settings.Default.DefaultCache.Get<LookupData>(CachePartition, xmlPath);
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
         Finsa.Caravan.Common.Properties.Settings.Default.DefaultCache.AddSliding(CachePartition, xmlPath, lookupData, Settings.Default.DefaultIntervalForVolatile);
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
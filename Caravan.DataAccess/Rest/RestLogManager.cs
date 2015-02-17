using System;
using System.Collections.Generic;
using Finsa.Caravan.Common.DataModel.Exceptions;
using Finsa.Caravan.Common.DataModel.Logging;
using Finsa.Caravan.Common.DataModel.Rest;
using Finsa.Caravan.DataAccess.Core;
using RestSharp;

namespace Finsa.Caravan.DataAccess.Rest
{
   public sealed class RestLogManager : LogManagerBase
   {
      public override LogResult LogRaw(LogType type, string appName, string userName, string codeUnit, string function, string shortMessage, string longMessage, string context, IEnumerable<KeyValuePair<string, string>> args)
      {
         var client = new RestClient("http://localhost/Caravan.RestService/security");
         var request = new RestRequest("{appName}/entries", Method.POST);

         request.AddUrlSegment("appName", appName);

         throw new NotImplementedException();

      }

      protected override IList<LogEntry> GetLogEntries(string appName, LogType? logType)
      {
         var client = new RestClient("http://localhost/Caravan.RestService/security");
         var request = new RestRequest("{appName}/entries/{logType}", Method.POST);
         throw new NotImplementedException();
      }

      protected override IList<LogSettings> GetLogSettings(string appName, LogType? logType)
      {
         try
         {
            var client = new RestClient("http://localhost/Caravan.RestService/security");
            var request = new RestRequest("{appName}/settings/{logType}", Method.POST);
           
            request.AddUrlSegment("appName", appName);

            if (logType != null)
            {
               request.AddUrlSegment("logType", logType.ToString()); //?????;
            }

            request.AddJsonBody(new RestRequest<object> {Auth = "AA", Body = new object()});

            var response = client.Execute<Common.DataModel.Rest.RestResponse<LogSettingsSingle>>(request);

            if (response.ErrorException != null)
            {
               if (response.ErrorMessage == AppNotFoundException.TheMessage)
                  throw new AppNotFoundException();
               throw new Exception(response.ErrorMessage);
            }
            var settings = new List<LogSettings> {response.Data.Body.Settings};

            return settings;
         }
         catch (Exception exception)
         {
            throw new Exception(exception.Message);
         }

         
      }

      protected override bool DoAddSettings(string appName, LogType logType, LogSettings settings)
      {
         try
         {
            var client = new RestClient("http://localhost/Caravan.RestService/security");
            var request = new RestRequest("{appName}/settings/{logType}", Method.PUT);

            request.AddUrlSegment("appName", appName);
            request.AddUrlSegment("logType", logType.ToString()); //?????;
            request.AddJsonBody(new RestRequest<LogSettingsSingle>
            {
               Auth = "AA",
               Body = new LogSettingsSingle
               {
                  Settings = new LogSettings
                  {
                     Days = settings.Days,
                     MaxEntries = settings.MaxEntries,
                     Enabled = settings.Enabled,
                     LogEntries = settings.LogEntries
                  }
               }
            });

            var response = client.Execute<Common.DataModel.Rest.RestResponse<LogSettingsSingle>>(request);

            if (response.ErrorException != null)
            {
               if (response.ErrorMessage == AppExistingException.TheMessage)
                  throw new AppExistingException();
               if(response.ErrorMessage == SettingsExistingException.TheMessage)
                  throw new SettingsExistingException();
               if (response.ErrorMessage == AppNotFoundException.TheMessage)
                  throw new AppNotFoundException();
               throw new Exception(response.ErrorMessage);
               
            }
            return true;
         }
         catch (Exception exception)
         {
            throw new Exception(exception.Message);
         }
      }

      protected override bool DoUpdateSettings(string appName, LogType logType, LogSettings settings)
      {
         try
         {
            var client = new RestClient("http://localhost/Caravan.RestService/security");
            var request = new RestRequest("{appName}/settings/{logType}", Method.PATCH);

            request.AddUrlSegment("appName", appName);
            request.AddUrlSegment("logType", logType.ToString()); //?????;
            request.AddJsonBody(new RestRequest<LogSettingsSingle>
            {
               Auth = "AA",
               Body = new LogSettingsSingle
               {
                  Settings = new LogSettings
                  {
                     Days = settings.Days,
                     MaxEntries = settings.MaxEntries,
                     Enabled = settings.Enabled,
                     LogEntries = settings.LogEntries
                  }
               }
            });

            var response = client.Execute<Common.DataModel.Rest.RestResponse<LogSettingsSingle>>(request);

            if (response.ErrorException != null)
            {
               if (response.ErrorMessage == AppExistingException.TheMessage)
                  throw new AppExistingException();
               if (response.ErrorMessage == SettingsExistingException.TheMessage)
                  throw new SettingsExistingException();
               if(response.ErrorMessage == AppNotFoundException.TheMessage)
                  throw new AppNotFoundException();
               throw new Exception(response.ErrorMessage);

            }
            return true;
         }
         catch (Exception exception)
         {
            throw new Exception(exception.Message);
         }
      }

      
       protected override bool DoDeleteSettings(string appName, LogType logType)
       {
           throw new NotImplementedException();
       }
   }
}

using Finsa.Caravan.Common.Models.Logging;
using Finsa.Caravan.Common.Models.Logging.Exceptions;
using Finsa.Caravan.Common.Models.Rest;
using Finsa.Caravan.Common.Models.Security.Exceptions;
using Finsa.Caravan.DataAccess.Core;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Finsa.Caravan.DataAccess.Rest
{
    internal sealed class RestLogManager : LogManagerBase
    {
        public override LogResult LogRaw(LogType logType, string appName, string userLogin, string codeUnit, string function, string shortMessage, string longMessage, string context, IEnumerable<KeyValuePair<string, string>> args)
        {
            var client = new RestClient("http://localhost/Caravan.RestService/security");
            var request = new RestRequest("{appName}/entries", Method.POST);

            request.AddUrlSegment("appName", appName);

            throw new NotImplementedException();
        }

        protected override IList<LogEntry> GetEntries(string appName, LogType? logType)
        {
            var client = new RestClient("http://localhost/Caravan.RestService/security");
            var request = new RestRequest("{appName}/entries/{logType}", Method.POST);
            throw new NotImplementedException();
        }

        protected override IList<LogSetting> GetSettings(string appName, LogType? logType)
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

                request.AddJsonBody(new RestRequest<object> { Auth = "AA", Body = new object() });

                var response = client.Execute<Common.Models.Rest.RestResponse<LogSetting>>(request);

                if (response.ErrorException != null)
                {
                    if (response.ErrorMessage == SecAppNotFoundException.TheMessage)
                        throw new SecAppNotFoundException();
                    throw new Exception(response.ErrorMessage);
                }
                var settings = new List<LogSetting> { response.Data.Body };

                return settings;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        protected override bool DoRemoveEntry(string appName, int logId)
        {
            throw new NotImplementedException();
        }

        protected override bool DoAddSetting(string appName, LogType logType, LogSetting setting)
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                var request = new RestRequest("{appName}/settings/{logType}", Method.PUT);

                request.AddUrlSegment("appName", appName);
                request.AddUrlSegment("logType", logType.ToString()); //?????;
                request.AddJsonBody(new RestRequest<LogSetting>
                {
                    Auth = "AA",
                    Body = new LogSetting
                       {
                           Days = setting.Days,
                           MaxEntries = setting.MaxEntries,
                           Enabled = setting.Enabled,
                           LogEntries = setting.LogEntries
                       }
                });

                var response = client.Execute<Common.Models.Rest.RestResponse<LogSetting>>(request);

                if (response.ErrorException != null)
                {
                    if (response.ErrorMessage == SecAppExistingException.TheMessage)
                        throw new SecAppExistingException();
                    if (response.ErrorMessage == LogSettingExistingException.TheMessage)
                        throw new LogSettingExistingException();
                    if (response.ErrorMessage == SecAppNotFoundException.TheMessage)
                        throw new SecAppNotFoundException();
                    throw new Exception(response.ErrorMessage);
                }
                return true;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        protected override bool DoUpdateSetting(string appName, LogType logType, LogSetting setting)
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                var request = new RestRequest("{appName}/settings/{logType}", Method.PATCH);

                request.AddUrlSegment("appName", appName);
                request.AddUrlSegment("logType", logType.ToString()); //?????;
                request.AddJsonBody(new RestRequest<LogSetting>
                {
                    Auth = "AA",
                    Body = new LogSetting
                       {
                           Days = setting.Days,
                           MaxEntries = setting.MaxEntries,
                           Enabled = setting.Enabled,
                           LogEntries = setting.LogEntries
                       }
                });

                var response = client.Execute<Common.Models.Rest.RestResponse<LogSetting>>(request);

                if (response.ErrorException != null)
                {
                    if (response.ErrorMessage == SecAppExistingException.TheMessage)
                        throw new SecAppExistingException();
                    if (response.ErrorMessage == LogSettingExistingException.TheMessage)
                        throw new LogSettingExistingException();
                    if (response.ErrorMessage == SecAppNotFoundException.TheMessage)
                        throw new SecAppNotFoundException();
                    throw new Exception(response.ErrorMessage);
                }
                return true;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        protected override bool DoRemoveSetting(string appName, LogType logType)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using Finsa.Caravan.Common.Models.Logging;
using Finsa.Caravan.Common.Models.Logging.Exceptions;
using Finsa.Caravan.Common.Models.Rest;
using Finsa.Caravan.Common.Models.Security.Exceptions;
using Finsa.Caravan.DataAccess.Core;
using RestSharp;
using Common.Logging;

namespace Finsa.Caravan.DataAccess.Drivers.Rest
{
    internal sealed class RestLogRepository : AbstractLogRepository<RestLogRepository>
    {
        protected override LogResult DoLogRaw(LogLevel logLevel, string appName, string userLogin, string codeUnit, string function, string shortMessage, string longMessage, string context, IEnumerable<KeyValuePair<string, string>> args)
        {
            var client = new RestClient("http://localhost/Caravan.RestService/security");
            var request = new RestRequest("{appName}/entries", Method.POST);

            request.AddUrlSegment("appName", appName);

            throw new NotImplementedException();
        }

        protected override IList<LogEntry> GetEntries(string appName, LogLevel? logLevel)
        {
            var client = new RestClient("http://localhost/Caravan.RestService/security");
            var request = new RestRequest("{appName}/entries/{logLevel}", Method.POST);
            throw new NotImplementedException();
        }

        protected override IList<LogSetting> GetSettings(string appName, LogLevel? logLevel)
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                var request = new RestRequest("{appName}/settings/{logLevel}", Method.POST);

                request.AddUrlSegment("appName", appName);

                if (logLevel != null)
                {
                    request.AddUrlSegment("logLevel", logLevel.ToString()); //?????;
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

        protected override bool DoAddSetting(string appName, LogLevel logLevel, LogSetting setting)
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                var request = new RestRequest("{appName}/settings/{logLevel}", Method.PUT);

                request.AddUrlSegment("appName", appName);
                request.AddUrlSegment("logLevel", logLevel.ToString()); //?????;
                request.AddJsonBody(new RestRequest<LogSetting>
                {
                    Auth = "AA",
                    Body = new LogSetting
                       {
                           Days = setting.Days,
                           MaxEntries = setting.MaxEntries,
                           Enabled = setting.Enabled,
                           //LogEntries = setting.LogEntries
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

        protected override bool DoUpdateSetting(string appName, LogLevel logLevel, LogSetting setting)
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                var request = new RestRequest("{appName}/settings/{logLevel}", Method.PATCH);

                request.AddUrlSegment("appName", appName);
                request.AddUrlSegment("logLevel", logLevel.ToString()); //?????;
                request.AddJsonBody(new RestRequest<LogSetting>
                {
                    Auth = "AA",
                    Body = new LogSetting
                       {
                           Days = setting.Days,
                           MaxEntries = setting.MaxEntries,
                           Enabled = setting.Enabled,
                           //LogEntries = setting.LogEntries
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

        protected override bool DoRemoveSetting(string appName, LogLevel logLevel)
        {
            throw new NotImplementedException();
        }
    }
}
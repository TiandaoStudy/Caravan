using System;
using System.Collections.Generic;
using System.Net;
using Finsa.Caravan.Common.Models.Logging.Exceptions;
using Finsa.Caravan.Common.Models.Rest;
using Finsa.Caravan.Common.Models.Security;
using Finsa.Caravan.Common.Models.Security.Exceptions;
using Finsa.Caravan.DataAccess.Core;
using RestSharp;

namespace Finsa.Caravan.DataAccess.Drivers.Rest
{
    internal sealed class RestSecurityRepository : SecurityRepositoryBase<RestSecurityRepository>
    {
        protected override IList<SecApp> GetApps()
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                var request = new RestRequest("", Method.POST);

                //request.AddUrlSegment("appName", appName);
                request.AddJsonBody(new RestRequest<object> { Auth = "AA", Body = new object() });

                var response = client.Execute<Common.Models.Rest.RestResponse<SecApp>>(request);

                var apps = new List<SecApp> { response.Data.Body };

                return apps;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        protected override SecApp GetApp(string appName)
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                var request = new RestRequest("{appName}", Method.POST);

                request.AddUrlSegment("appName", appName);
                request.AddJsonBody(new RestRequest<object> { Auth = "AA", Body = new object() });

                var response = client.Execute<Common.Models.Rest.RestResponse<SecApp>>(request);

                if (response.ErrorException != null)
                {
                    if (response.ErrorMessage == SecAppNotFoundException.TheMessage)
                        throw new SecAppNotFoundException(response.ErrorMessage);//verificare messaggio di output
                    throw new Exception(response.ErrorMessage);
                }

                return response.Data.Body;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        protected override bool DoAddApp(SecApp app)
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                var request = new RestRequest("", Method.PUT);

                request.AddJsonBody(new RestRequest<SecApp>
                {
                    Auth = "AA",
                    Body = new SecApp
                       {
                           Description = app.Description,
                           Name = app.Name,
                       }
                });

                var response = client.Execute<Common.Models.Rest.RestResponse<SecApp>>(request);
                if (response.ErrorMessage != null)
                {
                    if (response.ErrorMessage == SecAppExistingException.TheMessage)
                        throw new SecAppExistingException();
                    throw new Exception(response.ErrorMessage);
                }

                return true;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        protected override IList<SecGroup> GetGroups(string appName, string groupName)
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                RestRequest request;

                if (groupName != null)
                {
                    request = new RestRequest("{appName}/groups/{groupName}", Method.POST);
                    request.AddUrlSegment("appName", appName);
                    request.AddUrlSegment("groupName", groupName);
                }
                else
                {
                    request = new RestRequest("{appName}/groups", Method.POST);
                    request.AddUrlSegment("appName", appName);
                }

                request.AddJsonBody(new RestRequest<dynamic> { Auth = "AA", Body = new object() });

                var response = client.Execute<Common.Models.Rest.RestResponse<SecGroup>>(request);

                if (response.ErrorException != null)
                {
                    if (response.ErrorMessage == SecAppNotFoundException.TheMessage)
                        throw new SecAppNotFoundException();
                    throw new Exception(response.ErrorMessage);
                }

                var groups = new List<SecGroup> { response.Data.Body };

                return groups;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        protected override bool DoAddGroup(string appName, SecGroup newGroup)
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                var request = new RestRequest("{appName}/groups", Method.PUT);

                request.AddUrlSegment("appName", appName);
                request.AddJsonBody(new RestRequest<SecGroup>
                {
                    Auth = "AA",
                    Body = new SecGroup
                       {
                           Description = newGroup.Description,
                           Name = newGroup.Name
                       }
                });

                var response = client.Execute<Common.Models.Rest.RestResponse<SecGroup>>(request);
                if (response.ErrorException != null)
                {
                    if (response.ErrorMessage == SecAppNotFoundException.TheMessage)
                        throw new SecAppNotFoundException();
                    if (response.ErrorMessage == SecGroupExistingException.TheMessage)
                        throw new SecGroupExistingException();
                    throw new Exception(response.ErrorMessage);
                }

                return true;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        protected override bool DoRemoveGroup(string appName, string groupName)
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                var request = new RestRequest("{appName}/groups/{groupName}", Method.PATCH);

                request.AddUrlSegment("appName", appName);
                request.AddUrlSegment("groupName", groupName);
                request.AddJsonBody(new RestRequest<dynamic> { Auth = "AA", Body = new object() });

                var response = client.Execute<Common.Models.Rest.RestResponse<SecGroup>>(request);
                if (response.ErrorException != null)
                {
                    if (response.ErrorMessage == SecAppNotFoundException.TheMessage)
                        throw new SecAppNotFoundException();
                    if (response.ErrorMessage == SecGroupNotFoundException.TheMessage)
                        throw new SecGroupNotFoundException();
                    throw new Exception(response.ErrorMessage);
                }

                return true;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        protected override bool DoUpdateGroup(string appName, string groupName, SecGroup newGroup)
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                var request = new RestRequest("{appName}/groups/{groupName}", Method.PATCH);

                request.AddUrlSegment("appName", appName);
                request.AddUrlSegment("groupName", groupName);
                request.AddJsonBody(new RestRequest<SecGroup>
                {
                    Auth = "AA",
                    Body = new SecGroup
                       {
                           Description = newGroup.Description,
                           Name = newGroup.Name
                       }
                });

                var response = client.Execute<Common.Models.Rest.RestResponse<SecGroup>>(request);
                if (response.ErrorException != null)
                {
                    if (response.ErrorMessage == SecGroupExistingException.TheMessage)
                        throw new SecGroupExistingException();
                    if (response.ErrorMessage == SecGroupNotFoundException.TheMessage)
                        throw new SecGroupNotFoundException();
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

        protected override IList<SecUser> GetUsers(string appName, string userLogin)
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                RestRequest request;
                if (userLogin != null)
                {
                    request = new RestRequest("{appName}/users/{userLogin}", Method.POST);
                    request.AddUrlSegment("appName", appName);
                    request.AddUrlSegment("userLogin", userLogin);
                }
                else
                {
                    request = new RestRequest("{appName}/users", Method.POST);
                    request.AddUrlSegment("appName", appName);
                }
                request.AddJsonBody(new RestRequest<SecUser> { Auth = "AA", Body = null });
                var response = client.Execute<Common.Models.Rest.RestResponse<SecUser>>(request);

                if (response.ErrorException != null)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        if (response.ErrorMessage == SecAppNotFoundException.TheMessage)
                        {
                            throw new SecAppNotFoundException(); //verificare messaggio in output
                        }

                        if (response.ErrorMessage == SecUserNotFoundException.TheMessage)
                        {
                            throw new SecUserNotFoundException(); //verificare messaggio in output
                        }
                    }
                    throw new Exception(response.ErrorMessage);
                }
                var users = new List<SecUser> { response.Data.Body };
                return users;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        protected override bool DoAddUser(string appName, SecUser newUser)
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                var request = new RestRequest("{appName}/users", Method.PUT);

                request.AddUrlSegment("appName", appName);
                request.AddJsonBody(new RestRequest<SecUser>
                {
                    Auth = "AA",
                    Body = new SecUser
                       {
                           FirstName = newUser.FirstName,
                           LastName = newUser.LastName,
                           Email = newUser.Email,
                           Login = newUser.Login,
                           Active = newUser.Active
                       }
                });
                var response = client.Execute<Common.Models.Rest.RestResponse<SecUser>>(request);

                if (response.ErrorException != null)
                {
                    if (response.StatusCode == HttpStatusCode.Conflict)
                    {
                        throw new SecUserExistingException();
                    }
                    if (response.StatusCode == HttpStatusCode.NotFound)
                        throw new SecAppNotFoundException();
                    throw new Exception(response.ErrorMessage);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return true;
        }

        protected override bool DoRemoveUser(string appName, string userLogin)
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                var request = new RestRequest("{appName}/users/{userLogin}", Method.DELETE);

                request.AddUrlSegment("appName", appName);
                request.AddUrlSegment("userLogin", userLogin);

                request.AddJsonBody(new { Auth = "AA", Body = new object() });
                var response = client.Execute<Common.Models.Rest.RestResponse<dynamic>>(request);

                if (response.ErrorException != null)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                        if (response.ErrorMessage == SecAppNotFoundException.TheMessage)
                            throw new SecAppNotFoundException(response.ErrorMessage); //verificare messaggio in output
                    if (response.ErrorMessage == SecUserNotFoundException.TheMessage)
                        throw new SecUserNotFoundException(response.ErrorMessage); //verificare messaggio in output
                    throw new Exception(response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return true;
        }

        protected override bool DoUpdateUser(string appName, string userLogin, SecUser newUser)
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                var request = new RestRequest("{appName}/users/{userLogin}", Method.PATCH);

                request.AddUrlSegment("appName", appName);
                request.AddUrlSegment("userLogin", userLogin);
                request.AddJsonBody(new RestRequest<SecUser>
                {
                    Auth = "AA",
                    Body = new SecUser
                       {
                           FirstName = newUser.FirstName,
                           LastName = newUser.LastName,
                           Login = newUser.Login,
                           Email = newUser.Email
                       }
                });
                var response = client.Execute<Common.Models.Rest.RestResponse<SecUser>>(request);

                if (response.ErrorException != null)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        if (response.ErrorMessage == SecAppNotFoundException.TheMessage)
                        {
                            throw new SecAppNotFoundException(response.ErrorMessage); //verificare messaggio in output
                        }
                        if (response.ErrorMessage == SecUserNotFoundException.TheMessage)
                        {
                            throw new SecUserNotFoundException(response.ErrorMessage);
                        }
                    }
                    throw new Exception(response.ErrorMessage);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return true;
        }

        protected override bool DoAddUserToGroup(string appName, string userLogin, string groupName)
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                var request = new RestRequest("{appName}/users/{userLogin}/{groupName}", Method.PUT);

                request.AddUrlSegment("appName", appName);
                request.AddUrlSegment("userLogin", userLogin);
                request.AddUrlSegment("groupName", groupName);
                request.AddJsonBody(new RestRequest<dynamic> { Auth = "AA", Body = new object() });

                var response = client.Execute<Common.Models.Rest.RestResponse<SecUser>>(request);

                if (response.ErrorException != null)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                        throw new SecGroupNotFoundException();
                    if (response.StatusCode == HttpStatusCode.Conflict)
                        throw new SecUserExistingException();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return true;
        }

        protected override bool DoRemoveUserFromGroup(string appName, string userLogin, string groupName)
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                var request = new RestRequest("{appName}/users/{userLogin}/{groupName}", Method.DELETE);

                request.AddUrlSegment("appName", appName);
                request.AddUrlSegment("userLogin", userLogin);
                request.AddUrlSegment("groupName", groupName);
                request.AddJsonBody(new RestRequest<dynamic> { Auth = "AA", Body = new object() });

                var response = client.Execute<Common.Models.Rest.RestResponse<SecUser>>(request);
                if (response.ErrorException != null)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        if (response.ErrorMessage == SecUserNotFoundException.TheMessage)
                            throw new SecUserNotFoundException();
                        throw new SecGroupNotFoundException();
                    }
                    throw new Exception(response.ErrorMessage);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return true;
        }

        protected override IList<SecContext> GetContexts(string appName)
        {
            var client = new RestClient("http://localhost/Caravan.RestService/security");
            var request = new RestRequest("{appName}/contexts", Method.POST);

            request.AddUrlSegment("appName", appName);
            request.AddJsonBody(new RestRequest<dynamic> { Auth = "AA", Body = new object() });

            var response = client.Execute<Common.Models.Rest.RestResponse<SecContext>>(request);

            if (response.ErrorException != null)
            {
                if (response.ErrorMessage == SecAppNotFoundException.TheMessage)
                    throw new SecAppNotFoundException();
                throw new Exception(response.ErrorMessage);
            }

            var contexts = new List<SecContext> { response.Data.Body };

            return contexts;
        }

        protected override IList<SecObject> GetObjects(string appName, string contextName)
        {
            var client = new RestClient("http://localhost/Caravan.RestService/security");
            var request = new RestRequest("{appName}/objects", Method.POST);

            request.AddUrlSegment("appName", appName);
            request.AddJsonBody(new RestRequest<dynamic> { Auth = "AA", Body = new object() });

            var response = client.Execute<Common.Models.Rest.RestResponse<SecObject>>(request);

            var objs = new List<SecObject> { response.Data.Body };

            return objs;
        }

        #region Entries

        protected override IList<SecEntry> GetEntries(string appName, string contextName, string objectName, string userLogin)
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                IRestRequest request;

                if (objectName == null)
                {
                    request = new RestRequest("{appName}/entries/{contextName}/", Method.POST);
                    request.AddUrlSegment("appName", appName);
                    request.AddUrlSegment("contextName", contextName);
                }
                else
                {
                    request = new RestRequest("{appName}/entries/{contextName}/{objectName}", Method.POST);
                    request.AddUrlSegment("appName", appName);
                    request.AddUrlSegment("contextName", contextName);
                    request.AddUrlSegment("objectName", objectName);
                }

                request.AddJsonBody(new RestRequest<dynamic> { Auth = "AA", Body = new object() });

                var response = client.Execute<Common.Models.Rest.RestResponse<SecEntry>>(request);

                if (response.ErrorException != null)
                {
                    if (response.ErrorMessage == SecAppNotFoundException.TheMessage)
                        throw new SecAppNotFoundException();
                    throw new Exception(response.ErrorMessage);
                }

                var entries = new List<SecEntry> { response.Data.Body };

                return entries;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        protected override bool DoAddEntry(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName)
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                var request = new RestRequest("{appName}/entries", Method.PUT);

                request.AddUrlSegment("appName", appName);
                //if (userLogin != null)
                //{
                //    request.AddJsonBody(new RestRequest<SecEntry>
                //    {
                //        Auth = "AA",
                //        Body = new SecEntry { Context = secContext, Object = secObject, User = new SecUser { Login = userLogin } }
                //    });
                //}
                //else
                //{
                //    request.AddJsonBody(new RestRequest<SecEntry>
                //    {
                //        Auth = "AA",
                //        Body = new SecEntry { Context = secContext, Object = secObject, Group = new SecGroup { Name = groupName } }
                //    });
                //}

                var response = client.Execute<Common.Models.Rest.RestResponse<SecEntry>>(request);

                if (response.ErrorException != null)
                {
                    if (response.ErrorMessage == SecAppNotFoundException.TheMessage)
                        throw new SecAppNotFoundException();
                    if (response.ErrorMessage == LogEntryExistingException.TheMessage)
                        throw new LogEntryExistingException();
                    throw new Exception(response.ErrorMessage);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

            return true;
        }

        protected override bool DoRemoveEntry(string appName, string contextName, string objectName, string userLogin, string groupName)
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                var request = new RestRequest("{appName}/entries/{contextName}/{objectName}", Method.DELETE);

                request.AddUrlSegment("appName", appName);
                request.AddUrlSegment("contextName", contextName);
                request.AddUrlSegment("objectName", objectName);

                //if (userLogin != null)
                //{
                //    request.AddJsonBody(new RestRequest<SecEntry>
                //    {
                //        Auth = "AA",
                //        Body =  new SecEntry
                //            {
                //                User = new SecUser { Login = userLogin }
                //            }
                //    });
                //}
                //else
                //{
                //    request.AddJsonBody(new RestRequest<SecEntry>
                //    {
                //        Auth = "AA",
                //        Body = new SecEntry
                //            {
                //                Group = new SecGroup { Name = groupName }
                //            }
                //    });
                //}

                var response = client.Execute<Common.Models.Rest.RestResponse<SecEntry>>(request);
                if (response.ErrorException != null)
                {
                    if (response.ErrorMessage == SecAppNotFoundException.TheMessage)
                    {
                        throw new SecAppNotFoundException();
                    }
                    if (response.ErrorMessage == LogEntryExistingException.TheMessage)
                    {
                        throw new LogEntryExistingException();
                    }
                    throw new Exception(response.ErrorMessage);
                }
                //try
                //{
                //   var entry = JsonConvert.DeserializeObject<Common.DataModel.Rest.RestResponse<SecEntrySingle>>(response);
                //}
                //catch
                //{
                //   var error = JsonConvert.DeserializeObject<Common.DataModel.Rest.RestResponse<FailureBody>>(response);
                //   if (error != null && error.Body.Exception != null)
                //   {
                //      throw error.Body.Exception;
                //   }
                //   throw new Exception("BOH");
                //}
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return true;
        }

        #endregion Entries
    }
}
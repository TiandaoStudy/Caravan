
using Finsa.Caravan.DataAccess.Core;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;
using Finsa.Caravan.Common.Logging.Exceptions;
using Finsa.Caravan.Common.Security.Exceptions;
using Finsa.Caravan.Common.Security.Models;
using Finsa.Caravan.DataAccess.Rest.Models;

namespace Finsa.Caravan.DataAccess.Rest
{
    internal sealed class RestSecurityRepository : AbstractSecurityRepository<RestSecurityRepository>
    {
        protected override async Task<SecApp[]> GetAppsAsyncInternal()
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                var request = new RestRequest("", Method.POST);

                //request.AddUrlSegment("appName", appName);
                request.AddJsonBody(new RestRequest<object> { Auth = "AA", Body = new object() });

                var response = await client.ExecuteTaskAsync<Models.RestResponse<SecApp>>(request);

                var apps = new[] { response.Data.Body };

                return apps;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        protected override async Task<SecApp> GetAppAsyncInternal(string appName)
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                var request = new RestRequest("{appName}", Method.POST);

                request.AddUrlSegment("appName", appName);
                request.AddJsonBody(new RestRequest<object> { Auth = "AA", Body = new object() });

                var response = await client.ExecuteTaskAsync<Models.RestResponse<SecApp>>(request);

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

        protected override bool AddAppAsyncInternal(SecApp app)
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

                var response = client.Execute<Models.RestResponse<SecApp>>(request);
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

        protected override SecGroup[] GetGroupsInternal(string appName, string groupName)
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

                var response = client.Execute<Models.RestResponse<SecGroup>>(request);

                if (response.ErrorException != null)
                {
                    if (response.ErrorMessage == SecAppNotFoundException.TheMessage)
                        throw new SecAppNotFoundException();
                    throw new Exception(response.ErrorMessage);
                }

                var groups = new[] { response.Data.Body };

                return groups;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        protected override bool AddGroupInternal(string appName, SecGroup newGroup)
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

                var response = client.Execute<Models.RestResponse<SecGroup>>(request);
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

        protected override bool RemoveGroupInternal(string appName, string groupName)
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                var request = new RestRequest("{appName}/groups/{groupName}", Method.PATCH);

                request.AddUrlSegment("appName", appName);
                request.AddUrlSegment("groupName", groupName);
                request.AddJsonBody(new RestRequest<dynamic> { Auth = "AA", Body = new object() });

                var response = client.Execute<Models.RestResponse<SecGroup>>(request);
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

        protected override bool UpdateGroupInternal(string appName, string groupName, SecGroupUpdates groupUpdates)
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                var request = new RestRequest("{appName}/groups/{groupName}", Method.PATCH);

                request.AddUrlSegment("appName", appName);
                request.AddUrlSegment("groupName", groupName);
                request.AddJsonBody(new RestRequest<SecGroupUpdates>
                {
                    Auth = "AA",
                    Body = groupUpdates
                });

                var response = client.Execute<Models.RestResponse<SecGroup>>(request);
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

        protected override SecUser[] GetUsersInternal(string appName, string userLogin, string userEmail)
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
                var response = client.Execute<Models.RestResponse<SecUser>>(request);

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
                var users = new[] { response.Data.Body };
                return users;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        protected override bool AddUserInternal(string appName, SecUser newUser)
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
                var response = client.Execute<Models.RestResponse<SecUser>>(request);

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

        protected override bool RemoveUserInternal(string appName, string userLogin)
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                var request = new RestRequest("{appName}/users/{userLogin}", Method.DELETE);

                request.AddUrlSegment("appName", appName);
                request.AddUrlSegment("userLogin", userLogin);

                request.AddJsonBody(new { Auth = "AA", Body = new object() });
                var response = client.Execute<Models.RestResponse<dynamic>>(request);

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

        protected override bool UpdateUserInternal(string appName, string userLogin, SecUserUpdates userUpdates)
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                var request = new RestRequest("{appName}/users/{userLogin}", Method.PATCH);

                request.AddUrlSegment("appName", appName);
                request.AddUrlSegment("userLogin", userLogin);
                request.AddJsonBody(new RestRequest<SecUserUpdates>
                {
                    Auth = "AA",
                    Body = userUpdates
                });
                var response = client.Execute<Models.RestResponse<SecUser>>(request);

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

        protected override bool AddUserToGroupInternal(string appName, string userLogin, string groupName)
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                var request = new RestRequest("{appName}/users/{userLogin}/{groupName}", Method.PUT);

                request.AddUrlSegment("appName", appName);
                request.AddUrlSegment("userLogin", userLogin);
                request.AddUrlSegment("groupName", groupName);
                request.AddJsonBody(new RestRequest<dynamic> { Auth = "AA", Body = new object() });

                var response = client.Execute<Models.RestResponse<SecUser>>(request);

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

        protected override bool RemoveUserFromGroupInternal(string appName, string userLogin, string groupName)
        {
            try
            {
                var client = new RestClient("http://localhost/Caravan.RestService/security");
                var request = new RestRequest("{appName}/users/{userLogin}/{groupName}", Method.DELETE);

                request.AddUrlSegment("appName", appName);
                request.AddUrlSegment("userLogin", userLogin);
                request.AddUrlSegment("groupName", groupName);
                request.AddJsonBody(new RestRequest<dynamic> { Auth = "AA", Body = new object() });

                var response = client.Execute<Models.RestResponse<SecUser>>(request);
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

        protected override SecContext[] GetContextsInternal(string appName)
        {
            var client = new RestClient("http://localhost/Caravan.RestService/security");
            var request = new RestRequest("{appName}/contexts", Method.POST);

            request.AddUrlSegment("appName", appName);
            request.AddJsonBody(new RestRequest<dynamic> { Auth = "AA", Body = new object() });

            var response = client.Execute<Models.RestResponse<SecContext>>(request);

            if (response.ErrorException != null)
            {
                if (response.ErrorMessage == SecAppNotFoundException.TheMessage)
                    throw new SecAppNotFoundException();
                throw new Exception(response.ErrorMessage);
            }

            var contexts = new[] { response.Data.Body };

            return contexts;
        }

        protected override SecObject[] GetObjectsInternal(string appName, string contextName)
        {
            var client = new RestClient("http://localhost/Caravan.RestService/security");
            var request = new RestRequest("{appName}/objects", Method.POST);

            request.AddUrlSegment("appName", appName);
            request.AddJsonBody(new RestRequest<dynamic> { Auth = "AA", Body = new object() });

            var response = client.Execute<Models.RestResponse<SecObject>>(request);

            var objs = new[] { response.Data.Body };

            return objs;
        }

        #region Entries

        protected override SecEntry[] GetEntriesInternal(string appName, string contextName, string objectName, string userLogin)
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

                var response = client.Execute<Models.RestResponse<SecEntry>>(request);

                if (response.ErrorException != null)
                {
                    if (response.ErrorMessage == SecAppNotFoundException.TheMessage)
                        throw new SecAppNotFoundException();
                    throw new Exception(response.ErrorMessage);
                }

                var entries = new[] { response.Data.Body };

                return entries;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        protected override bool AddEntryInternal(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName)
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

                var response = client.Execute<Models.RestResponse<SecEntry>>(request);

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

        protected override bool RemoveEntryInternal(string appName, string contextName, string objectName, string userLogin, string groupName)
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

                var response = client.Execute<Models.RestResponse<SecEntry>>(request);
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
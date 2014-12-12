using System;
using System.Collections.Generic;
using System.Net;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataModel.Exceptions;
using Finsa.Caravan.DataModel.Rest;
using Finsa.Caravan.DataModel.Security;
using Newtonsoft.Json;
using RestSharp;

namespace Finsa.Caravan.DataAccess.Rest
{
   public sealed class RestSecurityManager : SecurityManagerBase<RestSecurityManager>
   {
      private static readonly object DummyBody = new object();

      protected override IList<SecApp> GetApps()
      {
         var client = CreateClient();
         var request = new RestRequest("", Method.POST);

         request.AddJsonBody(new RestRequest<object>
         {
            Auth = Db.RestAuthObject,
            Body = DummyBody
         });

         var response = client.Execute<DataModel.Rest.RestResponse<SecAppSingle>>(request);

         var apps = new List<SecApp> {response.Data.Body.App};

         return apps;
      }

      protected override SecApp GetApp(string appName)
      {
         var client = CreateClient();
         var request = new RestRequest("{appName}", Method.POST);
         
         request.AddUrlSegment("appName", appName);
         request.AddJsonBody(new RestRequest<object>
         {
            Auth = Db.RestAuthObject,
            Body = DummyBody
         });

         var response = client.Execute<DataModel.Rest.RestResponse<SecAppSingle>>(request);

         if (response.ErrorException != null)
         {
            if (response.ErrorMessage == AppNotFoundException.TheMessage)
               throw new AppNotFoundException(response.ErrorMessage);//verificare messaggio di output
            throw new Exception("verify the URL address");
         }
         
         return response.Data.Body.App;
      }


      protected override bool DoAddApp(SecApp app)
      {
         var client = CreateClient();
         var request = new RestRequest("", Method.PUT);

         try
         {
            request.AddJsonBody(new RestRequest<SecAppSingle>
            {
               Auth = Db.RestAuthObject,
               Body = new SecAppSingle
               {
                  App = new SecApp
                  {
                     Description = app.Description,
                     Name = app.Name,
                  }
               }
            });

            var response = client.Execute<DataModel.Rest.RestResponse<SecAppSingle>>(request);
         }
         catch (AppExistingException e)
         {
            throw new Exception(e.Message);
         }

         return true;
      }

      protected override IList<SecGroup> GetGroups(string appName, string groupName)
      {
         var client = CreateClient();
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

         request.AddJsonBody(new RestRequest<dynamic>
         {
            Auth = Db.RestAuthObject, 
            Body = new object()
         });

         var response = client.Execute<DataModel.Rest.RestResponse<SecGroupSingle>>(request);

         if (response.ErrorException != null)
         {
            if(response.ErrorMessage == AppNotFoundException.TheMessage)
               throw new AppNotFoundException();
            throw new Exception(response.ErrorMessage);
         }

         var groups = new List<SecGroup> {response.Data.Body.Group};

         return groups;
      }

      protected override bool DoAddGroup(string appName, SecGroup newGroup)
      {
         var client = CreateClient();
         var request = new RestRequest("{appName}/groups", Method.PUT);
         try
         {
            request.AddUrlSegment("appName", appName);
            request.AddJsonBody(new RestRequest<SecGroupSingle>
            {
               Auth = Db.RestAuthObject,
               Body = new SecGroupSingle
               {
                  Group = new SecGroup
                  {
                     Description = newGroup.Description,
                     Name = newGroup.Name,
                     IsAdmin = newGroup.IsAdmin
                  }
               }
            });
         }
         catch (AppNotFoundException e)
         {
            throw new Exception(e.Message);
         }
         catch (GroupExistingException e)
         {
            throw new Exception(e.Message);
         }

         client.Execute<DataModel.Rest.RestResponse<SecGroupSingle>>(request);
         return true;
      }

      protected override bool DoRemoveGroup(string appName, string groupName)
      {
         var client = CreateClient();
         var request = new RestRequest("{appName}/groups/{groupName}", Method.PATCH);
         try
         {
            request.AddUrlSegment("appName", appName);
            request.AddUrlSegment("groupName", groupName);
            request.AddJsonBody(new RestRequest<dynamic>
            {
               Auth = Db.RestAuthObject, 
               Body = DummyBody
            });
         }
         catch (AppNotFoundException e)
         {
            throw new Exception(e.Message);
         }
         catch (GroupNotFoundException e)
         {
            throw new Exception(e.Message);
         }

         client.Execute<DataModel.Rest.RestResponse<SecGroupSingle>>(request);
         return true;
      }

      protected override bool DoUpdateGroup(string appName, string groupName, SecGroup newGroup)
      {
         var client = CreateClient();
         var request = new RestRequest("{appName}/groups/{groupName}", Method.PATCH);
         try
         {
            request.AddUrlSegment("appName", appName);
            request.AddUrlSegment("groupName", groupName);
            request.AddJsonBody(new RestRequest<SecGroupSingle>
            {
               Auth = Db.RestAuthObject,
               Body = new SecGroupSingle
               {
                  Group = new SecGroup
                  {
                     Description = newGroup.Description,
                     Name = newGroup.Name,
                     IsAdmin = newGroup.IsAdmin
                  }
               }
            });
         }
         catch (AppNotFoundException e)
         {
            throw new Exception(e.Message);
         }

         catch (GroupExistingException e)
         {
            throw new Exception(e.Message);
         }

         client.Execute<DataModel.Rest.RestResponse<SecGroupSingle>>(request);
         return true;
      }

      protected override IList<SecUser> GetUsers(string appName, string userLogin)
      {
         var client = CreateClient();
         try
         {
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
            request.AddJsonBody(new RestRequest<SecUserSingle> { Auth = "AA", Body = null });
            var response = client.Execute<DataModel.Rest.RestResponse<SecUserSingle>>(request);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
               if (response.ErrorMessage == AppNotFoundException.TheMessage)
               {
                  throw new AppNotFoundException(response.ErrorMessage);//verificare messaggio in output
               }

               if (response.ErrorMessage == UserNotFoundException.TheMessage)
               {
                  throw new UserNotFoundException(response.ErrorMessage);//verificare messaggio in output
               }
            }
            var users = new List<SecUser> { response.Data.Body.User };
            return users;
         }
         catch (Exception e)
         {
            throw new Exception(e.Message);
         }
         
         
      }

      protected override bool DoAddUser(string appName, SecUser newUser)
      {
         var client = CreateClient();
         var request = new RestRequest("{appName}/users", Method.PUT);
         try
         {
            request.AddUrlSegment("appName", appName);

            request.AddJsonBody(new RestRequest<SecUserSingle>
            {
               Auth = Db.RestAuthObject,
               Body = new SecUserSingle
               {
                  User = new SecUser
                  {
                     FirstName = newUser.FirstName,
                     LastName = newUser.LastName,
                     Email = newUser.Email,
                     Login = newUser.Login,
                     Active = newUser.Active
                  }
               }
            });
            var response = client.Execute<DataModel.Rest.RestResponse<SecUserSingle>>(request);

            if (response.StatusCode == HttpStatusCode.Conflict)
            {
               throw new UserExistingException(UserExistingException.TheMessage);
            }
            if(response.StatusCode == HttpStatusCode.NotFound)
               throw new AppNotFoundException(AppNotFoundException.TheMessage);
            //if (response.StatusCode == HttpStatusCode.MethodNotAllowed)
            //{
            //   throw new Exception("verify URL address or the body of the request");
            //}

         }
         catch (Exception e)
         {
            throw new Exception(e.Message);
         }
         
         return true;
      }

      protected override bool DoRemoveUser(string appName, string userLogin)
      {
         var client = CreateClient();
         var request = new RestRequest("{appName}/users/{userLogin}", Method.DELETE);
         try
         {
            request.AddUrlSegment("appName", appName);
            request.AddUrlSegment("userLogin", userLogin);

            request.AddJsonBody(new
            {
               Auth = Db.RestAuthObject, 
               Body = DummyBody
            });
            var response = client.Execute<DataModel.Rest.RestResponse<dynamic>>(request);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
               if (response.ErrorMessage == AppNotFoundException.TheMessage)
               {
                  throw new AppNotFoundException(response.ErrorMessage); //verificare messaggio in output
               }
               if (response.ErrorMessage == UserNotFoundException.TheMessage)
               {
                  throw new UserNotFoundException(response.ErrorMessage);//verificare messaggio in output
               }
            }
         }
         catch (Exception e)
         {
            throw new Exception(e.Message);
         }
        
         return true;
      }

      protected override bool DoUpdateUser(string appName, string userLogin, SecUser newUser)
      {
         var client = CreateClient();
         var request = new RestRequest("{appName}/users/{userLogin}", Method.PATCH);
         try
         {
            request.AddUrlSegment("appName", appName);
            request.AddUrlSegment("userLogin", userLogin);
            request.AddJsonBody(new RestRequest<SecUserSingle>
            {
               Auth = Db.RestAuthObject,
               Body = new SecUserSingle
               {
                  User = new SecUser
                  {
                     FirstName = newUser.FirstName,
                     LastName = newUser.LastName,
                     Login = newUser.Login,
                     Email = newUser.Email
                  }
               }
            });
            var response = client.Execute<DataModel.Rest.RestResponse<SecUserSingle>>(request);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
               if (response.ErrorMessage == AppNotFoundException.TheMessage)
               {
                  throw new AppNotFoundException(response.ErrorMessage); //verificare messaggio in output
               }
               if (response.ErrorMessage == UserNotFoundException.TheMessage)
               {
                  throw new UserNotFoundException(response.ErrorMessage);
               }
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
         var client = CreateClient();
         var request = new RestRequest("{appName}/users/{userLogin}/{groupName}", Method.PUT);
         try
         {
            request.AddUrlSegment("appName", appName);
            request.AddUrlSegment("userLogin", userLogin);
            request.AddUrlSegment("groupName", groupName);
            request.AddJsonBody(new RestRequest<dynamic> {Auth = "AA", Body = new object()});

            var response = client.Execute<DataModel.Rest.RestResponse<SecUserSingle>>(request);
         }
         catch (UserExistingException e)
         {
            throw new Exception(e.Message);
         }
         catch (GroupNotFoundException e)
         {
            throw new Exception(e.Message);
         }

         return true;
      }

      protected override bool DoRemoveUserFromGroup(string appName, string userLogin, string groupName)
      {
         var client = CreateClient();
         var request = new RestRequest("{appName}/users/{userLogin}/{groupName}", Method.DELETE);
         try
         {
            request.AddUrlSegment("appName", appName);
            request.AddUrlSegment("userLogin", userLogin);
            request.AddUrlSegment("groupName", groupName);
            request.AddJsonBody(new RestRequest<dynamic>
            {
               Auth = Db.RestAuthObject, 
               Body = DummyBody
            });

            var response = client.Execute<DataModel.Rest.RestResponse<SecUserSingle>>(request);
         }
         catch (UserNotFoundException e)
         {
            throw new Exception(e.Message);
         }
         catch (GroupNotFoundException e)
         {
            throw new Exception(e.Message);
         }

         return true;
      }

      protected override IList<SecContext> GetContexts(string appName)
      {
         var client = CreateClient();
         var request = new RestRequest("{appName}/contexts", Method.POST);

         request.AddUrlSegment("appName", appName);
         request.AddJsonBody(new RestRequest<dynamic>
         {
            Auth = Db.RestAuthObject, 
            Body = DummyBody
         });

         var response = client.Execute<DataModel.Rest.RestResponse<SecContextSingle>>(request);

         if (response.ErrorException != null)
         {
            if(response.ErrorMessage == AppNotFoundException.TheMessage)
               throw new AppNotFoundException();
            throw new Exception(response.ErrorMessage);
         }

         var contexts = new List<SecContext> {response.Data.Body.Context};

         return contexts;
      }

      protected override IList<SecObject> GetObjects(string appName, string contextName)
      {
         var client = CreateClient();
         var request = new RestRequest("{appName}/objects", Method.POST);

         request.AddUrlSegment("appName", appName);
         request.AddJsonBody(new RestRequest<dynamic>
         {
            Auth = Db.RestAuthObject, 
            Body = DummyBody
         });

         var response = client.Execute<DataModel.Rest.RestResponse<SecObjectSingle>>(request);

         var objs = new List<SecObject> {response.Data.Body.Object};

         return objs;
      }

      #region Entries

      protected override IList<SecEntry> GetEntries(string appName, string contextName, string objectName, string userLogin)
      {
         var client = CreateClient();
         IRestRequest request;

         try
         {
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

            request.AddJsonBody(new RestRequest<dynamic>
            {
               Auth = Db.RestAuthObject, 
               Body = new object()
            });
         }
         catch (ArgumentException e)
         {
            throw new Exception(e.Message);
         }

         var response = client.Execute<DataModel.Rest.RestResponse<SecEntrySingle>>(request);

         var entries = new List<SecEntry> {response.Data.Body.Entry};

         return entries;
      }

      protected override bool DoAddEntry(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName)
      {
         var client = CreateClient();
         var request = new RestRequest("{appName}/entries", Method.PUT);
         try
         {
            request.AddUrlSegment("appName", appName);
            if (userLogin != null)
            {
               request.AddJsonBody(new RestRequest<SecEntrySingle>
               {
                  Auth = Db.RestAuthObject,
                  Body = new SecEntrySingle {Entry = new SecEntry {Context = secContext, Object = secObject, User = new SecUser {Login = userLogin}}}
               });
            }
            else
            {
               request.AddJsonBody(new RestRequest<SecEntrySingle>
               {
                  Auth = Db.RestAuthObject,
                  Body = new SecEntrySingle {Entry = new SecEntry {Context = secContext, Object = secObject, Group = new SecGroup {Name = groupName}}}
               });
            }

            var response = client.Execute<DataModel.Rest.RestResponse<SecEntrySingle>>(request);
         }
         catch (EntryExistingException e)
         {
            throw new Exception(e.Message);
         }
         return true;
      }

      protected override bool DoRemoveEntry(string appName, string contextName, string objectName, string userLogin, string groupName)
      {
         var client = CreateClient();
         var request = new RestRequest("{appName}/entries/{contextName}/{objectName}", Method.DELETE);
         try
         {
            request.AddUrlSegment("appName", appName);
            request.AddUrlSegment("contextName", contextName);
            request.AddUrlSegment("objectName", objectName);

            if (userLogin != null)
            {
               request.AddJsonBody(new RestRequest<SecEntrySingle>
               {
                  Auth = Db.RestAuthObject,
                  Body = new SecEntrySingle
                  {
                     Entry = new SecEntry
                     {
                        User = new SecUser {Login = userLogin}
                     }
                  }
               });
            }
            else
            {
               request.AddJsonBody(new RestRequest<SecEntrySingle>
               {
                  Auth = Db.RestAuthObject,
                  Body = new SecEntrySingle
                  {
                     Entry = new SecEntry
                     {
                        Group = new SecGroup {Name = groupName}
                     }
                  }
               });
            }

            var response = client.Execute(request).Content;

            try
            {
               var entry = JsonConvert.DeserializeObject<DataModel.Rest.RestResponse<SecEntrySingle>>(response);
            }
            catch
            {
               var error = JsonConvert.DeserializeObject<DataModel.Rest.RestResponse<FailureBody>>(response);
               if (error != null && error.Body.Exception != null)
               {
                  throw error.Body.Exception;
               }
               throw new Exception("BOH");
            }
         }
         catch (EntryExistingException e)
         {
            throw new Exception(e.Message);
         }
         return true;
      }

      #endregion

      #region Private Methods

      private static RestClient CreateClient()
      {
         return new RestClient(Configuration.Instance.ConnectionString + "/security");
      }

      #endregion
   }
}
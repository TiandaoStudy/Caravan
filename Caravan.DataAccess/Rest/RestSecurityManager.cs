using System;
using System.Collections.Generic;
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
      protected override IList<SecApp> GetApps(string appName)
      {
         var client = new RestClient("http://localhost/Caravan.RestService/security");
         var request = new RestRequest("{appName}", Method.POST);
         
         request.AddUrlSegment("appName", appName);
         request.AddJsonBody(new RestRequest<object> {Auth = "AA", Body = new object()});

         var response = client.Execute<DataModel.Rest.RestResponse<SecAppSingle>>(request);
       

         return (IList<SecApp>)(response.Data.Body.App);

        
      }

      protected override bool DoAddApp(SecApp app)
      {
         var client = new RestClient("http://localhost/Caravan.RestService/security");
         var request = new RestRequest("", Method.PUT);

         try
         {
           request.AddJsonBody(new RestRequest<SecAppSingle> 
            {Auth = "AA", Body = new SecAppSingle {
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

         request.AddJsonBody(new RestRequest<dynamic> {Auth = "AA", Body = new object()});

         var response = client.Execute<DataModel.Rest.RestResponse<SecGroupSingle>>(request);

         return (IList<SecGroup>)response.Data.Body;
      }

      protected override bool DoAddGroup(string appName, SecGroup newGroup)
      {
         var client = new RestClient("http://localhost/Caravan.RestService/security");
         var request = new RestRequest("{appName}/groups",Method.PUT);
         try
         {
            request.AddUrlSegment("appName", appName);
            request.AddJsonBody(new RestRequest<SecGroupSingle>
            {
               Auth = "AA",
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
         var client = new RestClient("http://localhost/Caravan.RestService/security");
         var request = new RestRequest("{appName}/groups/{groupName}", Method.PATCH);
         try
         {
            request.AddUrlSegment("appName", appName);
            request.AddUrlSegment("groupName", groupName);
            request.AddJsonBody(new RestRequest<dynamic>{Auth = "AA",Body = new object()});
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
         var client = new RestClient("http://localhost/Caravan.RestService/security");
         var request = new RestRequest("{appName}/groups/{groupName}", Method.PATCH);
         try
         {
            request.AddUrlSegment("appName", appName);
            request.AddUrlSegment("groupName", groupName);
            request.AddJsonBody(new RestRequest<SecGroupSingle> 
            { 
               Auth = "AA", Body = new SecGroupSingle
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
         request.AddJsonBody(new RestRequest<SecUserSingle> {Auth = "AA", Body = null});
         var response = client.Execute<DataModel.Rest.RestResponse<SecUserSingle>>(request);

         return (IList<SecUser>)(response.Data.Body.User);
      }

      protected override bool DoAddUser(string appName, SecUser newUser)
      {
         var client = new RestClient("http://localhost/Caravan.RestService/security");
         var request = new RestRequest("{appName}/users", Method.PUT);
         try
         {
            request.AddUrlSegment("appName", appName);
           
            request.AddJsonBody(new RestRequest<SecUserSingle>
            {
               Auth = "AA", 
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
            var response =client.Execute<DataModel.Rest.RestResponse<SecUserSingle>>(request);
         }
         catch (AppNotFoundException e)
         {
            throw new Exception(e.Message);
         }
         catch (UserExistingException e)
         {
            throw new Exception(e.Message);
         }
         return true;
      }

      protected override bool DoRemoveUser(string appName, string userLogin)
      {
         var client = new RestClient("http://localhost/Caravan.RestService/security");
         var request = new RestRequest("{appName}/users/{userLogin}", Method.DELETE);
         try
         {
            request.AddUrlSegment("appName", appName);
            request.AddUrlSegment("userLogin", userLogin);

            request.AddJsonBody(new {Auth = "AA", Body = new object()});
            var response = client.Execute<DataModel.Rest.RestResponse<dynamic>>(request);
         }
         catch (UserNotFoundException e)
         {
            throw new Exception(e.Message);
         }
         catch (AppNotFoundException e)
         {
            throw new Exception(e.Message);
         }
         return true;
      }

      protected override bool DoUpdateUser(string appName, string userLogin, SecUser newUser)
      {
         var client = new RestClient("http://localhost/Caravan.RestService/security");
         var request = new RestRequest("{appName}/users/{userLogin}", Method.PATCH);
         try
         {
            request.AddUrlSegment("appName", appName);
            request.AddUrlSegment("userLogin", userLogin);
            request.AddJsonBody(new RestRequest<SecUserSingle>
            {
               Auth = "AA",
               Body = new SecUserSingle {
                  User = new SecUser 
                  {
                     FirstName = newUser.FirstName, 
                     LastName = newUser.LastName,
                     Login = newUser.Login,
                     Email = newUser.Email}
                  }
            });
            var response = client.Execute<DataModel.Rest.RestResponse<SecUserSingle>>(request);
         }
         catch (UserNotFoundException e)
         {
            throw new Exception(e.Message);

         }
         catch (AppNotFoundException e)
         {
            throw new Exception(e.Message);
         }
         return true;
      }

      protected override bool DoAddUserToGroup(string appName, string userLogin, string groupName)
      {
         throw new NotImplementedException();
      }

      protected override bool DoRemoveUserFromGroup(string appName, string userLogin, string groupName)
      {
         throw new NotImplementedException();
      }

      protected override IList<SecContext> GetContexts(string appName)
      {
         var client = new RestClient("http://localhost/Caravan.RestService/security");
         var request = new RestRequest("{appName}/contexts", Method.POST);

         request.AddUrlSegment("appName", appName);
         request.AddJsonBody(new RestRequest<dynamic> {Auth = "AA", Body = new object()});

         var response = client.Execute<DataModel.Rest.RestResponse<SecContextSingle>>(request);

         return (IList<SecContext>) response.Data.Body;
      }

      protected override IList<SecObject> GetObjects(string appName, string contextName)
      {
         throw new NotImplementedException();
      }

      #region Entries

      protected override IList<SecEntry> GetEntries(string appName, string contextName, string objectName, string userLogin)
      {
         throw new NotImplementedException();
      }

      protected override bool DoAddEntry(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName)
      {
         throw new NotImplementedException();
      }

      protected override bool DoRemoveEntry(string appName, string contextName, string objectName, string userLogin, string groupName)
      {
         throw new NotImplementedException();
      }

      #endregion
   }
}

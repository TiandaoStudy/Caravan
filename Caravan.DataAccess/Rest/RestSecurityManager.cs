using System;
using System.Collections.Generic;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataModel.Security;
using Newtonsoft.Json;
using RestSharp;

namespace Finsa.Caravan.DataAccess.Rest
{
   public sealed class RestSecurityManager : SecurityManagerBase<RestSecurityManager>
   {
      protected override IList<SecApp> GetApps(string appName)
      {
         throw new NotImplementedException();
      }

      protected override bool DoAddApp(SecApp app)
      {
         throw new NotImplementedException();
      }

      protected override IList<SecGroup> GetGroups(string appName, string groupName)
      {
         throw new NotImplementedException();
      }

      protected override bool DoAddGroup(string appName, SecGroup newGroup)
      {
         throw new NotImplementedException();
      }

      protected override bool DoRemoveGroup(string appName, string groupName)
      {
         throw new NotImplementedException();
      }

      protected override bool DoUpdateGroup(string appName, string groupName, SecGroup newGroup)
      {
         throw new NotImplementedException();
      }

      protected override IList<SecUser> GetUsers(string appName, string userLogin)
      {
         var client = new RestClient("http://localhost/Caravan.RestService/security");
         var request = new RestRequest("{appName}/users/{userLogin}", Method.POST);
         request.AddUrlSegment("appName", appName);
         if (userLogin != null)
         {
            request.AddUrlSegment("userLogin", userLogin);
         }
         request.AddJsonBody(new {Auth = "AA", Body = new object()});
         var response = client.Execute<DataModel.Rest.RestResponse<SecUserSingle>>(request);

         return (IList<SecUser>)(response.Data.Body.User);
      }

      protected override bool DoAddUser(string appName, SecUser newUser)
      {
         var client = new RestClient("http://localhost/Caravan.RestService/security");
         var request = new RestRequest("{appName}", Method.POST);
         
         var added = false;
         
         
         request.AddUrlSegment("appName", appName);
         request.AddUrlSegment("userLogin", newUser.Login);

         //verificare se esiste l'app
         request.AddJsonBody(new { Auth = "AA", Body = appName });

         //verificare se l'utente esiste già
            
         added = true;
         

         return added;
      }

      protected override bool DoRemoveUser(string appName, string userLogin)
      {
         throw new NotImplementedException();
      }

      protected override bool DoUpdateUser(string appName, string userLogin, SecUser newUser)
      {
         throw new NotImplementedException();
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
         throw new NotImplementedException();
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

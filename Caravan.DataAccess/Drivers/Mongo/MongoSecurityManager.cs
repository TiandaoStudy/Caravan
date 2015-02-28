using System;
using System.Collections.Generic;
using Finsa.Caravan.Common.Models.Security;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataAccess.Drivers.Mongo.DataModel;
using Finsa.Caravan.DataAccess.Drivers.Mongo.DataModel.Logging;
using Finsa.Caravan.DataAccess.Drivers.Mongo.DataModel.Security;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Finsa.Caravan.DataAccess.Drivers.Mongo
{
   internal sealed class MongoSecurityManager : SecurityManagerBase<MongoSecurityManager>
   {
      protected override IList<SecApp> GetApps()
      {
         throw new System.NotImplementedException();
      }

      protected override SecApp GetApp(string appName)
      {
         throw new System.NotImplementedException();
      }

      protected override bool DoAddApp(SecApp app)
      {
         var newAppId = MongoUtilities.GetSequenceCollection().FindAndModify(new FindAndModifyArgs
         {
            Query = Query<MongoSequence>.Where(s => s.AppId == MongoUtilities.CreateObjectId(-1) && s.CollectionName == MongoUtilities.SecAppCollection),
            Update = Update<MongoSequence>.Inc(s => s.LastNumber, 1),
            VersionReturned = FindAndModifyDocumentVersion.Modified,
            Upsert = true, // Creates a new document if it does not exist.
         }).GetModifiedDocumentAs<MongoSequence>().LastNumber;

         return MongoUtilities.GetSecAppCollection().Insert(new MongoSecApp
         {
            Id = MongoUtilities.CreateObjectId(newAppId),
            AppId = newAppId,
            Name = app.Name,
            Description = app.Description,
            LogSettings = new List<MongoLogSettings>()
         }).Ok;
      }

      protected override IList<SecGroup> GetGroups(string appName, string groupName)
      {
         throw new System.NotImplementedException();
      }

      protected override bool DoAddGroup(string appName, SecGroup newGroup)
      {
         throw new System.NotImplementedException();
      }

      protected override bool DoRemoveGroup(string appName, string groupName)
      {
         throw new System.NotImplementedException();
      }

      protected override bool DoUpdateGroup(string appName, string groupName, SecGroup newGroup)
      {
         throw new System.NotImplementedException();
      }

      protected override IList<SecUser> GetUsers(string appName, string userLogin)
      {
         throw new System.NotImplementedException();
      }

      protected override bool DoAddUser(string appName, SecUser newUser)
      {
         throw new System.NotImplementedException();
      }

      protected override bool DoRemoveUser(string appName, string userLogin)
      {
         throw new System.NotImplementedException();
      }

      protected override bool DoUpdateUser(string appName, string userLogin, SecUser newUser)
      {
         throw new System.NotImplementedException();
      }

      protected override bool DoAddUserToGroup(string appName, string userLogin, string groupName)
      {
         throw new System.NotImplementedException();
      }

      protected override bool DoRemoveUserFromGroup(string appName, string userLogin, string groupName)
      {
         throw new System.NotImplementedException();
      }

      protected override IList<SecContext> GetContexts(string appName)
      {
         throw new System.NotImplementedException();
      }

      protected override IList<SecObject> GetObjects(string appName, string contextName)
      {
         throw new System.NotImplementedException();
      }

      protected override IList<SecEntry> GetEntries(string appName, string contextName, string objectName, string userLogin)
      {
         throw new System.NotImplementedException();
      }

      protected override bool DoAddEntry(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName)
      {
         throw new System.NotImplementedException();
      }

      protected override bool DoRemoveEntry(string appName, string contextName, string objectName, string userLogin, string groupName)
      {
         throw new System.NotImplementedException();
      }
   }
}

﻿using Finsa.Caravan.Common.Models.Security;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataAccess.Drivers.Mongo.DataModel;
using Finsa.Caravan.DataAccess.Drivers.Mongo.DataModel.Logging;
using Finsa.Caravan.DataAccess.Drivers.Mongo.DataModel.Security;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Collections.Generic;

namespace Finsa.Caravan.DataAccess.Drivers.Mongo
{
    internal sealed class MongoSecurityRepository : AbstractSecurityRepository<MongoSecurityRepository>
    {
        protected override SecApp[] GetAppsInternal()
        {
            throw new System.NotImplementedException();
        }

        protected override SecApp GetAppInternal(string appName)
        {
            throw new System.NotImplementedException();
        }

        protected override bool AddAppInternal(SecApp app)
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

        protected override SecGroup[] GetGroupsInternal(string appName, string groupName)
        {
            throw new System.NotImplementedException();
        }

        protected override bool AddGroupInternal(string appName, SecGroup newGroup)
        {
            throw new System.NotImplementedException();
        }

        protected override bool RemoveGroupInternal(string appName, string groupName)
        {
            throw new System.NotImplementedException();
        }

        protected override bool UpdateGroupInternal(string appName, string groupName, SecGroupUpdates groupUpdates)
        {
            throw new System.NotImplementedException();
        }

        protected override SecUser[] GetUsersInternal(string appName, string userLogin, string userEmail)
        {
            throw new System.NotImplementedException();
        }

        protected override bool AddUserInternal(string appName, SecUser newUser)
        {
            throw new System.NotImplementedException();
        }

        protected override bool RemoveUserInternal(string appName, string userLogin)
        {
            throw new System.NotImplementedException();
        }

        protected override bool UpdateUserInternal(string appName, string userLogin, SecUserUpdates userUpdates)
        {
            throw new System.NotImplementedException();
        }

        protected override bool AddUserToGroupInternal(string appName, string userLogin, string groupName)
        {
            throw new System.NotImplementedException();
        }

        protected override bool RemoveUserFromGroupInternal(string appName, string userLogin, string groupName)
        {
            throw new System.NotImplementedException();
        }

        protected override SecContext[] GetContextsInternal(string appName)
        {
            throw new System.NotImplementedException();
        }

        protected override SecObject[] GetObjectsInternal(string appName, string contextName)
        {
            throw new System.NotImplementedException();
        }

        protected override SecEntry[] GetEntriesInternal(string appName, string contextName, string objectName, string userLogin)
        {
            throw new System.NotImplementedException();
        }

        protected override bool AddEntryInternal(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName)
        {
            throw new System.NotImplementedException();
        }

        protected override bool RemoveEntryInternal(string appName, string contextName, string objectName, string userLogin, string groupName)
        {
            throw new System.NotImplementedException();
        }
    }
}
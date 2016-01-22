using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Security.Models;
using Finsa.Caravan.DataAccess.Core;
using System.Threading.Tasks;
using System;
using System.Linq;
using Finsa.Caravan.Common.Security;

namespace Finsa.Caravan.DataAccess.Mongo
{
    internal sealed class MongoSecurityRepository : AbstractSecurityRepository<MongoSecurityRepository>
    {
        public MongoSecurityRepository(ICaravanLog log, ICaravanSecurityValidator validator)
            : base(log, validator)
        {
        }

        public override void Dispose()
        {
            // Nulla da fare, per ora...
        }

        protected override Task<SecApp[]> GetAppsAsyncInternal(string appName)
        {
            throw new System.NotImplementedException();
        }

        protected override Task AddAppAsyncInternal(SecApp app)
        {
            //var newAppId = MongoUtilities.GetSequenceCollection().FindOneAndUpdateAsync(new FindAndModifyArgs
            //{
            //    Query = Query<MongoSequence>.Where(s => s.AppId == MongoUtilities.CreateObjectId(-1) && s.CollectionName == MongoUtilities.SecAppCollection),
            //    Update = Update<MongoSequence>.Inc(s => s.LastNumber, 1),
            //    VersionReturned = FindAndModifyDocumentVersion.Modified,
            //    Upsert = true, // Creates a new document if it does not exist.
            //}).GetModifiedDocumentAs<MongoSequence>().LastNumber;

            //MongoUtilities.GetSecAppCollection().InsertOneAsync(new MongoSecApp
            //{
            //    Id = MongoUtilities.CreateObjectId(newAppId),
            //    AppId = newAppId,
            //    Name = app.Name,
            //    Description = app.Description,
            //    LogSettings = new List<MongoLogSettings>()
            //}).Wait();
            throw new System.NotImplementedException();
        }

        protected override Task<SecGroup[]> GetGroupsAsyncInternal(string appName, int? groupId, string groupName)
        {
            throw new System.NotImplementedException();
        }

        protected override Task AddGroupAsyncInternal(string appName, SecGroup newGroup)
        {
            throw new System.NotImplementedException();
        }

        protected override Task RemoveGroupAsyncInternal(string appName, string groupName)
        {
            throw new System.NotImplementedException();
        }

        protected override Task UpdateGroupAsyncInternal(string appName, string groupName, SecGroupUpdates groupUpdates)
        {
            throw new System.NotImplementedException();
        }

        protected override Task<SecUser[]> GetUsersAsyncInternal(string appName, long? userId, string userLogin, string userEmail)
        {
            throw new System.NotImplementedException();
        }

        protected override Task AddUserAsyncInternal(string appName, SecUser newUser)
        {
            throw new System.NotImplementedException();
        }

        protected override Task RemoveUserAsyncInternal(string appName, string userLogin)
        {
            throw new System.NotImplementedException();
        }

        protected override Task UpdateUserAsyncInternal(string appName, string userLogin, SecUserUpdates userUpdates)
        {
            throw new System.NotImplementedException();
        }

        protected override Task AddUserToRoleAsyncInternal(string appName, string userLogin, string groupName, string roleName)
        {
            throw new System.NotImplementedException();
        }

        protected override Task RemoveUserFromRoleAsyncInternal(string appName, string userLogin, string groupName, string roleName)
        {
            throw new System.NotImplementedException();
        }

        protected override Task<SecContext[]> GetContextsAsyncInternal(string appName)
        {
            throw new System.NotImplementedException();
        }

        protected override Task<SecObject[]> GetObjectsAsyncInternal(string appName, string contextName)
        {
            throw new System.NotImplementedException();
        }

        protected override Task<SecEntry[]> GetEntriesAsyncInternal(string appName, string contextName, string objectName, string userLogin)
        {
            throw new System.NotImplementedException();
        }

        protected override Task<long> AddEntryAsyncInternal(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName, string roleName)
        {
            throw new System.NotImplementedException();
        }

        protected override Task RemoveEntryAsyncInternal(string appName, string contextName, string objectName, string userLogin, string groupName, string roleName)
        {
            throw new System.NotImplementedException();
        }

        protected override Task AddUserClaimAsyncInternal(string appName, string userLogin, SecClaim claim)
        {
            throw new NotImplementedException();
        }

        protected override Task RemoveUserClaimAsyncInternal(string appName, string userLogin, string serializedClaimHash)
        {
            throw new NotImplementedException();
        }

        protected override Task<SecRole[]> GetRolesAsyncInternal(string appName, string groupName, string roleName, int? roleId)
        {
            throw new NotImplementedException();
        }

        protected override Task AddRoleAsyncInternal(string appName, string groupName, SecRole newRole)
        {
            throw new NotImplementedException();
        }

        protected override Task RemoveRoleAsyncInternal(string appName, string groupName, string roleName)
        {
            throw new NotImplementedException();
        }

        protected override Task UpdateRoleAsyncInternal(string appName, string groupName, string roleName, SecRoleUpdates roleUpdates)
        {
            throw new NotImplementedException();
        }

        protected override Task<IQueryable<SecUser>> QueryUsersInGroupAsyncInternal(string appName, string groupName)
        {
            throw new NotImplementedException();
        }

        protected override Task<IQueryable<SecUser>> QueryUsersAsyncInternal(string appName)
        {
            throw new NotImplementedException();
        }

        protected override Task<IQueryable<SecUser>> QueryUsersInRoleAsyncInternal(string appName, string groupName, string roleName)
        {
            throw new NotImplementedException();
        }
    }
}

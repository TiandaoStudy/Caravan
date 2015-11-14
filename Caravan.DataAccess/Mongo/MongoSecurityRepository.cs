using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Security.Models;
using Finsa.Caravan.DataAccess.Core;
using System.Threading.Tasks;

namespace Finsa.Caravan.DataAccess.Mongo
{
    internal sealed class MongoSecurityRepository : AbstractSecurityRepository<MongoSecurityRepository>
    {
        public MongoSecurityRepository(ICaravanLog log)
            : base(log)
        {
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

        protected override Task<SecGroup[]> GetGroupsInternal(string appName, string groupName)
        {
            throw new System.NotImplementedException();
        }

        protected override Task AddGroupInternal(string appName, SecGroup newGroup)
        {
            throw new System.NotImplementedException();
        }

        protected override Task RemoveGroupInternal(string appName, string groupName)
        {
            throw new System.NotImplementedException();
        }

        protected override Task UpdateGroupInternal(string appName, string groupName, SecGroupUpdates groupUpdates)
        {
            throw new System.NotImplementedException();
        }

        protected override Task<SecUser[]> GetUsersInternal(string appName, string userLogin, string userEmail)
        {
            throw new System.NotImplementedException();
        }

        protected override Task AddUserInternal(string appName, SecUser newUser)
        {
            throw new System.NotImplementedException();
        }

        protected override Task RemoveUserInternal(string appName, string userLogin)
        {
            throw new System.NotImplementedException();
        }

        protected override Task UpdateUserInternal(string appName, string userLogin, SecUserUpdates userUpdates)
        {
            throw new System.NotImplementedException();
        }

        protected override Task AddUserToGroupInternal(string appName, string userLogin, string groupName)
        {
            throw new System.NotImplementedException();
        }

        protected override Task RemoveUserFromGroupInternal(string appName, string userLogin, string groupName)
        {
            throw new System.NotImplementedException();
        }

        protected override Task<SecContext[]> GetContextsInternal(string appName)
        {
            throw new System.NotImplementedException();
        }

        protected override Task<SecObject[]> GetObjectsInternal(string appName, string contextName)
        {
            throw new System.NotImplementedException();
        }

        protected override Task<SecEntry[]> GetEntriesInternal(string appName, string contextName, string objectName, string userLogin)
        {
            throw new System.NotImplementedException();
        }

        protected override Task AddEntryInternal(string appName, SecContext secContext, SecObject secObject, string userLogin, string groupName)
        {
            throw new System.NotImplementedException();
        }

        protected override Task RemoveEntryInternal(string appName, string contextName, string objectName, string userLogin, string groupName)
        {
            throw new System.NotImplementedException();
        }
    }
}

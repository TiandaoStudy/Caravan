using System;
using System.Data.Common;
using Finsa.Caravan.DataAccess.Core;

namespace Finsa.Caravan.DataAccess.Drivers.Mongo
{
    internal sealed class MongoDbManager : DbManagerBase
    {
        public override DataAccessKind Kind
        {
            get { return DataAccessKind.MongoDb; }
        }

        public override void ElaborateConnectionString(ref string connectionString)
        {
        }

        public override DbConnection CreateConnection()
        {
            throw new NotImplementedException();
        }
    }
}
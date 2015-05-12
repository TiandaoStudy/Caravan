using System;
using System.Data.Common;
using Finsa.Caravan.DataAccess.Core;

namespace Finsa.Caravan.DataAccess.Drivers.Mongo
{
    internal sealed class MongoDataSourceManager : AbstractDataSourceManager
    {
        public override DataSourceKind DataSourceKind
        {
            get { return DataSourceKind.MongoDb; }
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
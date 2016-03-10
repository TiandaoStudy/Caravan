using Finsa.Caravan.DataAccess.Core;
using System;
using System.Data.Common;

namespace Finsa.Caravan.DataAccess.Mongo
{
    internal sealed class MongoDataSourceManager : AbstractDataSourceManager
    {
        public override CaravanDataSourceKind DataSourceKind
        {
            get { return CaravanDataSourceKind.MongoDb; }
        }

        public override DateTime DataSourceDateTime
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string ElaborateConnectionString(string connectionString)
        {
            return connectionString;
        }

        public override DbConnection CreateConnection()
        {
            throw new NotImplementedException();
        }
    }
}

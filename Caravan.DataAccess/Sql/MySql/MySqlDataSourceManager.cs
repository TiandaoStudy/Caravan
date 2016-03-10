﻿using Dapper;
using Finsa.Caravan.DataAccess.Core;
using System;
using System.Data.Common;

namespace Finsa.Caravan.DataAccess.Sql.MySql
{
    internal sealed class MySqlDataSourceManager : AbstractDataSourceManager
    {
        public override CaravanDataSourceKind DataSourceKind
        {
            get { return CaravanDataSourceKind.MySql; }
        }

        public override DateTime DataSourceDateTime
        {
            get
            {
                using (var connection = OpenConnection())
                {
                    const string query = "TODO";
                    return connection.ExecuteScalar<DateTime>(query);
                }
            }
        }

        public override string ElaborateConnectionString(string connectionString)
        {
            // Nothing to do with the connection string.
            return connectionString;
        }

        public override DbConnection CreateConnection()
        {
            var connection = global::MySql.Data.MySqlClient.MySqlClientFactory.Instance.CreateConnection();
            connection.ConnectionString = ConnectionString;
            return connection;
        }
    }
}

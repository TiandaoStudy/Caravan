using FLEX.DataAccess.Core;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Data.Common;

namespace FLEX.DataAccess.SqlServerCe
{
	public sealed class SqlServerCeQueryExecutor : QueryExecutorBase
	{


		private static readonly DbProviderFactory DbFactory = DbProviderFactories.GetFactory("System.Data.SqlServerCe.4.0");
		public override void ElaborateConnectionString(ref string connectionString)
		{
			connectionString = connectionString;
		}

		public override IDbConnection OpenConnection()
		{
			var connection = DbFactory.CreateConnection();
			connection.ConnectionString = Configuration.Instance.ConnectionString;
			connection.Open();
			return connection;
		}
	}
}

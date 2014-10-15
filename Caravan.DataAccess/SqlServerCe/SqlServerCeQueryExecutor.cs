using System.Data;
using System.Data.Common;
using Finsa.Caravan.DataAccess.Core;

namespace Finsa.Caravan.DataAccess.SqlServerCe
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

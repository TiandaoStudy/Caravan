using System.Data.Common;

namespace Finsa.Caravan.DataAccess.SqlServerCe
{
	public sealed class SqlServerCeQueryExecutor : IQueryExecutor
	{
		private static readonly DbProviderFactory DbFactory = DbProviderFactories.GetFactory("System.Data.SqlServerCe.4.0");

	   public DatabaseVendor Vendor
	   {
	      get { return DatabaseVendor.SqlServerCe; }
	   }

	   public void ElaborateConnectionString(ref string connectionString)
		{
			connectionString = connectionString;
		}

		public DbConnection OpenConnection()
		{
			var connection = DbFactory.CreateConnection();
			connection.ConnectionString = Configuration.Instance.ConnectionString;
			connection.Open();
			return connection;
		}
	}
}

using System.Data.Common;

namespace Finsa.Caravan.DataAccess.Direct.SqlServerCe
{
	public sealed class SqlServerCeDbManager : IDbManager
	{
		private static readonly DbProviderFactory DbFactory = DbProviderFactories.GetFactory("System.Data.SqlServerCe.4.0");

	   public DataAccessKind Kind
	   {
	      get { return DataAccessKind.SqlServerCe; }
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

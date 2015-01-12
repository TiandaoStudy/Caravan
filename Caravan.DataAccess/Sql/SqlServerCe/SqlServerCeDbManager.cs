using System.Data.Common;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataAccess.Properties;

namespace Finsa.Caravan.DataAccess.Sql.SqlServerCe
{
	public sealed class SqlServerCeDbManager : DbManagerBase
	{
		private static readonly DbProviderFactory DbFactory = DbProviderFactories.GetFactory("System.Data.SqlServerCe.4.0");

	   public override DataAccessKind Kind
	   {
	      get { return DataAccessKind.SqlServerCe; }
	   }

	   public override void ElaborateConnectionString(ref string connectionString)
		{
			connectionString = connectionString;
		}

		public override DbConnection CreateConnection()
		{
			var connection = DbFactory.CreateConnection();
			connection.ConnectionString = Settings.Default.ConnectionString;
			return connection;
		}
	}
}

using Finsa.Caravan.Common.DataModel.Logging;
using Finsa.Caravan.Common.Models.Security;
using Finsa.Caravan.DataAccess.Sql;
using System.Data.Entity;

namespace Finsa.Caravan.DataAccess.Core
{
    internal abstract class DbContextBase : CaravanDbContext<DbContextBase>
    {
        internal DbContextBase()
            : base(Db.Manager.OpenConnection(), true)
        {
        }

        public DbSet<LogEntry> LogEntries { get; set; }

        public DbSet<LogSetting> LogSettings { get; set; }

        public DbSet<SecApp> SecApps { get; set; }

        public DbSet<SecContext> SecContexts { get; set; }

        public DbSet<SecEntry> SecEntries { get; set; }

        public DbSet<SecGroup> SecGroups { get; set; }

        public DbSet<SecObject> SecObjects { get; set; }

        public DbSet<SecUser> SecUsers { get; set; }
    }
}
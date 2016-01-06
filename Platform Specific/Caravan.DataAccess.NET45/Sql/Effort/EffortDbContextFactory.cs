// Copyright 2015-2025 Finsa S.p.A. <finsa@finsa.it>
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except
// in compliance with the License. You may obtain a copy of the License at:
// 
// "http://www.apache.org/licenses/LICENSE-2.0"
// 
// Unless required by applicable law or agreed to in writing, software distributed under the License
// is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
// or implied. See the License for the specific language governing permissions and limitations under
// the License.

using PommaLabs.Thrower;
using System.Data.Entity;

namespace Finsa.Caravan.DataAccess.Sql.Effort
{
    /// <summary>
    ///   Una factory dedicata agli unit test. Crea dei contesti in memoria che usano sempre la
    ///   medesima stringa di connessione verso Effort.
    /// </summary>
    /// <typeparam name="TContext">Il contesto oggetto dei test.</typeparam>
    public sealed class EffortDbContextFactory<TContext> : ConfigurableDbContextFactory<TContext>, IUnitTestableDbContextFactory<TContext>
        where TContext : UnitTestableDbContext<TContext>, new()
    {
        /// <summary>
        ///   Il gestore della connessione verso Effort.
        /// </summary>
        internal static EffortDataSourceManager Manager { get; } = new EffortDataSourceManager();

        /// <summary>
        ///   Da usare SOLO E SOLTANTO negli unit test, resetta la connessione di Effort.
        /// </summary>
        public void Reset()
        {
            // A new connection is created and persisted for the whole test duration.
            Manager.ResetConnection();

            // The database is recreated, since it is in-memory and probably it does not exist.
            using (var ctx = Create())
            {
                ctx.Database.CreateIfNotExists();
                Database.SetInitializer(new DropCreateDatabaseAlways<TContext>());
                ctx.Database.Initialize(true);
                Database.SetInitializer(new CreateDatabaseIfNotExists<TContext>());
            }
        }

        /// <summary>
        ///   Creates a new instance of a derived <see cref="T:System.Data.Entity.DbContext"/> type.
        /// </summary>
        /// <returns>An instance of <typeparamref name="TContext"/>.</returns>
        public override TContext Create()
        {
            var dbContext = new TContext();
            CopyConfiguration(DefaultConfiguration, dbContext.Configuration);
            return dbContext;
        }

        /// <summary>
        ///   Creates a new instance of a derived <see cref="DbContext"/> type.
        /// </summary>
        /// <param name="customConfiguration">A custom context configuration.</param>
        /// <returns>An instance of <typeparamref name="TContext"/>.</returns>
        public override TContext Create(DbContextConfiguration<TContext> customConfiguration)
        {
            RaiseArgumentNullException.IfIsNull(customConfiguration, nameof(customConfiguration));
            var dbContext = new TContext();
            CopyConfiguration(customConfiguration, dbContext.Configuration);
            return dbContext;
        }
    }
}

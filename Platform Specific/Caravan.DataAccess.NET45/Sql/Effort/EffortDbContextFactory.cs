﻿// Copyright 2015-2025 Finsa S.p.A. <finsa@finsa.it>
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
using System.Data.Common;
using System.Data.Entity;
using System.Reflection;

namespace Finsa.Caravan.DataAccess.Sql.Effort
{
    /// <summary>
    ///   Una factory dedicata agli unit test. Crea dei contesti in memoria che usano sempre la
    ///   medesima stringa di connessione verso Effort.
    /// </summary>
    /// <typeparam name="TContext">Il contesto oggetto dei test.</typeparam>
    public sealed class EffortDbContextFactory<TContext> : ConfigurableDbContextFactory<TContext>, IUnitTestableDbContextFactory<TContext>
        where TContext : UnitTestableDbContext<TContext>
    {
        /// <summary>
        ///   Usato per creare i contesti per i test.
        /// </summary>
        private static readonly ConstructorInfo ContextCreator = typeof(TContext).GetConstructor(new[] { typeof(DbConnection) });

        /// <summary>
        ///   Usato per gestire la stringa di connessione al DB in memoria.
        /// </summary>
        private readonly EffortDataSourceManager _dataSourceManager;

        /// <summary>
        ///   Istanzia la factory partendo dal gestore dato.
        /// </summary>
        /// <param name="dataSourceManager">
        ///   Il gestore della connessione, la classe si aspetta che sia quello di Effort.
        /// </param>
        public EffortDbContextFactory(ICaravanDataSourceManager dataSourceManager)
        {
            var effortDataSourceManager = dataSourceManager as EffortDataSourceManager;
            RaiseArgumentNullException.IfIsNull(dataSourceManager, nameof(dataSourceManager));
            _dataSourceManager = effortDataSourceManager;
        }

        /// <summary>
        ///   Da usare SOLO E SOLTANTO negli unit test, resetta la connessione di Effort.
        /// </summary>
        public void Reset()
        {
            // A new connection is created and persisted for the whole test duration.
            _dataSourceManager.ResetConnection();

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
            var dbContext = ContextCreator.Invoke(new object[] { _dataSourceManager.OpenConnection() }) as TContext;
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
            var dbContext = ContextCreator.Invoke(new object[] { _dataSourceManager.OpenConnection() }) as TContext;
            CopyConfiguration(customConfiguration, dbContext.Configuration);
            return dbContext;
        }
    }
}

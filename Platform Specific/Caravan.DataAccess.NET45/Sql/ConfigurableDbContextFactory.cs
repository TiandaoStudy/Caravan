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

namespace Finsa.Caravan.DataAccess.Sql
{
    /// <summary>
    ///   A configurable factory for creating derived <see cref="DbContext"/> instances.
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    public class ConfigurableDbContextFactory<TContext> : IConfigurableDbContextFactory<TContext>
        where TContext : DbContext
    {
        /// <summary>
        ///   Builds the context factory and sets the default configuration.
        /// </summary>
        public ConfigurableDbContextFactory()
            : this(new DbContextConfiguration<TContext>())
        {
        }

        /// <summary>
        ///   Builds the context factory using sets the specified configuration as default.
        /// </summary>
        /// <param name="defaultConfiguration">The new default configuration.</param>
        public ConfigurableDbContextFactory(DbContextConfiguration<TContext> defaultConfiguration)
        {
            RaiseArgumentNullException.IfIsNull(defaultConfiguration, nameof(defaultConfiguration));
            DefaultConfiguration = defaultConfiguration;
        }

        /// <summary>
        ///   The default configuration used for contexts created within this factory.
        /// </summary>
        public DbContextConfiguration<TContext> DefaultConfiguration { get; }

        /// <summary>
        ///   Creates a new instance of a derived <see cref="DbContext"/> type.
        /// </summary>
        /// <returns>An instance of <typeparamref name="TContext"/>.</returns>
        public virtual TContext Create()
        {
            var dbContext = DefaultConfiguration.ContextCreator();
            CopyConfiguration(DefaultConfiguration, dbContext.Configuration);
            return dbContext;
        }

        /// <summary>
        ///   Creates a new instance of a derived <see cref="DbContext"/> type.
        /// </summary>
        /// <param name="customConfiguration">A custom context configuration.</param>
        /// <returns>An instance of <typeparamref name="TContext"/>.</returns>
        public virtual TContext Create(DbContextConfiguration<TContext> customConfiguration)
        {
            RaiseArgumentNullException.IfIsNull(customConfiguration, nameof(customConfiguration));
            var dbContext = customConfiguration.ContextCreator();
            CopyConfiguration(customConfiguration, dbContext.Configuration);
            return dbContext;
        }

        /// <summary>
        ///   Copies the source configuration members into the target ones.
        /// </summary>
        /// <param name="source">Source configuration.</param>
        /// <param name="target">Target configuration.</param>
        protected static void CopyConfiguration(DbContextConfiguration<TContext> source, System.Data.Entity.Infrastructure.DbContextConfiguration target)
        {
            target.AutoDetectChangesEnabled = source.AutoDetectChangesEnabled;
            target.EnsureTransactionsForFunctionsAndCommands = source.EnsureTransactionsForFunctionsAndCommands;
            target.LazyLoadingEnabled = source.LazyLoadingEnabled;
            target.ProxyCreationEnabled = source.ProxyCreationEnabled;
            target.UseDatabaseNullSemantics = source.UseDatabaseNullSemantics;
            target.ValidateOnSaveEnabled = target.ValidateOnSaveEnabled;
        }
    }
}
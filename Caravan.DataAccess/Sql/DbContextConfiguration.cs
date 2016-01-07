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

using System;
using System.Data.Entity;

namespace Finsa.Caravan.DataAccess.Sql
{
    /// <summary>
    ///   Provides access to configuration options for <see cref="IConfigurableDbContextFactory{TContext}"/>.
    /// </summary>
    public class DbContextConfiguration<TContext>
        where TContext : DbContext
    {
        /// <summary>
        ///   Gets or sets a value indicating whether the
        ///   System.Data.Entity.Infrastructure.DbChangeTracker.DetectChanges method is called
        ///   automatically by methods of System.Data.Entity.DbContext and related classes. The
        ///   default value is _true_.
        /// </summary>
        public bool AutoDetectChangesEnabled { get; set; } = true;

        /// <summary>
        ///   The function called when a new context instance is needed.
        /// </summary>
        public Func<TContext> ContextCreator { get; set; } = () => Activator.CreateInstance<TContext>();

        /// <summary>
        ///   Gets or sets the value that determines whether SQL functions and commands should be
        ///   always executed in a transaction. This flag is _enabled_ by default.
        /// </summary>
        /// <remarks>
        ///   This flag determines whether a new transaction will be started when methods such as
        ///   System.Data.Entity.Database.ExecuteSqlCommand(System.String,System.Object[]) are
        ///   executed outside of a transaction. Note that this does not change the behavior of System.Data.Entity.DbContext.SaveChanges.
        /// </remarks>
        public bool EnsureTransactionsForFunctionsAndCommands { get; set; } = true;

        /// <summary>
        ///   Gets or sets a value indicating whether lazy loading of relationships exposed as
        ///   navigation properties is enabled. Lazy loading is _disabled_ by default.
        /// </summary>
        public bool LazyLoadingEnabled { get; set; } = false;

        /// <summary>
        ///   Gets or sets a value indicating whether or not the framework will create instances of
        ///   dynamically generated proxy classes whenever it creates an instance of an entity type.
        ///   Note that even if proxy creation is enabled with this flag, proxy instances will only
        ///   be created for entity types that meet the requirements for being proxied. Proxy
        ///   creation is _enabled_ by default.
        /// </summary>
        public bool ProxyCreationEnabled { get; set; } = true;

        /// <summary>
        ///   Gets or sets a value indicating whether database null semantics are exhibited when
        ///   comparing two operands, both of which are potentially nullable. The default value is
        ///   _false_. For example (operand1 == operand2) will be translated as: (operand1
        ///   = operand2) if UseDatabaseNullSemantics is true, respectively (((operand1 = operand2)
        ///     AND (NOT (operand1 IS NULL OR operand2 IS NULL))) OR ((operand1 IS NULL) AND
        ///     (operand2 IS NULL))) if UseDatabaseNullSemantics is false.
        /// </summary>
        public bool UseDatabaseNullSemantics { get; set; } = false;

        /// <summary>
        ///   Gets or sets a value indicating whether tracked entities should be validated
        ///   automatically when System.Data.Entity.DbContext.SaveChanges is invoked. The default
        ///   value is _true_.
        /// </summary>
        public bool ValidateOnSaveEnabled { get; set; } = true;
    }
}
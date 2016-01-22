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

using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Finsa.Caravan.DataAccess.Sql
{
    /// <summary>
    ///   A configurable factory for creating derived <see cref="DbContext"/> instances.
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    public interface IConfigurableDbContextFactory<TContext> : IDbContextFactory<TContext>
        where TContext : DbContext
    {
        /// <summary>
        ///   The default configuration used for contexts created within this factory.
        /// </summary>
        DbContextConfiguration<TContext> DefaultConfiguration { get; }

        /// <summary>
        ///   Creates a new instance of a derived <see cref="DbContext"/> type.
        /// </summary>
        /// <param name="customConfiguration">A custom context configuration.</param>
        /// <returns>An instance of <typeparamref name="TContext"/>.</returns>
        TContext Create(DbContextConfiguration<TContext> customConfiguration);
    }
}

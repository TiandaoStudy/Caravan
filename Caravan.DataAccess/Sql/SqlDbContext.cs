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

using System.Data.Common;
using System.Data.Entity;

namespace Finsa.Caravan.DataAccess.Sql
{
    /// <summary>
    ///   Contesto DB usato dalla parte di accesso ai dati di Caravan.
    /// </summary>
    internal sealed class SqlDbContext : UnitTestableDbContext<SqlDbContext>
    {
        #region Constants

        internal const short TinyLength = 8;     // 2^3
        internal const short SmallLength = 32;   // 2^5
        internal const short MediumLength = 256; // 2^8
        internal const short LargeLength = 1024; // 2^10

        #endregion Constants

        static SqlDbContext()
        {
            switch (CaravanDataAccessConfiguration.Instance.SqlInitializer)
            {
                case "CreateDatabaseIfNotExists":
                    Database.SetInitializer(new CreateDatabaseIfNotExists<SqlDbContext>());
                    break;

                case "DropCreateDatabaseAlways":
                    Database.SetInitializer(new DropCreateDatabaseAlways<SqlDbContext>());
                    break;

                case "DropCreateDatabaseIfModelChanges":
                    Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SqlDbContext>());
                    break;

                default:
                    Database.SetInitializer<SqlDbContext>(null);
                    break;
            }
        }

        public SqlDbContext(DbConnection dbConnection)
            : base(dbConnection)
        {
            // Il lazy loading viene disabilitato dalla classe che si occupa di generare i contesti.
        }

        public SqlDbContext(ICaravanDataSourceManager dataSourceManager)
            : base(dataSourceManager.CreateConnection())
        {
            // Il lazy loading viene disabilitato dalla classe che si occupa di generare i contesti.
        }
    }
}

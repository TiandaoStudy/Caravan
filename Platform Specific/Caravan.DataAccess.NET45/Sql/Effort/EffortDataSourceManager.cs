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

namespace Finsa.Caravan.DataAccess.Sql.Effort
{
    internal static class EffortDataSourceManager
    {
        private static DbConnection _connection;

        static EffortDataSourceManager()
        {
            ResetConnection();
        }

        public static DbConnection OpenConnection()
        {
            // Open connection, since someone might have left it closed.
            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }
            return _connection;
        }

        public static void ResetConnection()
        {
            // Connection should be persisted, otherwise the DB may be lost.
            _connection = global::Effort.DbConnectionFactory.CreatePersistent("caravan");
        }
    }
}
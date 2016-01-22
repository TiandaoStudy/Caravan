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

namespace Finsa.Caravan.DataAccess.Core
{
    /// <summary>
    ///   Messaggi di errore usati nella libreria DataAccess.
    /// </summary>
    internal static class ErrorMessages
    {
        public const string InvalidEnumValue = "Given enum value is not valid";

        public const string NullClaim = "Caravan security claim cannot be null";
        public const string NullRole = "Caravan security role cannot be null";

        public const string NullOrWhiteSpaceAppName = "Caravan application name cannot be null, empty or blank";
        public const string NullOrWhiteSpaceContextName = "Caravan context name cannot be null, empty or blank";
        public const string NullOrWhiteSpaceGroupName = "Caravan group name cannot be null, empty or blank";
        public const string NullOrWhiteSpaceObjectName = "Caravan object name cannot be null, empty or blank";
        public const string NullOrWhiteSpaceRoleName = "Caravan role name cannot be null, empty or blank";
        public const string NullOrWhiteSpaceUserEmail = "Caravan user email cannot be null, empty or blank";
        public const string NullOrWhiteSpaceUserLogin = "Caravan user login cannot be null, empty or blank";
        public const string NullOrWhiteSpaceClaimHash = "Caravan security claim hash cannot be null, empty or blank";

        public const string AbstractSecurityRepository_GroupNotFound = "Group has not been specified or it does not exist";

        public const string Drivers_DriverNotForUnitTesting = "This driver cannot be used for unit testing. Please use Sql.Effort";
        public const string Drivers_DriverNotForCommonUsage = "This driver should be used only for unit testing";
    }
}
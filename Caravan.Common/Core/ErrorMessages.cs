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

namespace Finsa.Caravan.Common.Core
{
    /// <summary>
    ///   Messaggi di errore usati nella libreria Common.
    /// </summary>
    internal static class ErrorMessages
    {
        public const string InvalidEnumValue = "Given enum value is not valid";
        public const string InvalidGroupName = "Group name cannot contain slashes";
        public const string InvalidRoleName = "Role name cannot contain slashes";

        public const string NullOrWhiteSpaceAppName = "Caravan application name cannot be null, empty or blank";

        public const string CaravanUserManager_UserIdNotFound = "User ID {0} not found";
        public const string CaravanUserManager_UserAlreadyInRole = "User is already in role";
        public const string CaravanUserManager_UserNotInRole = "User is not in role";
    }
}
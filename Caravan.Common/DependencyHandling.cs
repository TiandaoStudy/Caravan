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

namespace Finsa.Caravan.Common
{
    /// <summary>
    ///   Modalità di gestione delle dipendenze.
    /// </summary>
    public enum DependencyHandling
    {
        /// <summary>
        ///   Modalità standard.
        /// </summary>
        Default = 0,

        /// <summary>
        ///   Modalità dedicata agli UNIT TEST.
        /// </summary>
        UnitTesting = 1,

        /// <summary>
        ///   Modalità dedicata all'ambiente di DEVL.
        /// </summary>
        DevelopmentEnvironment = 2,

        /// <summary>
        ///   Modalità dedicata all'ambiente di TEST.
        /// </summary>
        TestEnvironment = 3,

        /// <summary>
        ///   Modalità dedicata all''ambiente di PROD.
        /// </summary>
        ProductionEnvironment = 4
    }
}

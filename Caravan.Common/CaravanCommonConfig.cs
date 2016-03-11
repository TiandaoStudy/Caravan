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

using AutoMapper;
using Common.Logging;
using System;

namespace Finsa.Caravan.Common
{
    /// <summary>
    ///   Configurazione per la libreria Common.
    /// </summary>
    public static class CaravanCommonConfig
    {
        #region AutoMapper

        /// <summary>
        ///   Imposta le mappature necessarie alla libreria Common.
        /// </summary>
        /// <param name="cfg">La configurazione per AutoMapper.</param>
        /// <returns>La configurazione modificata per la libreria Common.</returns>
        public static IMapperConfiguration SetMappings(IMapperConfiguration cfg)
        {
            cfg.CreateMap<string, LogLevel>().ConvertUsing(str =>
            {
                LogLevel logLevel;
                return Enum.TryParse(str, true, out logLevel) ? logLevel : LogLevel.Debug;
            });

            cfg.CreateMap<NLog.LogLevel, LogLevel>();

            return cfg;
        }

        /// <summary>
        ///   Il mappatore della libreria Common.
        /// </summary>
        public static IMapper Mapper = new MapperConfiguration(cfg => SetMappings(cfg)).CreateMapper();

        #endregion AutoMapper
    }
}

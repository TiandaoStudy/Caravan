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
using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Security.Models;
using Finsa.Caravan.WebApi.Models.Security;

namespace Finsa.Caravan.WebApi
{
    /// <summary>
    ///   Configurazione per la libreria WebApi.
    /// </summary>
    public static class CaravanWebApiConfig
    {
        #region AutoMapper

        /// <summary>
        ///   Imposta le mappature necessarie alla libreria WebApi.
        /// </summary>
        /// <param name="cfg">La configurazione per AutoMapper.</param>
        /// <returns>La configurazione modificata per la libreria WebApi.</returns>
        public static IMapperConfiguration SetMappings(IMapperConfiguration cfg)
        {
            cfg.CreateMap<SecApp, LinkedSecApp>();

            return cfg;
        }

        /// <summary>
        ///   Il mappatore della libreria WebApi.
        /// </summary>
        public static IMapper Mapper = new MapperConfiguration(cfg => SetMappings(CaravanCommonConfig.SetMappings(cfg))).CreateMapper();

        #endregion AutoMapper
    }
}
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

using AutoMapper;
using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Logging.Models;
using Finsa.Caravan.Common.Security.Models;
using Finsa.Caravan.DataAccess.Sql.Logging.Entities;
using Finsa.Caravan.DataAccess.Sql.Security.Entities;
using Finsa.CodeServices.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Finsa.Caravan.DataAccess
{
    /// <summary>
    ///   Configurazione per la libreria DataAccess.
    /// </summary>
    public static class CaravanDataAccessConfig
    {
        #region AutoMapper

        /// <summary>
        ///   Imposta le mappature necessarie alla libreria DataAccess.
        /// </summary>
        /// <param name="cfg">La configurazione per AutoMapper.</param>
        /// <returns>La configurazione modificata per la libreria DataAccess.</returns>
        public static IMapperConfiguration SetMappings(IMapperConfiguration cfg)
        {
            // Mappings (SQL --> Models) for Security
            cfg.CreateMap<SqlSecApp, SecApp>();
            cfg.CreateMap<SqlSecClaim, SecClaim>();
            cfg.CreateMap<SqlSecContext, SecContext>();
            cfg.CreateMap<SqlSecEntry, SecEntry>().AfterMap((se, e) =>
            {
                e.ContextName = se.Object.Context.Name;
            });
            cfg.CreateMap<SqlSecGroup, SecGroup>();
            cfg.CreateMap<SqlSecObject, SecObject>();
            cfg.CreateMap<SqlSecRole, SecRole>().AfterMap((sr, r) =>
            {
                r.AppName = sr.Group.App.Name;
            });
            cfg.CreateMap<SqlSecUser, SecUser>()
                .ForMember(dest => dest.UserName, opts => opts.Ignore())
                .AfterMap((su, u) =>
                {
                    u.Groups = su.Roles
                        .Select(r => r.Group)
                        .Distinct()
                        .Select(Mapper.Map<SecGroup>)
                        .ToArray();
                });

            // Mappings (SQL --> Models) for Logging
            cfg.CreateMap<SqlLogEntry, LogEntry>().AfterMap((sl, l) =>
            {
                var array = new KeyValuePair<string, string>[10];
                var index = 0;
                if (sl.Key0 != null)
                {
                    array[index++] = KeyValuePair.Create(sl.Key0, sl.Value0);
                }
                if (sl.Key1 != null)
                {
                    array[index++] = KeyValuePair.Create(sl.Key1, sl.Value1);
                }
                if (sl.Key2 != null)
                {
                    array[index++] = KeyValuePair.Create(sl.Key2, sl.Value2);
                }
                if (sl.Key3 != null)
                {
                    array[index++] = KeyValuePair.Create(sl.Key3, sl.Value3);
                }
                if (sl.Key4 != null)
                {
                    array[index++] = KeyValuePair.Create(sl.Key4, sl.Value4);
                }
                if (sl.Key5 != null)
                {
                    array[index++] = KeyValuePair.Create(sl.Key5, sl.Value5);
                }
                if (sl.Key6 != null)
                {
                    array[index++] = KeyValuePair.Create(sl.Key6, sl.Value6);
                }
                if (sl.Key7 != null)
                {
                    array[index++] = KeyValuePair.Create(sl.Key7, sl.Value7);
                }
                if (sl.Key8 != null)
                {
                    array[index++] = KeyValuePair.Create(sl.Key8, sl.Value8);
                }
                if (sl.Key9 != null)
                {
                    array[index++] = KeyValuePair.Create(sl.Key9, sl.Value9);
                }
                Array.Resize(ref array, index);
                l.Arguments = array;
            });
            cfg.CreateMap<SqlLogSetting, LogSetting>()
                .ForMember(dest => dest.LogLevel, opts => opts.Ignore())
                .ForMember(dest => dest.LogLevelString, opts => opts.MapFrom(src => src.LogLevel));

            return cfg;
        }

        /// <summary>
        ///   Il mappatore della libreria DataAccess.
        /// </summary>
        public static IMapper Mapper = new MapperConfiguration(cfg => SetMappings(CaravanCommonConfig.SetMappings(cfg))).CreateMapper();

        #endregion AutoMapper
    }
}

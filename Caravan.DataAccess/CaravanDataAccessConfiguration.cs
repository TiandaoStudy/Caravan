using AutoMapper;
using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Logging.Models;
using Finsa.Caravan.Common.Security.Models;
using Finsa.Caravan.DataAccess.Sql.Logging.Entities;
using Finsa.Caravan.DataAccess.Sql.Security.Entities;
using Finsa.CodeServices.Common;
using Finsa.CodeServices.Common.Portability;
using System;
using System.Collections.Generic;
using System.Linq;
using Westwind.Utilities.Configuration;

namespace Finsa.Caravan.DataAccess
{
    /// <summary>
    ///   Configuration class for Caravan.DataAccess. Default values are set inside the
    ///   configuration file itself.
    /// </summary>
    public sealed class CaravanDataAccessConfiguration : AppConfiguration
    {
        #region Static instance

        /// <summary>
        ///   Gets the static configuration instance.
        /// </summary>
        /// <value>The static configuration instance.</value>
        public static CaravanDataAccessConfiguration Instance { get; } = InitializeInstance();

        private static CaravanDataAccessConfiguration InitializeInstance()
        {
            var configurationFile = "Caravan.config";
            if (PortableEnvironment.AppIsRunningOnAspNet)
            {
                // If application is running on ASP.NET, then we look for the configuration file
                // inside the root of the project. Usually, configuration file are not stored into
                // the "bin" directory, because every change would make the application restart.
                configurationFile = "~/" + configurationFile;
            }

            var instance = new CaravanDataAccessConfiguration();
            instance.Initialize(new ConfigurationFileConfigurationProvider<CaravanDataAccessConfiguration>
            {
                ConfigurationFile = PortableEnvironment.MapPath(configurationFile),
                ConfigurationSection = "caravan.dataAccess"
            });

            OnStart();

            return instance;
        }

        #endregion Static instance

        /// <summary>
        ///   Initializes a new instance of the <see cref="CaravanCommonConfiguration"/> class and
        ///   sets the default values for each configuration entry.
        /// </summary>
        public CaravanDataAccessConfiguration()
        {
            ConnectionString = string.Empty;
            DataSourceKind = CaravanDataSourceKind.Effort;

            // SQL
            SqlSchema = "mydb";
            SqlInitializer = "CreateDatabaseIfNotExists";

            // SQL - Oracle
            OracleStatementCacheSize = 10;

            // REST
            RestTestAuthObject = "_TEST_TOKEN_";
        }

        public string ConnectionString { get; set; }

        public CaravanDataSourceKind DataSourceKind { get; set; }

        /// <summary>
        ///   L'intervallo dopo il quale le impostazioni di log messe in cache devono essere aggiornate.
        /// 
        ///   L'intervallo di default è di 30 minuti.
        /// </summary>
        public TimeSpan Logging_SettingsCache_Interval { get; set; } = TimeSpan.FromMinutes(30);

        #region SQL

        public string SqlSchema { get; set; }

        public string SqlInitializer { get; set; }

        #region Oracle

        public int OracleStatementCacheSize { get; set; }

        #endregion Oracle

        #endregion SQL

        #region REST

        public string RestTestAuthObject { get; set; }

        #endregion REST

        #region MongoDB

        public string MongoDbName { get; set; }

        #endregion MongoDB

        #region OnStart

        private static void OnStart()
        {
            // Mappings (SQL --> Models) for Security
            Mapper.CreateMap<SqlSecApp, SecApp>();
            Mapper.CreateMap<SqlSecClaim, SecClaim>();
            Mapper.CreateMap<SqlSecContext, SecContext>();
            Mapper.CreateMap<SqlSecEntry, SecEntry>().AfterMap((se, e) =>
            {
                e.ContextName = se.Object.Context.Name;
            });
            Mapper.CreateMap<SqlSecGroup, SecGroup>();
            Mapper.CreateMap<SqlSecObject, SecObject>();
            Mapper.CreateMap<SqlSecRole, SecRole>().AfterMap((sr, r) =>
            {
                r.AppName = sr.Group.App.Name;
            });
            Mapper.CreateMap<SqlSecUser, SecUser>()
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
            Mapper.CreateMap<SqlLogEntry, LogEntry>().AfterMap((sl, l) =>
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
            Mapper.CreateMap<SqlLogSetting, LogSetting>()
                .ForMember(dest => dest.LogLevel, opts => opts.Ignore())
                .ForMember(dest => dest.LogLevelString, opts => opts.MapFrom(src => src.LogLevel));
        }

        #endregion OnStart
    }
}
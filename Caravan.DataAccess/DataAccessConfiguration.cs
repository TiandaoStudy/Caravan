using System;
using System.Collections.Generic;
using AutoMapper;
using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Models.Logging;
using Finsa.Caravan.Common.Models.Security;
using Finsa.Caravan.DataAccess.Drivers.Sql.Models.Logging;
using Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security;
using Finsa.CodeServices.Common;
using Finsa.CodeServices.Common.Portability;
using Westwind.Utilities.Configuration;

namespace Finsa.Caravan.DataAccess
{
    /// <summary>
    ///   Configuration class for Caravan.DataAccess. Default values are set inside the
    ///   configuration file itself.
    /// </summary>
    public sealed class DataAccessConfiguration : AppConfiguration
    {
        #region Static instance

        private static readonly DataAccessConfiguration CachedInstance;

        static DataAccessConfiguration()
        {
            var configurationFile = "Caravan.config";
            if (PortableEnvironment.AppIsRunningOnAspNet)
            {
                // If application is running on ASP.NET, then we look for the configuration file
                // inside the root of the project. Usually, configuration file are not stored into
                // the "bin" directory, because every change would make the application restart.
                configurationFile = "~/" + configurationFile;
            }
            CachedInstance = new DataAccessConfiguration();
            CachedInstance.Initialize(new ConfigurationFileConfigurationProvider<DataAccessConfiguration>
            {
                ConfigurationFile = PortableEnvironment.MapPath(configurationFile),
                ConfigurationSection = "caravan.dataAccess"
            });
            OnStart();
        }

        /// <summary>
        ///   Gets the static configuration instance.
        /// </summary>
        /// <value>The static configuration instance.</value>
        public static DataAccessConfiguration Instance
        {
            get { return CachedInstance; }
        }

        #endregion
        
        /// <summary>
        ///   Initializes a new instance of the <see cref="CommonConfiguration"/> class and sets the
        ///   default values for each configuration entry.
        /// </summary>
        public DataAccessConfiguration()
        {
            ConnectionString = String.Empty;
            DataSourceKind = CaravanDataSourceKind.FakeSql;

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

        #region SQL

        public string SqlSchema { get; set; }

        public string SqlInitializer { get; set; }

        #region Oracle

        public int OracleStatementCacheSize { get; set; }

        #endregion

        #endregion

        #region REST

        public string RestTestAuthObject { get; set; }

        #endregion

        #region MongoDB

        public string MongoDbName { get; set; }

        #endregion

        #region OnStart

        private static void OnStart()
        {
            // Mappings
            Mapper.CreateMap<SqlLogSetting, LogSetting>();
            Mapper.CreateMap<SqlSecApp, SecApp>();
            Mapper.CreateMap<SqlSecContext, SecContext>();
            Mapper.CreateMap<SqlSecEntry, SecEntry>().AfterMap((se, e) =>
            {
                e.ContextName = se.Object.Context.Name;
            });
            Mapper.CreateMap<SqlSecGroup, SecGroup>();
            Mapper.CreateMap<SqlSecObject, SecObject>();
            Mapper.CreateMap<SqlSecUser, SecUser>();
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
        }

        #endregion
    }
}

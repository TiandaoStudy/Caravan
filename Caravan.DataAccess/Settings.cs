using System;
using System.Collections.Generic;
using AutoMapper;
using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Models.Logging;
using Finsa.Caravan.Common.Models.Security;
using Finsa.Caravan.DataAccess.Drivers.Sql.Models.Logging;
using Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security;

namespace Finsa.Caravan.DataAccess.Properties
{
    // This class allows you to handle specific events on the settings class:
    //  The SettingChanging event is raised before a setting's value is changed.
    //  The PropertyChanged event is raised after a setting's value is changed.
    //  The SettingsLoaded event is raised after the setting values are loaded.
    //  The SettingsSaving event is raised before the setting values are saved.
    public sealed partial class Settings
    {
        public Settings()
        {
            // To add event handlers for saving and changing settings, uncomment the lines below:
            //
            // this.SettingChanging += this.SettingChangingEventHandler;
            //
            // this.SettingsSaving += this.SettingsSavingEventHandler;
            //
        }

        static Settings()
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

        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e)
        {
            // Add code to handle the SettingChangingEvent event here.
        }

        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Add code to handle the SettingsSaving event here.
        }
    }
}
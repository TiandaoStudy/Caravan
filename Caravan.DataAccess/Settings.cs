﻿using AutoMapper;
using Finsa.Caravan.Common.Models.Logging;
using Finsa.Caravan.Common.Models.Security;
using Finsa.Caravan.DataAccess.Sql.Models.Logging;
using Finsa.Caravan.DataAccess.Sql.Models.Security;

namespace Finsa.Caravan.DataAccess.Properties {
    
    
    // This class allows you to handle specific events on the settings class:
    //  The SettingChanging event is raised before a setting's value is changed.
    //  The PropertyChanged event is raised after a setting's value is changed.
    //  The SettingsLoaded event is raised after the setting values are loaded.
    //  The SettingsSaving event is raised before the setting values are saved.
    public sealed partial class Settings {
        
        public Settings() {
            // // To add event handlers for saving and changing settings, uncomment the lines below:
            //
            // this.SettingChanging += this.SettingChangingEventHandler;
            //
            // this.SettingsSaving += this.SettingsSavingEventHandler;
            //
        }

        static Settings()
        {
            // Mappings
            Mapper.CreateMap<SqlLogEntry, LogEntry>();
            Mapper.CreateMap<SqlLogSetting, LogSetting>();
            Mapper.CreateMap<SqlSecApp, SecApp>();
            Mapper.CreateMap<SqlSecContext, SecContext>();
            Mapper.CreateMap<SqlSecEntry, SecEntry>();
            Mapper.CreateMap<SqlSecGroup, SecGroup>();
            Mapper.CreateMap<SqlSecObject, SecObject>();
            Mapper.CreateMap<SqlSecUser, SecUser>();
        }
        
        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e) {
            // Add code to handle the SettingChangingEvent event here.
        }
        
        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) {
            // Add code to handle the SettingsSaving event here.
        }
    }
}

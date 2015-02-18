using System;
using System.Globalization;
using System.Web;

namespace Finsa.Caravan.WebForms.Properties {
    
    
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
        
        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e) {
            // Add code to handle the SettingChangingEvent event here.
        }
        
        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) {
            // Add code to handle the SettingsSaving event here.
        }

        #region Internal Settings

        internal CultureInfo CurrentUserCulture
        {
           get { return CultureInfo.CreateSpecificCulture("it-IT"); }
        }

        internal TimeSpan DefaultIntervalForVolatile
        {
           get { return TimeSpan.FromMinutes((HttpContext.Current.Session != null) ? HttpContext.Current.Session.Timeout * 2 : 30); }
        }

        internal string ExceptionSessionKey
        {
           get { return "C895F535-DA46-478c-ACD3-9E21B69A76D6"; }
        }

        #endregion
    }
}

using System;
using System.Linq;
using Finsa.Caravan;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.DataModel.Logging;
using Finsa.Caravan.DataModel.Security;
using NUnit.Framework;

namespace UnitTests.DataAccess
{
   [TestFixture]
   public class LogManagerTests
   {
      private SecApp _myApp;
      private SecApp _myApp2;
      private LogSettings _settingError;

      [SetUp]
      public void Init()
      {
         Db.ClearAllTablesUseOnlyInsideUnitTestsPlease();
           _myApp = new SecApp {Name = "unittests.dataaccess", Description = "Test Application 1"};
           Db.Security.AddApp(_myApp);
           _myApp2 = new SecApp {Name = "mio_test2", Description = "Test Application 2"};
           Db.Security.AddApp(_myApp2);
           _settingError = new LogSettings() { Days = 30, Enabled = 1, MaxEntries = 100 };

           Db.Logger.AddSettings(_myApp.Name, LogType.Error, _settingError);
           Db.Logger.AddSettings(_myApp.Name, LogType.Fatal, _settingError);
           Db.Logger.AddSettings(_myApp.Name, LogType.Info, _settingError);
           Db.Logger.AddSettings(_myApp.Name, LogType.Debug, _settingError);
           Db.Logger.AddSettings(_myApp.Name, LogType.Warn, _settingError);
      }

      [TearDown]
      public void CleanUp()
      {
      }

      #region Log Settings
      [Test]
      [ExpectedException(typeof (ArgumentException))]
      public void LogSettings_NullAppNameValidLogType_ThrowsArgumentException()
      {
         Db.Logger.LogSettings(null, LogType.Error);
      }

      [Test]
      public void LogSettings_NoArgs_ReturnListOfAllApps()
      {
         var settings = Db.Logger.LogSettings();

         Assert.That(settings.Count,Is.EqualTo(5));
      }

      
      [Test]
      public void LogSettings_ValidAppName_ReturnsListForAppName()
      {
         var settings = Db.Logger.LogSettings(_myApp.Name);

         Assert.That(settings.Count, Is.EqualTo(5));
      }

      [Test]
      public void LogSettings_ValidArgs_Returns()
      {
         var result = Db.Logger.LogInfo<LogManagerTests>("pino", "pino pino", "test", new[]
         {
            CKeyValuePair.Create("arg1", "1"),
            CKeyValuePair.Create("arg2", "2"),
         });

         var q = Db.Logger.Logs(_myApp.Name).Where(l => l.CodeUnit == "unittests.dataaccess.logmanagertests" && l.ShortMessage == "pino").ToList();

         Assert.That(q.Count(),Is.EqualTo(1));
         Assert.That(q.First().Arguments[0].Key, Is.EqualTo("arg1"));
         Assert.That(q.First().Arguments[0].Value, Is.EqualTo("1"));
         Assert.That(q.First().Arguments[1].Key, Is.EqualTo("arg2"));
         Assert.That(q.First().Arguments[1].Value, Is.EqualTo("2"));
      }

      [Test]
      public void UpdateSetting_ValidArgs_SettingUpdated()
      {
         var update = new LogSettings { Days = 40, Enabled = 1,MaxEntries = 50};
         Db.Logger.UpdateSettings(_myApp.Name,LogType.Info,update);

         var q = Db.Logger.LogSettings(_myApp.Name).Where(s => s.AppId == update.AppId && s.Type==LogType.Info).ToList();
         Assert.That(q.Count, Is.EqualTo(1));
         Assert.That(q.First().MaxEntries,Is.EqualTo(50));
         Assert.That(q.First().Days,Is.EqualTo(40));
      }

      [Test]
      [ExpectedException(typeof (ArgumentException))]
      public void UpdateSetting_EmptyAppName_Throws()
      {
         var update = new LogSettings { Days = 40, Enabled = 1, MaxEntries = 50 };
         Db.Logger.UpdateSettings("", LogType.Info, update);
      }

      [Test]
      [ExpectedException(typeof (ArgumentNullException))]
      public void Updatesettings_NullSetting_throws()
      {
         Db.Logger.UpdateSettings(_myApp.Name, LogType.Info, null);
      }

      #endregion

      #region Logging Methods

      //LogResult Log(LogType type, string appName, string userName, string codeUnit, string function, Exception exception, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null);

      [Test]
      public void Log_validArgs_()
      {
         
      }

      [Test]
      [ExpectedException(typeof (ArgumentNullException))]
      public void Log_NullException_Throws()
      {
         var res = Db.Logger.Log(LogType.Info, "", "", "", "", null);
      }
     
      #endregion
   }
}
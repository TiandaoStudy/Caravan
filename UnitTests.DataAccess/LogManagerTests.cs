using System;
using System.Linq;
using System.Threading.Tasks;
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
            KeyValuePair.Create("arg1", "1"),
            KeyValuePair.Create("arg2", "2"),
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

         var q = Db.Logger.LogSettings(_myApp.Name).Where(s => s.AppId == _myApp.Id && s.Type==LogType.Info).ToList();
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

      [Test]
      public void Log_validArgs_()
      {
         var res = Db.Logger.LogRaw(LogType.Info, _myApp.Name, "", "UnitTests.DataAccess.LogManagerTests", "Log_validArgs_", new Exception() );
         Assert.True(res.Succeeded);

         var q = Db.Logger.Logs(_myApp.Name).Where(l => l.CodeUnit == "unittests.dataaccess.logmanagertests");
         Assert.That(q.Count(),Is.EqualTo(1));

         var q1 = Db.Logger.Logs(_myApp.Name).Where(l => l.Function == "log_validargs_");
         Assert.That(q1.Count(), Is.EqualTo(1));
      }

      [Test]
      public void Log_NullException_ReturnsFalse()
      {
         var res = Db.Logger.LogRaw(LogType.Info, "", "", "", "", exception: null);
         Assert.That(res.Succeeded,Is.EqualTo(false));
      }

      [Test]
      public void Log_EmptyCodeUnit_ReturnsFalse()
      {
         var res = Db.Logger.LogRaw(LogType.Info, "", "", "", "", new Exception());
        Assert.False(res.Succeeded);
      
      }
     
      [Test]
      public void LogWithCodeUnit_validArgs()
      {
         var res = Db.Logger.Log<LogManagerTests>(LogType.Error, new Exception());
          Assert.True(res.Succeeded);

         var q = Db.Logger.Logs(LogType.Error).Where(l => l.Function == "logwithcodeunit_validargs");

         Assert.That(q.Count(),Is.EqualTo(1));
      }

      [Test]
      public void LogWithCodeUnit_EmptyShortMessage_ReturnsFalse()
      {
         var res = Db.Logger.Log<LogManagerTests>(LogType.Error,"");
          Assert.False(res.Succeeded);
         
      }

      [Test]
      public void LogDebug_validArgs()
      {
         var res = Db.Logger.LogDebug<LogManagerTests>(new Exception());
         Assert.True(res.Succeeded);

         var q = Db.Logger.Logs(LogType.Debug).Where(l => l.Function == "logdebug_validargs");

         Assert.That(q.Count(), Is.EqualTo(1));
      }

      [Test]
      public void LogError_validArgs()
      {
         var res = Db.Logger.LogError<LogManagerTests>(new Exception());
         Assert.True(res.Succeeded);

         var q = Db.Logger.Logs(LogType.Error).Where(l => l.Function == "logerror_validargs");

         Assert.That(q.Count(), Is.EqualTo(1));
      }

      [Test]
      public void LogWarn_validArgs()
      {
         var res = Db.Logger.LogWarn<LogManagerTests>(new Exception());
         Assert.True(res.Succeeded);

         var q = Db.Logger.Logs(LogType.Warn).Where(l => l.Function == "logwarn_validargs");

         Assert.That(q.Count(), Is.EqualTo(1));
      }

      [Test]
      public void LogInfo_validArgs()
      {
         var res = Db.Logger.LogInfo<LogManagerTests>(new Exception());
         Assert.True(res.Succeeded);

         var q = Db.Logger.Logs(LogType.Info).Where(l => l.Function == "loginfo_validargs");

         Assert.That(q.Count(), Is.EqualTo(1));
      }

      [Test]
      public void LogFatal_validArgs()
      {
         var res = Db.Logger.LogFatal<LogManagerTests>(new Exception());
         Assert.True(res.Succeeded);

         var q = Db.Logger.Logs(LogType.Fatal).Where(l => l.Function == "logfatal_validargs");

         Assert.That(q.Count(), Is.EqualTo(1));
      }

      [Test]
      public void LogWarnAsync_validArgs()
      {
         var res = Db.Logger.LogWarnAsync<LogManagerTests>(new Exception());

         Task.WaitAll(res);

         Assert.IsTrue(res.Result.Succeeded);

      }

      [Test]
      public void LogErrorAsync_validArgs()
      {
         var tasks = new Task[2];
         tasks[0] = Db.Logger.LogErrorAsync<LogManagerTests>(new Exception());
         tasks[1] = Db.Logger.LogErrorAsync<LogManagerTests>(new Exception());

         var res1 = (Task <LogResult>) tasks[0];
         var res2 = (Task<LogResult>)tasks[1];

         Assert.True(res1.Result.Succeeded);
         Assert.True(res2.Result.Succeeded);

         Assert.IsTrue(tasks[0].Status==TaskStatus.RanToCompletion);
         Assert.IsTrue(tasks[0].Status == TaskStatus.RanToCompletion);
      }

      #endregion
   }
}
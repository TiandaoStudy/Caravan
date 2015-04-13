using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace Finsa.Caravan.DataAccess
{
    /// <summary>
    ///   Target per il log di Caravan su database.
    /// </summary>
    [Target("CaravanLog")]
    public sealed class DbLogTarget : Target
    {
        public DbLogTarget()
        {
            Function = new SimpleLayout("${callsite:className=false:methodName=true}");
        }

        [RequiredParameter]
        public Layout CodeUnit { get; set; }

        [RequiredParameter]
        public Layout Function { get; set; }

        protected override void Write(LogEventInfo logEvent)
        {
            var codeUnit = CodeUnit.Render(logEvent);
            var function = Function.Render(logEvent);
            //Db.Logger.LogRawAsync(

            //);
        } 
    }
}

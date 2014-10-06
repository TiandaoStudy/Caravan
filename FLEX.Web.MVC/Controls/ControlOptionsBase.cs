namespace FLEX.Web.MVC.Controls
{
   public abstract class ControlOptionsBase
   {
      protected const string JQueryNoop = "$.noop";

      public string ID { get; set; }

      internal string SafeID
      {
         get { return ID.Replace('-', '_'); }
      }
   }
}

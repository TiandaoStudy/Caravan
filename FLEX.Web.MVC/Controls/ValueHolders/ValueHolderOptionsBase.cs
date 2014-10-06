namespace FLEX.Web.MVC.Controls.ValueHolders
{
   public abstract class ValueHolderOptionsBase : ControlOptionsBase
   {
      protected ValueHolderOptionsBase()
      {
         // Default values
         OnChange = JQueryNoop;
         SearchCriteriaID = "default";
      }
      
      public string OnChange { get; set; }
      public string SearchCriteriaID { get; set; }
   }
}

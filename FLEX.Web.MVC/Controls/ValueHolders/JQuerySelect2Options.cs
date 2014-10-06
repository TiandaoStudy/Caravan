using System;

namespace FLEX.Web.MVC.Controls.ValueHolders
{
   public sealed class JQuerySelect2Options : ValueHolderOptionsBase
   {
      public JQuerySelect2Options()
      {
         // Default values
         AllowClear = false;
         MinimumInputLength = 1;
         PlaceHolder = String.Empty;
         QueryBuilder = JQueryNoop;
      }

      public bool AllowClear { get; set; }
      public string DataMapper { get; set; }
      public string DataSourceUrl { get; set; }
      public int MinimumInputLength { get; set; }
      public string PlaceHolder { get; set; }
      public string QueryBuilder { get; set; }
   }
}

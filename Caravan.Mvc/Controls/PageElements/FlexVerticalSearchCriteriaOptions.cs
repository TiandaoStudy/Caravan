using System;

namespace Finsa.Caravan.Mvc.Controls.PageElements
{
   public sealed class FlexVerticalSearchCriteriaOptions : ControlOptionsBase
   {
   }

   public sealed class FlexVerticalSearchCriteriumOptions : ControlOptionsBase
   {
      public string Label { get; set; }
      public Func<dynamic> Control { get; set; } 
   }
}

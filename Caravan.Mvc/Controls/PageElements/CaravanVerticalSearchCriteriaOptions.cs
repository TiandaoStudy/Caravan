using System;

namespace Finsa.Caravan.Mvc.Controls.PageElements
{
   public sealed class CaravanVerticalSearchCriteriaOptions : ControlOptionsBase
   {
   }

   public sealed class CaravanVerticalSearchCriteriumOptions : ControlOptionsBase
   {
      public string Label { get; set; }
      public Func<dynamic> Control { get; set; } 
   }
}

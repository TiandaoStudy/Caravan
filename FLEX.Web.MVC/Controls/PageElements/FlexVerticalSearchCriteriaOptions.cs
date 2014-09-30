using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLEX.Web.MVC.Controls.PageElements
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

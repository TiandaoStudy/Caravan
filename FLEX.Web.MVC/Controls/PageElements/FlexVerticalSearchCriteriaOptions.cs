using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages;

namespace FLEX.Web.MVC.Controls.PageElements
{
   public sealed class FlexVerticalSearchCriteriaOptions
   {
      public string Label { get; set; }
      public Func<dynamic> Control { get; set; } 
   }
}

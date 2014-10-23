using System;
using System.Collections.Generic;

namespace Finsa.Caravan.Mvc.Controls.DataVisualization
{
   public sealed class CaravanDataGridOptions : ControlOptionsBase
   {
      //public IPagedList<object> PagedItems { get; set; }
      public Func<int, string> PagerAction { get; set; }
      public ICollection<CaravanDataGridColumnOptions> Columns { get; set; }
   }

   public sealed class CaravanDataGridColumnOptions
   {
      public string Header { get; set; }
      public Func<dynamic, dynamic> Control { get; set; } 
   }
}

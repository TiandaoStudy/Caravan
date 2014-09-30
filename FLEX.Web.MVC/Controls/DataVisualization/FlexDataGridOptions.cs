using System.Collections.Generic;
using PagedList;

namespace FLEX.Web.MVC.Controls.DataVisualization
{
   public sealed class FlexDataGridOptions : ControlOptionsBase
   {
      public IPagedList PagedItems { get; set; }
      public ICollection<FlexDataGridColumnOptions> Columns { get; set; }
   }

   public sealed class FlexDataGridColumnOptions
   {
      public string Header { get; set; }
   }
}

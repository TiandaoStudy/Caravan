using System;
using System.Collections.Generic;
using Finsa.Caravan.Common.Models.Formatting;

namespace Finsa.Caravan.Mvc.Controls.DataVisualization
{
   public sealed class BootstrapTableOptions : ControlOptionsBase
   {
      public BootstrapTableOptions()
      {
         ShowExportButton = true;
         ShowPagination = true;
         ShowSearchBox = true;
      }

      public ICollection<BootstrapTableColumnOptions> Columns { get; set; }
      public IEnumerable<dynamic> Items { get; set; }
      public bool ShowExportButton { get; set; }
      public bool ShowPagination { get; set; }
      public bool ShowSearchBox { get; set; }
      public string UpdateAction { get; set; }
   }

   public sealed class BootstrapTableColumnOptions : ControlOptionsBase
   {
      public BootstrapTableColumnOptions()
      {
         // Default values
         Header = String.Empty;
         HorizontalAlignment = HorizonalTextAlignment.Left;
         Sortable = false;
         VerticalAlignment = VerticalTextAlignment.Middle;
      }
      
      public Func<dynamic, dynamic> Control { get; set; }
      public string Header { get; set; }
      public HorizonalTextAlignment HorizontalAlignment { get; set; }
      public bool Sortable { get; set; }
      public VerticalTextAlignment VerticalAlignment { get; set; }

      #region Pseudo-Internal Properties

      public string HAlign
      {
         get
         {
            switch (HorizontalAlignment)
            {
               case HorizonalTextAlignment.Left:
                  return "left";
               case HorizonalTextAlignment.Center:
                  return "center";
               case HorizonalTextAlignment.Right:
                  return "right";
               default:
                  throw new NotSupportedException();
            }
         }
      }

      public string VAlign
      {
         get
         {
            switch (VerticalAlignment)
            {
               case VerticalTextAlignment.Top:
                  return "top";
               case VerticalTextAlignment.Middle:
                  return "center";
               case VerticalTextAlignment.Bottom:
                  return "bottom";
               default:
                  throw new NotSupportedException();
            }
         }
      }

      #endregion
   }
}

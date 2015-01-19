using System;

namespace Finsa.Caravan.Mvc.Controls.ValueHolders
{
   /// <summary>
   ///   http://bootstrap-datepicker.readthedocs.org/en/release/options.html
   /// </summary>
   public sealed class BootstrapDatePickerOptions : ValueHolderOptionsBase
   {
      public BootstrapDatePickerOptions()
      {
         // Default values
         Autoclose = false;
         DateFormat = "dd/mm/yyyy";
         MinView = MinViewMode.Days;
         StartView = StartViewMode.Month;
         StartDate = "01/01/2000";
         EndDate = "31/12/2999";
         
      }

      public bool Autoclose { get; set; }

      public bool ClearBtn { get; set; }

      public string DateFormat { get; set; }

      public string StartDate { get; set; }

      public string EndDate { get; set; }

      public StartViewMode? StartView{ get; set; }
   
      public MinViewMode? MinView{ get; set; }

      public enum StartViewMode : byte
      {
         Month = 0,
         Year = 1,
         Decade = 2
      }

      public enum MinViewMode : byte
      {
         Days = 0,
         Months = 1,
         Years = 2
      }

      public DateTime SelectedDate { get; set; }

      public string ChangeDate { get; set; }
   }
}

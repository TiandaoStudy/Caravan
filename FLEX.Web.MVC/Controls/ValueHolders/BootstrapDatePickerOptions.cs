using System;

namespace FLEX.Web.MVC.Controls.ValueHolders
{
   /// <summary>
   ///   http://bootstrap-datepicker.readthedocs.org/en/release/options.html
   /// </summary>
   public sealed class BootstrapDatePickerOptions
   {
      public BootstrapDatePickerOptions()
      {
         // Default values
         State = false;
         Size = SwitchSize.Normal;
      }

      public string ID { get; set; }

      public bool State { get; set; }

      public SwitchSize Size { get; set; }

      public string OnChange { get; set; }

      internal string SizeString 
      {
         get
         {
            switch (Size)
            {
               case SwitchSize.Mini:
                  return "mini";
               case SwitchSize.Small:
                  return "small";
               case SwitchSize.Normal:
                  return "normal";
               case SwitchSize.Large:
                  return "large";
               default:
                  throw new InvalidOperationException();
            }
         }
      }

      public enum SwitchSize : byte
      {
         Mini,
         Small,
         Normal,
         Large
      }
   }
}

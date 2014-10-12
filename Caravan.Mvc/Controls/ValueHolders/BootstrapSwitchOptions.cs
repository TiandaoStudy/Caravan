using System;

namespace FLEX.Web.MVC.Controls.ValueHolders
{
   public sealed class BootstrapSwitchOptions : ValueHolderOptionsBase
   {
      public BootstrapSwitchOptions()
      {
         // Default values
         State = false;
         Size = SwitchSize.Normal;
      }

      public bool State { get; set; }

      public SwitchSize Size { get; set; }

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

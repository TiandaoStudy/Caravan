using System;
using System.Diagnostics;
using System.Web.UI;

namespace FLEX.Web.WebControls.Templates
{
   internal class ControlTemplate<TControl> : ITemplate where TControl : Control, new()
   {
      private static readonly Action<TControl> NoInit = c => { /* Empty */ };
      private readonly Action<TControl> _initControl;

      public ControlTemplate(Action<TControl> initControl)
      {
         Debug.Assert(initControl != null);
         _initControl = initControl;
      }

      public ControlTemplate() : this(NoInit)
      {
         // Empty, for now...
      }

      public void InstantiateIn(Control container)
      {
         Debug.Assert(container != null);
         var userControl = new TControl();
         _initControl(userControl);
         container.Controls.Add(userControl);
      }
   }
}

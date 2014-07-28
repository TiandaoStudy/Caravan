using System;
using System.Web.UI;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls.Ajax
// ReSharper restore CheckNamespace
{
   /// <summary>
   ///   TODO
   /// </summary>
   public abstract class AjaxControlBase : UserControl
   {
      private const string DoPostBackViewStateKey = "AjaxControlBase.DoPostBack";
      private const string EnabledViewStateKey = "AjaxControlBase.Enabled";

      private const bool DoPostBackDefaultValue = false;
      private const bool EnabledDefaultValue = true;

      #region Public Properties

      /// <summary>
      ///   TODO
      /// </summary>
      public bool DoPostBack
      {
         get
         {
            if (ViewState[DoPostBackViewStateKey] == null)
            {
               ViewState[DoPostBackViewStateKey] = DoPostBackDefaultValue;
            }
            return (bool) ViewState[DoPostBackViewStateKey];
         }
         set
         {
            ViewState[DoPostBackViewStateKey] = value;
            OnDoPostBackChanged(value);
         }
      }

      /// <summary>
      ///   TODO
      /// </summary>
      public bool Enabled
      {
         get
         {
            if (ViewState[EnabledViewStateKey] == null)
            {
               ViewState[EnabledViewStateKey] = EnabledDefaultValue;
            }
            return (bool) ViewState[EnabledViewStateKey];
         }
         set
         {
            ViewState[EnabledViewStateKey] = value;
            OnEnabledChanged(value);
         }
      }

      #endregion

      protected virtual void Page_Load(object sender, EventArgs e)
      {
         // Automatically uses default values...
         OnDoPostBackChanged(DoPostBack);
         OnEnabledChanged(Enabled);
      }

      protected virtual void OnDoPostBackChanged(bool doPostBack)
      {
         // Empty...
      }

      protected virtual void OnEnabledChanged(bool enabled)
      {
         // Empty...
      }
   }
}

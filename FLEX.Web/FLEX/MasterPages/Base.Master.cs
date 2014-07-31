﻿using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using FLEX.Web.UserControls;
using FLEX.Web.UserControls.Ajax;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.MasterPages
// ReSharper restore CheckNamespace
{
   public partial class Base : MasterPage, IPageBase
   {
      protected void Page_Init(object sender, EventArgs e)
      {
         flexHiddenTrigger.Triggered += flexHiddenTrigger_Triggered;
      }

      public ErrorHandler ErrorHandler
      {
         get { return Master.ErrorHandler; }
      }

      public HtmlForm MainForm
      {
         get { return Master.MainForm; }
      }

      public MenuBar MenuBar
      {
         get { return flexMenuBar; }
      }

      public PageFooter PageFooter
      {
         get { return flexPageFooter; }
      }

      public bool HasPageVisibleHandlers
      {
         get { return Page_Visible != null; }
      }

      public ScriptManager ScriptManager
      {
         get { return Master.ScriptManager; }
      }

      public event EventHandler Page_Visible;

      private void flexHiddenTrigger_Triggered(object sender, EventArgs args)
      {
         if (Page_Visible != null)
         {
            Page_Visible(this, new EventArgs());
         }
      }
   }
}
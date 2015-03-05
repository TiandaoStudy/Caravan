﻿using System;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.WebForms.Properties;
using FLEX.WebForms;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls
// ReSharper restore CheckNamespace
{
   public partial class PageFooter : ControlBase
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         try
         {
            // Footer extender
            var ext = LoadControl(Settings.Default.ControlsExtendersFolder + "/PageFooter.ascx");
            footerExtender.Controls.Add(ext);

            // ...
            if (IsPostBack) return;
            
            rptFooterInfo.DataSource = PageManager.Instance.GetFooterInfo();
            rptFooterInfo.DataBind();
         }
         catch (Exception ex)
         {
            Db.Logger.LogError<PageFooter>(ex);
            throw;
         }
      }
   }
}
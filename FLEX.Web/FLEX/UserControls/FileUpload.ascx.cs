using System;
using FLEX.Common.Data;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls
// ReSharper restore CheckNamespace
{
   /// <summary>
   ///   TODO
   /// </summary>
   public partial class FileUpload : ControlBase
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         try
         {
            // Empty, for now...
         }
         catch (Exception ex)
         {
            DbLogger.Instance.LogError<FileUpload>("Page_Load", ex);
            throw;
         }
      }

      #region Public Properties

      /// <summary>
      ///   TODO
      /// </summary>
      public string OnClientClick
      {
         get { return inputFileUpload.Attributes["onclick"]; }
         set { inputFileUpload.Attributes["onclick"] = value; }
      }

      /// <summary>
      ///   TODO
      /// </summary>
      public string Title
      {
         get { return inputFileUpload.Attributes["title"]; }
         set { inputFileUpload.Attributes["title"] = value; }
      }

      #endregion
   }
}
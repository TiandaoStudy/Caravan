using System;
using System.Web.UI.HtmlControls;
using Finsa.Caravan.DataAccess;

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
            DataSource.Logger.LogError<FileUpload>(ex);
            throw;
         }
      }

      #region Public Properties

      public bool AllowMultiple
      {
         get { return inputFileUpload.Attributes["multiple"] == "multiple"; }
         set
         {
            if (value)
            {
               inputFileUpload.Attributes["multiple"] = "multiple";
            }
            else
            {
               inputFileUpload.Attributes.Remove("multiple");
            }
         }
      }

      /// <summary>
      ///   TODO
      /// </summary>
      public HtmlInputFile Input
      {
         get { return inputFileUpload; }
      }

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
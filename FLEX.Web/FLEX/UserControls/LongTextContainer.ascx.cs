using System;
using System.Web.UI;
using Thrower;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls
// ReSharper restore CheckNamespace
{
   public partial class LongTextContainer : UserControl
   {
      private string _text = String.Empty;

      #region Public Properties

      public string ContainerTitle { get; set; }

      public int MaxTextLength { get; set; }

      public string ShortenedText { get; private set; }

      public string Text
      {
         get { return _text; }
         set
         {
            Raise<ArgumentNullException>.IfIsNull(value, "Text property cannot be null");           
            if (value.Length <= MaxTextLength)
            {
               // Nothing to do, text fits the container.
               ShortenedText = value.Replace(Environment.NewLine, "");
            }
            else
            {
               ShortenedText = value.Substring(0, MaxTextLength).Replace(Environment.NewLine, "") + "...";
            }
            _text = value.Replace(Environment.NewLine, "<br/>"); // Text should not have normal line breaks (it is HTML).
         }
      }

      #endregion
   }
}
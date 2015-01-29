using PommaLabs.Diagnostics;
using System;
using PommaLabs.Extensions;

// ReSharper disable CheckNamespace This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls
// ReSharper restore CheckNamespace
{
    public partial class LongTextContainer : ControlBase
    {
        private const string TextViewStateKey = "LongTextContainer.Text";

        #region Public Properties

        public string ContainerTitle { get; set; }

        public int MaxTextLength { get; set; }

        public string ShortenedText { get; private set; }

        public string Text
        {
            get { return (string) (ViewState[TextViewStateKey] ?? ""); }
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
                ViewState[TextViewStateKey] = value.Replace(Environment.NewLine, "<br/>"); // Text should not have normal line breaks (it is HTML).
            }
        }

        public string EscapedText
        {
            get { return Text.HtmlEncode().Replace(Environment.NewLine, "<br/>"); }
        }

        #endregion Public Properties
    }
}
using System;
using Finsa.CodeServices.Common.Diagnostics;

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
            get { return (string) (ViewState[TextViewStateKey] ?? String.Empty); }
            set
            {
                Raise<ArgumentNullException>.IfIsNull(value, "Text property cannot be null");
                if (value.Length <= MaxTextLength)
                {
                    // Nothing to do, text fits the container.
                    ShortenedText = value.Replace(Environment.NewLine, String.Empty);
                }
                else
                {
                    ShortenedText = value.Substring(0, MaxTextLength).Replace(Environment.NewLine, String.Empty) + "...";
                }
                ViewState[TextViewStateKey] = value;
            }
        }

        #endregion Public Properties
    }
}
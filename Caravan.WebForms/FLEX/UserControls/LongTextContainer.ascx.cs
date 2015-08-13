using System;
using PommaLabs.Thrower;

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
            get { return (string) (ViewState[TextViewStateKey] ?? string.Empty); }
            set
            {
                Raise<ArgumentNullException>.IfIsNull(value, "Text property cannot be null");
                if (value.Length <= MaxTextLength)
                {
                    // Nothing to do, text fits the container.
                    ShortenedText = value.Replace(Environment.NewLine, string.Empty);
                }
                else
                {
                    ShortenedText = value.Substring(0, MaxTextLength).Replace(Environment.NewLine, string.Empty) + "...";
                }
                ViewState[TextViewStateKey] = value;
            }
        }

        #endregion Public Properties
    }
}
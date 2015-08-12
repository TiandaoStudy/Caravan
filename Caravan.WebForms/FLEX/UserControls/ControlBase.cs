using System;
using System.Globalization;
using System.Text;
using System.Web.UI;
using Finsa.CodeServices.Common.Extensions;
using FLEX.Web.MasterPages;

// ReSharper disable CheckNamespace This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls
// ReSharper restore CheckNamespace
{
    public abstract class ControlBase : UserControl
    {
        public IHead Master
        {
            get { return Page.Master as IHead; }
        }

        public string JQueryID()
        {
            return JQueryID(this);
        }

        public string JQueryID(Control control)
        {
            return string.Format("\"#{0}\"", control.ClientID);
        }

        protected static string EncodeJsNumber(decimal d)
        {
            return Convert.ToString(d, CultureInfo.InvariantCulture);
        }

        protected static string EncodeJsNumber(decimal? d)
        {
            return d.HasValue ? EncodeJsNumber(d.Value) : string.Empty;
        }

        protected static string EncodeJsNumber(int i)
        {
            return Convert.ToString(i, CultureInfo.InvariantCulture);
        }

        protected static string EncodeJsNumber(int? i)
        {
            return i.HasValue ? EncodeJsNumber(i.Value) : string.Empty;
        }

        protected static string HtmlEncode(string text)
        {
            return text.HtmlEncode();
        }

        protected internal static string ToJavaScriptString(string s)
        {
            return ToJavaScriptString(s, false);
        }

        protected static string ToJavaScriptString(string s, bool useSingleQuotes)
        {
            var delimiter = useSingleQuotes ? '\'' : '\"';
            var sb = new StringBuilder();
            sb.Append(delimiter);
            foreach (char c in s)
            {
                switch (c)
                {
                    case '\'':
                        sb.Append("\\\'");
                        break;

                    case '\"':
                        sb.Append("\\\"");
                        break;

                    case '\\':
                        sb.Append("\\\\");
                        break;

                    case '\b':
                        sb.Append("\\b");
                        break;

                    case '\f':
                        sb.Append("\\f");
                        break;

                    case '\n':
                        sb.Append("\\n");
                        break;

                    case '\r':
                        sb.Append("\\r");
                        break;

                    case '\t':
                        sb.Append("\\t");
                        break;

                    default:
                        int i = c;
                        if (i < 32 || i > 127)
                        {
                            sb.AppendFormat("\\u{0:X04}", i);
                        }
                        else
                        {
                            sb.Append(c);
                        }
                        break;
                }
            }
            sb.Append(delimiter);
            return sb.ToString();
        }
    }
}

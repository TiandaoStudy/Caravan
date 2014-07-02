using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using FLEX.Web.UserControls.Ajax;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.MasterPages
// ReSharper restore CheckNamespace
{
   public partial class Head : MasterPage
   {
      private static readonly string CachedRootPath;
      private static readonly string CachedFlexPath;
      private static readonly string CachedMyFlexPath;
      private readonly Random _random = new Random();

      static Head()
      {
         CachedRootPath = FullyQualifiedApplicationPath;
         CachedFlexPath = CachedRootPath + "FLEX";
         CachedMyFlexPath = CachedRootPath + "MyFLEX";
      }

      #region Public Properties

      public static string RootPath
      {
         get { return CachedRootPath; }
      }

      public static string FlexPath
      {
         get { return CachedFlexPath; }
      }

      public static string MyFlexPath
      {
         get { return CachedMyFlexPath; }
      }

      public ErrorHandler ErrorHandler
      {
         get { return flexErrorHandler; }
      }

      public HtmlForm MainForm
      {
         get { return aspnetForm; }
      }

      public ScriptManager ScriptManager
      {
         get { return scriptManager; }
      }

      #endregion

      protected string NoCacheTag
      {
         get
         {
            const int maxInt = 10000000;
            const string tagFmt = "?__NOCACHE__={0:0000000}"; // Same zeroes as maxInt
            var randInt = _random.Next(0, maxInt);
            return String.Format(tagFmt, randInt);
         }
      }

      #region Whitespace Remover

      private static readonly Regex RegexBetweenTags = new Regex(@">(?! )\s+", RegexOptions.Compiled);
      private static readonly Regex RegexLineBreaks = new Regex(@"([\n\s])+?(?<= {2,})<", RegexOptions.Compiled);

      protected override void Render(HtmlTextWriter writer)
      {
         using (var htmlwriter = new HtmlTextWriter(new StringWriter()))
         {
            base.Render(htmlwriter);
            var html = htmlwriter.InnerWriter.ToString();

            // Trim the whitespace from the 'html' variable
            html = RegexBetweenTags.Replace(html, ">");
            html = RegexLineBreaks.Replace(html, "<");

            writer.Write(html.Trim());
         }
      }

      #endregion

      /// <summary>
      ///   Taken here: http://devio.wordpress.com/2009/10/19/get-absolut-url-of-asp-net-application/
      /// </summary>
      private static string FullyQualifiedApplicationPath
      {
         get
         {
            //Return variable declaration
            string appPath = null;

            //Getting the current context of HTTP request
            var context = HttpContext.Current;

            //Checking the current context content
            if (context != null)
            {
               //Formatting the fully qualified website url/name
               appPath = string.Format("{0}://{1}{2}{3}",
                  context.Request.Url.Scheme,
                  context.Request.Url.Host,
                  context.Request.Url.Port == 80
                     ? string.Empty
                     : ":" + context.Request.Url.Port,
                  context.Request.ApplicationPath);
            }
            if (!appPath.EndsWith("/"))
               appPath += "/";
            return appPath;
         }
      }
   }
}
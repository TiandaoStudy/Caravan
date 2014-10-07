using System;
using System.IO;
using System.IO.Compression;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using FLEX.Web.UserControls.Ajax;
using FLEX.Web.WebForms;
using FLEX.WebForms;
using WebMarkupMin.Core;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.MasterPages
// ReSharper restore CheckNamespace
{
   public partial class Head : MasterPage, IHead
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

      #region Page Events

      protected override void OnLoad(EventArgs e)
      {
         base.OnLoad(e);

         // Conditionally compresses page output.
         GZipEncodePage();

         // Browser cache management...
         Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
         Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
         Response.AppendHeader("Expires", "0"); // Proxies.
      }

      protected override void Render(HtmlTextWriter writer)
      {
         using (var htmlTextWriter = new HtmlTextWriter(new StringWriter()))
         {
            base.Render(htmlTextWriter);
            var html = htmlTextWriter.InnerWriter.ToString();

            if (Configuration.Instance.EnableOutputMinification)
            {
               // Minify content of the 'html' variable
               var htmlMinifier = WebMarkupMinContext.Current.Markup.CreateHtmlMinifierInstance();
               html = htmlMinifier.Minify(html).MinifiedContent;
            }

            writer.Write(html);
         }
      }

      #endregion

      #region Page Compression

      /// <summary>
      /// Determines if GZip is supported
      /// </summary>
      /// <returns></returns>
      public static bool IsGZipSupported()
      {
         var acceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];
         return !string.IsNullOrEmpty(acceptEncoding) && acceptEncoding.Contains("gzip") || acceptEncoding.Contains("deflate");
      }

      /// <summary>
      ///   Sets up the current page or handler to use GZip through a Response.Filter.
      ///   IMPORTANT:  
      ///   You have to call this method before any output is generated!
      /// </summary>
      public static void GZipEncodePage()
      {
         var response = HttpContext.Current.Response;

         if (Configuration.Instance.EnableOutputCompression && IsGZipSupported())
         {
            var acceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];
            if (acceptEncoding.Contains("deflate"))
            {
               response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
               response.AppendHeader("Content-Encoding", "deflate");
            }
            else
            {
               response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
               response.AppendHeader("Content-Encoding", "gzip");
            }
         }

         // Allows proxy servers to cache encoded and unencoded versions separately.
         response.AppendHeader("Vary", "Content-Encoding");
      }

      #endregion

      public void RedirectIfNotAuthenticated()
      {
         // If user is not authenticated, then we redirect her to the session expired page.
         if (Configuration.Instance.CheckSecurity && !HttpContext.Current.User.Identity.IsAuthenticated)
         {
            Response.Redirect(Configuration.Instance.SessionExpiredPageUrl, true);
         }
      }

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
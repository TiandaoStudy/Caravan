using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.WebForms.Properties;
using FLEX.Web.XmlSettings.MenuBar;
using FLEX.WebForms;
using SecurityManager = FLEX.WebForms.SecurityManager;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls
// ReSharper restore CheckNamespace
{
   public partial class MenuBar : ControlBase
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         // ...
         if (IsPostBack) return;

         try
         {
            // Legge il file XML con il quale si configura la barra del menu.
            var xml = new XmlDocument();
            xml.Load(Server.MapPath(Settings.Default.MenuBarXmlPath));
           
            var sourceXml = xml.OuterXml;
            sourceXml = SecurityManager.Instance.ApplyMenuSecurity(Context.User, sourceXml);

            Menu menu;
            using (var sourceStream = new MemoryStream(Encoding.UTF8.GetBytes(sourceXml)))
            {
               menu = Menu.DeserializeFrom(sourceStream);
            }
            // Aggiungere elementi al menu
            ul_menu.InnerHtml = menu.Group.Aggregate("", (current, item) => current + Recursivo(item, true));
         }
         catch (Exception ex)
         {
            DataSource.Logger.LogError<MenuBar>(ex);
            throw;
         }        
      }

      private static string Recursivo(Item item, bool firstLevel)
      {
         var menu = firstLevel ? "-menu" : "";
         if (item.Group == null)
         {
            const string sApplicationUrl = "http://localhost/FLEX.Extensions.TestWebUI/";
            var url = item.URL;
            var clientCall = item.ClientCall;
            var caption = item.Caption;
            if (caption != "Separator")
            {
               if (url != null)
                  return "<li class=\"dropdown\"> <a runat=\"server\"  href=\"" + sApplicationUrl + url + "\">" + item.Caption + "</a></li>";
               return "<li><a onclick=\"" + clientCall + "\"" + "href=\"#\">" + caption + "</a></li>";
            }
            return "<li class=\"divider\"></li>";
         }

         string result;
         if (firstLevel)
            result = "<li class=\"dropdown\"> <a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\" data-hover=\"dropdown\" data-delay=\"0\" data-close-others=\"true\">" + item.Caption +
                     "<span class=\"caret\"></span></a><ul class=\"dropdown" + menu + "\">";
         else
            result = "<li class=\"dropdown-submenu\"><a href=\"#\">" + item.Caption + "</a><ul class=\"dropdown-menu\">";

         result = item.Group.Item.Aggregate(result, (current, item1) => current + Recursivo(item1, false));
         return result + "</ul></li>";
      }
   }
}
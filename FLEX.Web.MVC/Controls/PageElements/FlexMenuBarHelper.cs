using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FLEX.RestService.Core;
using RestSharp;

namespace FLEX.Web.MVC.Controls.PageElements
{
   public static class FlexMenuBarHelper
   {
      public static string GetMenuFromService()
      {
         var client = new RestClient("http://localhost/FLEX.RestService");
         var request = new RestRequest("security/menu", Method.GET);

         FLEX.RestService.Core.Menu menu;
         IRestResponse response = client.Execute(request);
         var content = response.Content; // raw content as string
         using (var stream = new StringReader(HttpUtility.HtmlDecode(content)))
         {
            menu= FLEX.RestService.Core.Menu.DeserializeFrom(stream);
         }

        return  menu.Group.Aggregate("", (current, item) => current + Recursivo(item, true));

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

﻿using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Finsa.Caravan.XmlSchemas.MenuEntries;
using RestSharp;

namespace Finsa.Caravan.Mvc.Controls.PageElements
{
   public static class FlexMenuBarHelper
   {
      public static string GetMenuFromService(AjaxHelper ajaxHelper)
      {
         var client = new RestClient("http://localhost/Caravan.RestService");
         var request = new RestRequest("security/menu", Method.GET);

         Menu menu;
         var response = client.Execute(request);
         var content = response.Content; // raw content as string
         using (var stream = new StringReader(HttpUtility.HtmlDecode(content)))
         {
            menu = Menu.DeserializeFrom(stream);
         }

         return menu.Group.Aggregate("", (current, item) => current + Recursivo(item, true, ajaxHelper));
      }

      private static string Recursivo(Item item, bool firstLevel, AjaxHelper ajaxHelper)
      {
         var menu = firstLevel ? "-menu" : "";
         if (item.Group == null)
         {
            var caption = item.Caption;
            if (caption != "Separator")
            {
               if (item.URL != null)
               {
                  return "<li>" + ajaxHelper.ActionLink(caption, item.URL, new AjaxOptions
                  {
                     HttpMethod = "GET", 
                     InsertionMode = InsertionMode.Replace, 
                     UpdateTargetId = "main-page-container",
                     OnBegin = "showMenuBarSpinner",
                     OnSuccess = "hideMenuBarSpinner"
                  }) + "</li>";
               }
               return "<li><a onclick=\"" + item.ClientCall + "\"" + "href=\"#\">" + caption + "</a></li>";
            }
            return "<li class=\"divider\"></li>";
         }

         string result;
         if (firstLevel)
            result = "<li class=\"dropdown\"> <a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\" data-hover=\"dropdown\" data-delay=\"0\" data-close-others=\"true\">" + item.Caption +
                     "<span class=\"caret\"></span></a><ul class=\"dropdown" + menu + "\">";
         else
            result = "<li class=\"dropdown-submenu\"><a href=\"#\">" + item.Caption + "</a><ul class=\"dropdown-menu\">";

         result = item.Group.Item.Aggregate(result, (current, item1) => current + Recursivo(item1, false, ajaxHelper));
         return result + "</ul></li>";
      }
   }
}
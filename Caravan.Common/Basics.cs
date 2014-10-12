using System;
using System.Web;
using FLEX.Common.Web;

namespace FLEX.Common
{
   public static class Basics
   {
      #region Event Handling

      public static void TriggerEvent(Action<ISearchControl, SearchCriteriaSelectedArgs> eventHandler, ISearchControl sender, SearchCriteriaSelectedArgs args)
      {
         if (eventHandler != null)
         {
            eventHandler(sender, args);
         }
      }

      public static void TriggerEvent(EventHandler eventHandler, object sender, EventArgs args)
      {
         if (eventHandler != null)
         {
            eventHandler(sender, args);
         }
      }

      #endregion

      public static string MapPath(string path)
      {
         if (HttpContext.Current == null) return path;
         var mappedPath = HttpContext.Current.Server.MapPath(path);
         return mappedPath.EndsWith("\\") ? mappedPath : mappedPath + "\\";
      }
   }
}
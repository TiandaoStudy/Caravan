using System;

namespace Finsa.Caravan.Extensions
{
   public static class EventExtensions
   {
        public static void SafeInvoke(this EventHandler eventHandler, object sender, EventArgs args)
        {
            if (eventHandler != null) {
                eventHandler(sender, args);
            }
        }
   }
}

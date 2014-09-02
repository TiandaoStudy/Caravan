using System;

namespace FLEX.Common.Text
{
   internal static class StringExtensions
   {
      public static bool IsNullOrWhiteSpace(string str)
      {
         if (String.IsNullOrEmpty(str))
         {
            return true;
         }
         str = str.Trim();
         return String.IsNullOrEmpty(str);
      }
   }
}

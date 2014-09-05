using System;

namespace FLEX.Common
{
   public static class StringExtensions
   {
      public static string Truncate(this string str, int maxLength)
      {
         if (String.IsNullOrEmpty(str))
         {
            return str;
         }
         maxLength = Math.Max(0, maxLength);
         return (str.Length < maxLength ? str : str.Substring(0, maxLength));
      }
   }
}

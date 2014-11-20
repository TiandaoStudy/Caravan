﻿using System;

namespace Finsa.Caravan.Extensions
{
   public static class DelegateExtensions
   {
      public static void SafeInvoke(this Action action)
      {
         if (action != null)
         {
            action();
         }
      }

      public static void SafeInvoke<T1>(this Action<T1> action, T1 arg1)
      {
         if (action != null)
         {
            action(arg1);
         }
      }

      public static void SafeInvoke<T1, T2>(this Action<T1, T2> action, T1 arg1, T2 arg2)
      {
         if (action != null)
         {
            action(arg1, arg2);
         }
      }

      public static TRet SafeInvoke<T1, T2, TRet>(this Func<T1, T2, TRet> func, T1 arg1, T2 arg2)
      {
         return (func != null) ? func(arg1, arg2) : default(TRet);
      }
   }
}
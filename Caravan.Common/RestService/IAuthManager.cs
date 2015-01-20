﻿namespace Finsa.Caravan.Common.RestService
{
   public interface IAuthManager
   {
      AuthResult Authenticate(dynamic authObject);
   }

   public struct AuthResult
   {
      public bool IsValid { get; set; }

      public string Message { get; set; }

      public dynamic UserInfo { get; set; }
   }
}
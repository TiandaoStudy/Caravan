﻿using System.Data.Common;

namespace Finsa.Caravan.DataAccess
{
   /// <summary>
   /// 
   /// </summary>
   public interface IDbManager
   {
      DataAccessKind Kind { get; }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="connectionString"></param>
      void ElaborateConnectionString(ref string connectionString);

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      DbConnection OpenConnection();

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      DbConnection CreateConnection();
   }

   public enum DataAccessKind
   {
      Oracle,
      Postgres,
      Rest,
      SqlServer,
      SqlServerCe
   }
}
﻿using System.Data;
using System.Data.Common;

namespace Finsa.Caravan.DataAccess
{
   /// <summary>
   /// 
   /// </summary>
   public interface IQueryExecutor
   {
      DatabaseVendor Vendor { get; }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="query"></param>
      /// <returns></returns>
      DataTable FillDataTableFromQuery(string query);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="query"></param>
      /// <param name="parameters"></param>
      /// <returns></returns>
      DataTable FillDataTableFromQuery(string query, dynamic parameters);

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
   }

   public enum DatabaseVendor
   {
      Dummy,
      Oracle,
      SqlServer,
      SqlServerCe
   }
}
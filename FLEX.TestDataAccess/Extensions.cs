using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using FLEX.Common.Data;
using FLEX.Common.Web;

namespace FLEX.Extensions.TestDataAccess
{
   public sealed partial class Candidate
   {
      public static DataTable RetrieveAll()
      {
         var sql = new StringBuilder("select c.Cand_Id, c.Cand_Name, c.Cand_Surname, c.Cand_Email, s.Schl_Description from Candidate c, School s, CandidateAttended ca where c.Cand_Id = ca.Cand_Id and s.Schl_Id = ca.Schl_Id");
         return QueryExecutor.Instance.FillDataTableFromQuery(sql.ToString());
      }

      public static DataTable RetrieveByCriteria(SearchCriteria criteria)
      {
         var sql =
            new StringBuilder(
               "select c.Cand_Id, c.Cand_Name, c.Cand_Surname, c.Cand_Email, s.Schl_Description, g.Gend_Description from Candidate c, School s, CandidateAttended ca, Gender g where c.Cand_Id = ca.Cand_Id and s.Schl_Id = ca.Schl_Id and c.Gend_Id = g.Gend_Id ");
         // Gender
         var gendId = criteria["GEND_ID"];
         if (gendId.Count == 1)
         {
            sql.AppendFormat(" and c.Gend_Id = '{0}' ", gendId[0]);
         }
         else if (gendId.Count > 1)
         {
            sql.AppendFormat(" and c.Gend_Id in ({0}) ", ToCommaSeparetedList(gendId));
         }
         // School
         var schlId = criteria["Schl_Id"];
         if (schlId.Count == 1)
         {
            sql.AppendFormat(" and ca.Schl_Id = '{0}' ", schlId[0]);
         }
         else if (schlId.Count > 1)
         {
            sql.AppendFormat(" and ca.Schl_Id in ({0}) ", ToCommaSeparetedList(schlId));
         }
         // Cand_Email
         var candEmail = criteria["Cand_Email"];
         if (candEmail.Count == 1)
         {
            sql.AppendFormat(" and c.Cand_Email = '{0}' ", candEmail[0]);
         }
         // Cand_Id
         var candId = criteria["Cand_Id"];
         if (candId.Count == 1)
         {
            sql.AppendFormat(" and c.Cand_Id = '{0}' ", candId[0]);
         }
         var _results = QueryExecutor.Instance.FillDataTableFromQuery(sql.ToString());
         _results.TableName = "Candidates";
         return _results;
      }

      private static string ToCommaSeparetedList(IEnumerable<string> values)
      {
         return String.Join(", ", values.ToArray());
      }
   }
}
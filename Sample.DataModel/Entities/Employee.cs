﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.DataModel.Entities
{
   [Table("Employees")]
   public class Employee
   {
      [Key, Column("Employee ID")]
      public int EmployeeID { get; set; }

      [Column("Last Name")]
      public string LastName { get; set; }

      [Column("First Name")]
      public string FirstName { get; set; }

      [Column("Title")]
      public string Title { get; set; }
   }
}
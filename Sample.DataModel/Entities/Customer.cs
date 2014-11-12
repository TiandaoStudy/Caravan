using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.DataModel.Entities
{
   [Table("Customers")]
   public class Customer
   {
      [Key, Column("Customer ID")]
      public string CustomerID { get; set; }

      [Column("Company Name")]
      public string CompanyName { get; set; }

      [Column("Contact Name")]
      public string ContactName { get; set; }

      [Column("Contact Title")]
      public string ContactTitle { get; set; }

      [Column("Address")]
      public string Address { get; set; }

      [Column("City")]
      public string City { get; set; }

      [Column("Region")]
      public string Region { get; set; }

      [Column("Postal Code")]
      public string PostalCode { get; set; }

      [Column("Country")]
      public string Country { get; set; }

      [Column("Phone")]
      public string Phone { get; set; }

      [Column("Fax")]
      public string Fax { get; set; }
   }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.DataModel.Entities
{
   [Table("Shippers")]
   public class Shipper
   {
      [Key, Column("Shipper ID")]
      public int ShipperID { get; set; }

      [Column("Company Name")]
      public string CompanyName { get; set; }
   }
}
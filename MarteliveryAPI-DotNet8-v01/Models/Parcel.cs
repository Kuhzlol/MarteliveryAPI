using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarteliveryAPI_DotNet8_v01.Models
{
    [Table("parcels")]
    public class Parcel
    {
        [Key]
        [Column("parcel_id")]
        public string ParcelId { get; set; } = Guid.NewGuid().ToString();

        [Column("pickup_location", TypeName = "varchar(250)")]
        public required string PickupLocation { get; set; }

        [Column("delivery_location", TypeName = "varchar(250)")]
        public required string DeliveryLocation { get; set; }

        [Column ("total_distance", TypeName = "float")]
        public required float TotalDistance { get; set; }

        [Column("length")]
        public required float Length { get; set; }

        [Column("width")]
        public required float Width { get; set; }

        [Column("height")]
        public required float Height { get; set; }

        [Column("weight")]
        public required float Weight { get; set; }

        [Column("customer_id")]
        public required string CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public List<Quote>? Quotes { get; set; }
    }
}

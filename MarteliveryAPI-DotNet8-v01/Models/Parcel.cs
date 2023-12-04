using System.ComponentModel.DataAnnotations.Schema;

namespace MarteliveryAPI_DotNet8_v01.Models
{
    [Table("parcels")]
    public class Parcel
    {
        [Column("parcel_id", TypeName = "uuid")]
        public Guid ParcelId { get; set; }
        [Column("pickup_location", TypeName = "varchar(250)")]
        public required string PickupLocation { get; set; }
        [Column("delivery_location", TypeName = "varchar(250)")]
        public required string DeliveryLocation { get; set; }
        [Column("length")]
        public required float Length { get; set; }
        [Column("width")]
        public required float Width { get; set; }
        [Column("height")]
        public required float Height { get; set; }
        [Column("weight")]
        public required float Weight { get; set; }
        [Column("customer_id", TypeName = "uuid")]
        public required Guid CustomerId { get; set; }
        public required Customer Customer { get; set; }
        public List<Quote>? Quotes { get; set; }
    }
}

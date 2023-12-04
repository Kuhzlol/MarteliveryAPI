using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarteliveryAPI_DotNet8_v01.Models
{
    [Table("deliveries")]
    public class Delivery
    {
        [Column("delivery_id", TypeName = "uuid")]
        public Guid DeliveryId { get; set; }
        [Column("pickup_time")]
        public required DateTime PickupTime { get; set; }
        [Column("delivery_time")]
        public required DateTime DeliveryTime { get; set; }
        [Column("delivery_status", TypeName = "varchar(250)")]
        public required string DeliveryStatus { get; set; }
        [Column("quote_id", TypeName = "uuid")]
        public required Guid QuoteId { get; set; }
        public required Quote Quote { get; set; }
        public List<CarrierRating>? CarrierRatings { get; set; }
    }
}

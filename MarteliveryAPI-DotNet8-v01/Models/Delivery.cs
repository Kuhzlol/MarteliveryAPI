using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarteliveryAPI_DotNet8_v01.Models
{
    [Table("deliveries")]
    public class Delivery
    {
        [Column("delivery_id", TypeName = "uuid")]
        public Guid DeliveryId { get; set; }

        [Column("pickup_time")]
        public DateTime? PickupTime { get; set; } = DateTime.UtcNow;

        [Column("delivery_time")]
        public DateTime? DeliveryTime { get; set; }

        [Column("delivery_status", TypeName = "varchar(250)")]
        public string? DeliveryStatus { get; set; } = "Pending";

        [Column("quote_id", TypeName = "uuid")]
        [Required]
        public Guid QuoteId { get; set; }

        public required Quote Quote { get; set; }

        public List<CarrierRating>? CarrierRatings { get; set; }
    }
}

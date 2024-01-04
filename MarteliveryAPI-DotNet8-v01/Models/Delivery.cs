using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarteliveryAPI_DotNet8_v01.Models
{
    [Table("deliveries")]
    public class Delivery
    {
        [Key]
        [Column("delivery_id")]
        public string DeliveryId { get; set; } = Guid.NewGuid().ToString();

        [Column("pickup_time")]
        public DateTime? PickupTime { get; set; } = DateTime.UtcNow;

        [Column("delivery_time")]
        public DateTime? DeliveryTime { get; set; }

        [Column("delivery_status", TypeName = "varchar(250)")]
        public string? DeliveryStatus { get; set; } = "Pending";

        [Column("quote_id")]
        [Required]
        public required string QuoteId { get; set; }
        public required Quote Quote { get; set; }

        public List<CarrierRating>? CarrierRatings { get; set; }
    }
}

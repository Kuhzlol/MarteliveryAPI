using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarteliveryAPI_DotNet8_v01.Models
{
    [PrimaryKey(nameof(DeliveryId), nameof(CustomerId))]
    [Table("carrier_ratings")]
    public class CarrierRating
    {
        [Column("delivery_id")]
        public required string DeliveryId { get; set; }
        public Delivery? Delivery { get; set; }

        [Column("customer_id")]
        public required string CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [Column("carrier_rate")]
        [Range(1, 5)]
        public required int CarrierRate { get; set; }

    }
}

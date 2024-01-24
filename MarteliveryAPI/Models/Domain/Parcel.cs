using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarteliveryAPI.Models.Domain
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

        [Column("total_distance")]
        [Precision(6, 2)]
        public required decimal TotalDistance { get; set; }

        [Column("length")]
        [Precision(6, 2)]
        public required decimal Length { get; set; }

        [Column("width")]
        [Precision(6, 2)]
        public required decimal Width { get; set; }

        [Column("height")]
        [Precision(6, 2)]
        public required decimal Height { get; set; }

        [Column("weight")]
        [Precision(6, 2)]
        public required decimal Weight { get; set; }

        [Column("user_id")]
        public required string UserId { get; set; }
        public User? User { get; set; }

        public List<Quote>? Quotes { get; set; }
    }
}

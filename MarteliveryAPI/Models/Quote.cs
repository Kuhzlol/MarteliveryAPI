using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MarteliveryAPI.Models
{
    [Table("quotes")]
    public class Quote
    {
        [Key]
        [Column("quote_id")]
        public string QuoteId { get; set; } = Guid.NewGuid().ToString();

        [Column("price_per_km")]
        public required float PricePerKm { get; set; }

        [Column("total_price")]
        public required float TotalPrice { get; set; }

        [Column("status", TypeName = "varchar(250)")]
        public string? Status { get; set; } = "Pending";

        [Column("user_id")]
        public required string UserId { get; set; }
        public User? User { get; set; }

        [Column("parcel_id")]
        public required string ParcelId { get; set; }
        public Parcel? Parcel { get; set; }

        public List<Delivery>? Deliveries { get; set; }

        public List<Payment>? Payments { get; set; }
    }
}
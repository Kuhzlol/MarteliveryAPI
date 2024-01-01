using System.ComponentModel.DataAnnotations.Schema;
using MarteliveryAPI_DotNet8_v01.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MarteliveryAPI_DotNet8_v01.Models
{
    [Table("quotes")]
    public class Quote
    {
        [Column("quote_id")]
        public string QuoteId { get; set; } = Guid.NewGuid().ToString();

        [Column("price_per_km")]
        public required float PricePerKm { get; set; }

        [Column("total_price")]
        public required float TotalPrice { get; set; }

        [Column("status", TypeName = "varchar(250)")]
        public required string Status { get; set; }

        [Column("carrier_id")]
        public required string CarrierId { get; set; }
        public required Carrier Carrier { get; set; }

        [Column("parcel_id")]
        public required string ParcelId { get; set; }
        public required Parcel Parcel { get; set; }

        public List<Delivery>? Deliveries { get; set; }

        public List<Payment>? Payments { get; set; }
    }
}
﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MarteliveryAPI_DotNet8_v01.Models
{
    [Table("quotes")]
    public class Quote
    {
        [Column("quote_id", TypeName = "uuid")]
        public Guid QuoteId { get; set; }
        [Column("delivery_distance")]
        public required float DeliveryDistance { get; set; }
        [Column("price_per_km")]
        public required float PricePerKm { get; set; }
        [Column("total_price")]
        public required float TotalPrice { get; set; }
        [Column("status", TypeName = "varchar(250)")]
        public required string Status { get; set; }
        [Column("carrier_id", TypeName = "uuid")]
        public required Guid CarrierId { get; set; }
        public required Carrier Carrier { get; set; }
        [Column("parcel_id", TypeName = "uuid")]
        public required Guid ParcelId { get; set; }
        public required Parcel Parcel { get; set; }
        public List<Delivery>? Deliveries { get; set; }
        public List<Payment>? Payments { get; set; }
    }
}
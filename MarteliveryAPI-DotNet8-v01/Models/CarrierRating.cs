﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarteliveryAPI_DotNet8_v01.Models
{
    [PrimaryKey(nameof(DeliveryId), nameof(CustomerId))]
    [Table("carrier_ratings")]
    public class CarrierRating
    {
        [Column("delivery_id")]
        public required int DeliveryId { get; set; }
        public required Delivery Delivery { get; set; }
        [Column("customer_id")]
        public required int CustomerId { get; set; }
        public required Customer Customer { get; set; }
        [Column("carrier_rate")]
        public required int CarrierRate { get; set; }

    }
}

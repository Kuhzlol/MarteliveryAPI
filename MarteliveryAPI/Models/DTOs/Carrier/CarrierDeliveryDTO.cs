namespace MarteliveryAPI.Models.DTOs.Carrier
{
    public class CarrierDeliveryDTO
    {
        public required DateTime DeliveryTime { get; set; }
        public required string DeliveryStatus { get; set; }
        public required string QuoteId { get; set; }
    }
}

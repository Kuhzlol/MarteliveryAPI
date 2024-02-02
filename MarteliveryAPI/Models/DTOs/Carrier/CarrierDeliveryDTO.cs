namespace MarteliveryAPI.Models.DTOs.Carrier
{
    public class CarrierDeliveryDTO
    {
        public DateTime? DeliveryTime { get; set; }
        public string? DeliveryStatus { get; set; }
        public string? QuoteId { get; set; }
    }
}

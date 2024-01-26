namespace MarteliveryAPI.Models.DTOs.User
{
    public class GetDeliveryInfoDTO
    {
        public string DeliveryId { get; set; }
        public DateTime? PickupTime { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public string? DeliveryStatus { get; set; }
        public string QuoteId { get; set; }
    }
}

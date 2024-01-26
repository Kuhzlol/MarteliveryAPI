namespace MarteliveryAPI.Models.DTOs.Admin
{
    public class AdminUpdateDeliveryDTO
    {
        public DateTime PickupTime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public string DeliveryStatus { get; set; }
        public string QuoteId { get; set; }
    }
}

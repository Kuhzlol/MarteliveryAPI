namespace MarteliveryAPI.Models.DTOs.Customer
{
    public class CustomerCarrierRatingDTO
    {
        public required string DeliveryId { get; set; }
        public required int CarrierRate { get; set; }
    }
}

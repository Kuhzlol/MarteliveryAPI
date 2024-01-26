namespace MarteliveryAPI.Models.DTOs.User
{
    public class GetCarrierRatingInfoDTO
    {
        public string CarrierRatingId { get; set; }
        public string DeliveryId { get; set; }
        public string UserId { get; set; }
        public int CarrierRate { get; set; }
    }
}

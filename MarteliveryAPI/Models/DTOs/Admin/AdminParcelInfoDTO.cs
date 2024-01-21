namespace MarteliveryAPI.Models.DTOs.Admin
{
    public class AdminParcelInfoDTO
    {
        public string ParcelId { get; set; }
        public string PickupLocation { get; set; }
        public string DeliveryLocation { get; set; }
        public float TotalDistance { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public string UserId { get; set; }
    }
}

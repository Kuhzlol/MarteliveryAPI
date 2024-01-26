namespace MarteliveryAPI.Models.DTOs.Admin
{
    public class AdminUpdateParcelDTO
    {
        public required string PickupLocation { get; set; }
        public required string DeliveryLocation { get; set; }
        public required float TotalDistance { get; set; }
        public required float Length { get; set; }
        public required float Width { get; set; }
        public required float Height { get; set; }
        public required float Weight { get; set; }
        public required string UserId { get; set; }
    }
}

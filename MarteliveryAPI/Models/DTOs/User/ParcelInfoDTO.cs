namespace MarteliveryAPI.Models.DTOs.User
{
    public class ParcelInfoDTO
    {
        public required string ParcelId { get; set; }
        public required string PickupLocation { get; set; }
        public required string DeliveryLocation { get; set; }
        public required decimal TotalDistance { get; set; }
        public required decimal Length { get; set; }
        public required decimal Width { get; set; }
        public required decimal Height { get; set; }
        public required decimal Weight { get; set; }
        public required string UserId { get; set; }
    }
}

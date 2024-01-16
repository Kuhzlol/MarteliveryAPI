namespace MarteliveryAPI.Models.DTOs.Customer
{
    public class CustomerParcelDTO
    {
        public required string PickupLocation { get; set; }
        public required string DeliveryLocation { get; set; }
        public required float TotalDistance { get; set; }
        public required float Length { get; set; }
        public required float Width { get; set; }
        public required float Height { get; set; }
        public required float Weight { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MarteliveryAPI.Models.DTOs
{
    public class ParcelDTO
    {
        [Required]
        public required string PickupLocation { get; set; }
        [Required]
        public required string DeliveryLocation { get; set; }
        [Required]
        public required float TotalDistance { get; set; }
        [Required]
        public required float Length { get; set; }
        [Required]
        public required float Width { get; set; }
        [Required]
        public required float Height { get; set; }
        [Required]
        public required float Weight { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MarteliveryAPI.DTOs
{
    public class UserInfoDTO
    {
        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public required DateOnly DateOfBirth { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required string PhoneNumber { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MarteliveryAPI.Models.DTOs
{
    public class UserAllInfoDTO
    {
        [Required]
        public required string Id { get; set; }
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required]
        public required DateOnly DateOfBirth { get; set; }
        [Required]
        public required DateTime CreatedOn { get; set; }
        [Required]
        public required string UserName { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required bool EmailConfirmed { get; set; }
        [Required]
        public required string PhoneNumber { get; set; }
        [Required]
        public required bool PhoneNumberConfirmed { get; set; }
        [Required]
        public required bool TwoFactorEnabled { get; set; }
        [Required]
        public required DateTime LockoutEnd { get; set; }
        [Required]
        public required bool LockoutEnabled { get; set; }
        [Required]
        public required int AccessFailedCount { get; set; }
        [Required]
        public required bool IsCustomer { get; set; }
    }
}

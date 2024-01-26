using System.ComponentModel.DataAnnotations;

namespace MarteliveryAPI.Models.DTOs.Admin
{
    public class AdminUpdateUserDTO
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        public required bool EmailConfirmed { get; set; }

        public required bool PhoneNumberConfirmed { get; set; }
    }
}

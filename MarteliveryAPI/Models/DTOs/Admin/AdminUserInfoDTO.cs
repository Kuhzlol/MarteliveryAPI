namespace MarteliveryAPI.Models.DTOs.Admin
{
    public class AdminUserInfoDTO
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTimeOffset LockoutEnd { get; set; }

        public bool LockoutEnabled { get; set; }

        public bool IsCustomer { get; set; }
    }
}

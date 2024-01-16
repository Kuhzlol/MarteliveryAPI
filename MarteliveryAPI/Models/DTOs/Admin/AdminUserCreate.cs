namespace MarteliveryAPI.Models.DTOs.Admin
{
    public class AdminUserCreate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool IsCustomer { get; set; }
    }
}

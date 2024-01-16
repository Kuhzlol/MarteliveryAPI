namespace MarteliveryAPI.Models.DTOs.User
{
    public class UserInfoDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
    }
}

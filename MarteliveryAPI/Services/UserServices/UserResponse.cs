namespace MarteliveryAPI.Services.UserServices
{
    public class UserResponse
    {
        public record class GeneralResponse(bool Flag, string Message);
        public record class LoginResponse(bool Flag, string Token, string Message);
    }
}

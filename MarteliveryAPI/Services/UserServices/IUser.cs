using MarteliveryAPI.Models.DTOs.User;
using static MarteliveryAPI.Services.UserServices.UserResponse;

namespace MarteliveryAPI.Services.UserServices
{
    public interface IUser
    {
        Task<GeneralResponse> CreateAccount(UserRegisterDTO userDTO);

        Task<LoginResponse> LoginAccount(UserLoginDTO loginDTO);
    }
}

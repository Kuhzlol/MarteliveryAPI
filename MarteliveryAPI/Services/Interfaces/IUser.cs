using MarteliveryAPI.Models.DTOs;
using static MarteliveryAPI.Services.Options.UserResponseOption;

namespace MarteliveryAPI.Services.Interfaces
{
    public interface IUser
    {
        Task<GeneralResponse> CreateAccount(UserRegisterDTO userDTO);

        Task<LoginResponse> LoginAccount(UserLoginDTO loginDTO);
    }
}

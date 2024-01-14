using MarteliveryAPI.DTOs;
using static MarteliveryAPI.Services.Options.UserResponseOption;

namespace MarteliveryAPI.Services.Interface
{
    public interface IUserRepository
    {
        Task<GeneralResponse> CreateAccount(UserRegisterDTO userDTO);

        Task<LoginResponse> LoginAccount(UserLoginDTO loginDTO);
    }
}

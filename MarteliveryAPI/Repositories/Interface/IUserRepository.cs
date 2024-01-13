using MarteliveryAPI.DTOs;
using static MarteliveryAPI.Services.ResponseService;

namespace MarteliveryAPI.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<GeneralResponse> CreateAccount(UserRegisterDTO userDTO);

        Task<LoginResponse> LoginAccount(UserLoginDTO loginDTO);
    }
}

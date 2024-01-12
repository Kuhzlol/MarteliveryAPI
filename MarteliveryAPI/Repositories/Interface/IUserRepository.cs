using MarteliveryAPI.DTOs;
using static MarteliveryAPI.DTOs.ServiceResponses;

namespace MarteliveryAPI.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<GeneralResponse> CreateAccount(UserRegisterDTO userDTO);

        Task<LoginResponse> LoginAccount(UserLoginDTO loginDTO);
    }
}

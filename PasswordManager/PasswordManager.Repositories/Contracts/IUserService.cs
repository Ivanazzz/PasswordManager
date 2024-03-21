using PasswordManager.Models.Dtos;
using PasswordManager.Models.Entities;

namespace PasswordManager.Repositories.Contracts
{
    public interface IUserService
    {
        Task RegisterAsync(UserRegistrationDto userRegistrationDto);

        Task<User> LoginAsync(UserLoginDto userLoginDto);
    }
}

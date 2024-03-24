using PasswordManager.Models.Dtos.PasswordDtos;

namespace PasswordManager.Repositories.Contracts
{
    public interface IPasswordRepository
    {
        // Ivana
        Task<PasswordCheckerDto> CheckPasswordStrength(string password);
    }
}

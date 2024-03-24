using System.Text;
using System.Text.RegularExpressions;

using PasswordManager.Models.CustomExceptions;
using PasswordManager.Models.Dtos.PasswordDtos;
using PasswordManager.Repositories.Contracts;

namespace PasswordManager.Repositories.Services
{
    public class PasswordRepository : IPasswordRepository
    {
        private const int MaxScore = 5;
        private const int PasswordMinLength = 10;

        // Ivana
        public async Task<PasswordCheckerDto> CheckPasswordStrength(string password)
        {
            if (password == null)
            {
                throw new BadRequestException("Няма въведена парола");
            }

            int score = MaxScore;
            StringBuilder advice = new StringBuilder();

            advice.AppendLine("Съвети за подобряване на вашата парола:");

            if (password.Length < PasswordMinLength) 
            {
                score--;
                advice.AppendLine($"- да бъде по-дълга от {PasswordMinLength} символа");
            }

            if (!Regex.IsMatch(password, "[a-z]"))
            {
                score--;
                advice.AppendLine("- да съдържа поне една малка буква от a до z");
            }

            if (!Regex.IsMatch(password, "[A-Z]"))
            {
                score--;
                advice.AppendLine("- да съдържа поне една голяма буква от A до Z");
            }

            if (!Regex.IsMatch(password, @"\d"))
            {
                score--;
                advice.AppendLine("- да съдържа поне една цифра");
            }

            if (!Regex.IsMatch(password, @"[^a-zA-Z0-9]"))
            {
                score--;
                advice.AppendLine("- да съдържа поне един специален символ");
            }

            return new PasswordCheckerDto
            {
                YourScore = score,
                Advice = score == MaxScore 
                    ? "Вашата парола е сигурна" 
                    : advice.ToString()
            };
        }
    }
}

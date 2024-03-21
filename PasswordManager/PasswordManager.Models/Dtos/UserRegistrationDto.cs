using System.Reflection;

namespace PasswordManager.Models.Dtos
{
    public class UserRegistrationDto
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordConfirmation { get; set; }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Models;
using PasswordManager.Models.CustomExceptions;
using PasswordManager.Models.Dtos;
using PasswordManager.Models.Entities;
using PasswordManager.Repositories.Contracts;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManager.Repositories.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly PasswordManagerDbContext context;

        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        public UserRepository(PasswordManagerDbContext context)
        {
            this.context = context;
        }

        public async Task<User> LoginAsync(UserLoginDto userLoginDto)
        {
            var user = await context.Users
                .SingleOrDefaultAsync(u => u.Email == userLoginDto.Email
                    && !u.IsDeleted);

            if (user == null)
            {
                throw new NotFoundException("Невалиден потребител");
            }

            bool isPasswordValid = VerifyPassword(
                userLoginDto.Password, 
                user.PasswordHash, 
                Convert.FromHexString(user.PasswordSalt));

            if (!isPasswordValid)
            {
                throw new NotFoundException("Невалиден потребител");
            }

            return user;
        }

        public async Task RegisterAsync(UserRegistrationDto userRegistrationDto)
        {
            bool userExists = await context.Users
                .SingleOrDefaultAsync(u => u.Email == userRegistrationDto.Email && !u.IsDeleted) != null
                    ? true
                    : false;

            if (userExists)
            {
                throw new BadRequestException("Потребител с този имейл вече съществува");
            }

            var hashedPassword = HashPasword(userRegistrationDto.Password, out var salt);

            var user = new User()
            {
                Username = userRegistrationDto.Username,
                Email = userRegistrationDto.Email,
                PasswordHash = hashedPassword,
                PasswordSalt = Convert.ToHexString(salt)
            };

            await context.AddAsync(user);
            await context.SaveChangesAsync();

        }

        public async Task<UserDto?> GetUserAsync(string email)
        {
            var user = await context.Users
                .Where(u => u.Email == email && !u.IsDeleted)
                .Select(u => new UserDto()
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email
                })
                .SingleOrDefaultAsync();

            return user;
        }

        private string HashPasword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToHexString(hash);
        }

        private bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
    }
}

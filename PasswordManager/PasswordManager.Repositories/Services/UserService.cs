using AutoMapper;
using PasswordManager.Models.Dtos;
using PasswordManager.Models.Entities;
using PasswordManager.Repositories.Contracts;
using System.Security.Cryptography;

namespace PasswordManager.Repositories.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;

        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        public UserService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public Task<User> LoginAsync(UserLoginDto userLoginDto)
        {
            throw new NotImplementedException();
        }

        public Task RegisterAsync(UserRegistrationDto userRegistrationDto)
        {
            throw new NotImplementedException();
        }
    }
}

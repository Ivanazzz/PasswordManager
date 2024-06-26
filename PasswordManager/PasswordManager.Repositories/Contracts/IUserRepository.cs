﻿using PasswordManager.Models.Dtos.UserDtos;
using PasswordManager.Models.Entities;

namespace PasswordManager.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task RegisterAsync(UserRegistrationDto userRegistrationDto);

        Task<User> LoginAsync(UserLoginDto userLoginDto);

        Task<UserDto?> GetUserAsync(string email);
    }
}

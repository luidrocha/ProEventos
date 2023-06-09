﻿using Microsoft.AspNetCore.Identity;
using ProEventos.Application.Dtos;
using System.Threading.Tasks;

namespace ProEventos.Application.IContratos
{
    public interface IAccountService
    {
        Task<bool> UserExists(string username);
        Task<UserUpdateDto> GetUserByUserNameAsync(string userName);
        Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password);
        Task<UserDto> CreateAccountAsync(UserDto userDto);
        Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto);
    }
}

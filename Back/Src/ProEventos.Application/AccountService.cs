using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProEventos.Application.Dtos;
using ProEventos.Application.IContratos;
using ProEventos.Domain.Identity;
using System;
using System.Threading.Tasks;
using ProEventos.Persistence.IContratos;
using Microsoft.EntityFrameworkCore;
using AutoMapper.Execution;

namespace ProEventos.Application
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserPersist _userPersist;
        private readonly IMapper _mapper;

        public AccountService(UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              IMapper mapper,
                              IUserPersist userPersist)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userPersist = userPersist;
            _mapper = mapper;


        }
        public async Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password)
        {
            try
            {
                var user = await _userManager.Users.SingleOrDefaultAsync(
                    user => user.UserName.ToLower() == userUpdateDto.Username.ToLower());

                // Se colocar True no parametro, ele bloqueia caso a senha não confira com a do usuário
                // o ideal seria fazer um logica de N tentaivas, bloqeia. False não bloqeia

                return await _signInManager.CheckPasswordSignInAsync(user, password, false);
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar verificar password. Erro: {ex.Message}");
            }
        }

        public async Task<UserDto> CreateAccountAsync(UserDto userDto)
        {
            try
            {
                // Mapeia o objeto  userDto para User

                var user = _mapper.Map<User>(userDto);
                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (result.Succeeded)
                {
                    // UserDto é a classe . Faz o mapeamento inverso
                    var userReturn = _mapper.Map<UserDto>(user);
                    return userReturn;
                }
                return null;

            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar criar usuário. Erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> GetUserByUserNameAsync(string userName)
        {
            try
            {
                var user = await _userPersist.GetUserByUserName(userName);
                if (user == null) return null;

                // faz o mapeamento reverso para DTO

                var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
                return userUpdateDto;
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar recuperar usuário por username. Erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await _userPersist.GetUserByUserName(userUpdateDto.Username);

                if (user == null) return null;

                // após encontrar, mapeia o user com os falores vindo do DTO

                _mapper.Map<UserUpdateDto>(user);

                // GeneratePasswordResetTokenAsync foi usado para não deslogar o usuario na troca de senha

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, userUpdateDto.Password);

                _userPersist.Update<User>(user);

                if (await _userPersist.SaveChangesAsync())
                {
                    var userRetorno = await _userPersist.GetUserByUserName(user.UserName);

                    return _mapper.Map<UserUpdateDto>(userRetorno);
                }
                return null;
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar atualizar usuário. Erro: {ex.Message}");
            }
        }

        public async Task<bool> UserExists(string username)
        {
            try
            {
                // retorna true se o usuario existe

                return await _userManager.Users.AnyAsync(user => user.UserName == username.ToLower());

            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar verificar se usuario existe. Erro: {ex.Message}");
            }
        }
    }
}

﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Dtos;
using ProEventos.Application.IContratos;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace ProEventos.API.Controllers
{
    // Colocando neste ponto o authorize bloquei todos os END-POINTS
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService,
                                 ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }
        [HttpGet("GetUser/{userName}")]
        // Permite chamar o metodo sem autenticação. Login e registrar deve ter este metodo
        [AllowAnonymous] // alguém que não esta com token
        public async Task<IActionResult> GetUser(string userName)
        {
            try
            {
                var user = await _accountService.GetUserByUserNameAsync(userName);
                return Ok(user);

            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar usuário.Error: {ex.Message}");
            }


        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto userDto)
        {

            try
            {
                if (await _accountService.UserExists(userDto.Username))
                    return BadRequest("Usuário já existe");

                var user = await _accountService.CreateAccountAsync(userDto);
                if (user != null)
                    return Ok(user);

                return BadRequest("Não foi possivel cadastrar o usuario. Tente mais tarde");


            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar usuario. Error: {ex.Message}");

            }


        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {

            try
            {

                var user = await _accountService.GetUserByUserNameAsync(userLogin.UserName);
                // Verifica se o usuario existe, senão existir não executa mais nada. 
                if (user == null) return Unauthorized(" Usuario ou senha iválida !");
                // Verifica o usuario e a senha, se tiver errado não executa mais nada e retorna
                var result = await _accountService.CheckUserPasswordAsync(user, userLogin.Password);

                if (!result.Succeeded) return Unauthorized(" Usuario ou senha iválida !");

            // Se encontrar o usuario monta um objeto com as informação de lonig e token
                return Ok(
                    new
                    {
                        userName = user.Username,
                        PrimeiroNome = user.PrimeiroNome,
                        token = _tokenService.CreateToken(user).Result

                    }
                );


            }
            catch (System.Exception ex)
            {

               return this.StatusCode(StatusCodes.Status500InternalServerError,
               $"Erro ao tentar fazer o login, tente mais tarde. Erro: {ex.Message}");
            }



        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Extensions;
using ProEventos.Application.Dtos;
using ProEventos.Application.IContratos;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text.RegularExpressions;
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
        [HttpGet("GetUser")]
        // Permite chamar o metodo sem autenticação. Login e registrar deve ter este metodo
        //[AllowAnonymous] // alguém que não esta com token
        public async Task<IActionResult> GetUser()
        {
            try
            {
                // User vem de ClaimsPrincipal / ControllerBase que é gerada por padrão do .net identity.
                // Pega o user se for diferente de null

                var userName = User.GetUserName();
                //.FindFirst(ClaimTypes.Name)?.Value;

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
                if (await _accountService.UserExists(userDto.UserName))
                    return BadRequest("Usuário já existe");
                // Retorna um UserUpdateDto que contem o token, assim, apos registrar pode-se fazer o login direto
                var user = await _accountService.CreateAccountAsync(userDto);
                if (user != null)
                    return Ok(new
                                {
                                    userName = user.UserName,
                                    PrimeiroNome = user.PrimeiroNome,
                                    token = _tokenService.CreateToken(user).Result

                                });

                return BadRequest("Não foi possivel cadastrar o usuario. Tente mais tarde");


            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar registrar usuário. Error: {ex.Message}");

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
                        userName = user.UserName,
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

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserUpdateDto userUpdateDto)
        {
            // Verifica se o usuario passado é o mesmo que o usuario logado, com token.
            if(userUpdateDto.UserName != User.GetUserName())
            return Unauthorized("Usuário inválido");

            try
            { // User.GetUserName() Vem do metodo de extensão, pois só podemos atualizar um user com base no Token
                var user = await _accountService.GetUserByUserNameAsync(User.GetUserName());

                if (user == null) return Unauthorized("Usuário inválido");

                var userReturn  = await _accountService.UpdateAccount(userUpdateDto);

                if (userReturn == null) return NoContent();

                return Ok(
                    new
                    {
                        userName = userReturn.UserName,
                        PrimeiroNome = userReturn.PrimeiroNome,
                        token = _tokenService.CreateToken(userReturn).Result

                    }

                );


            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                       $"Erro ao tentar atualizar os dados, tente mais tarde. Erro: {ex.Message}");
            }

        }

    }
}

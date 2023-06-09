using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProEventos.Application.Dtos;
using ProEventos.Application.IContratos;
using ProEventos.Domain.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application
{

    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public readonly SymmetricSecurityKey _key;

        // IConfiguration config para pegar o seguredo

        public TokenService(IConfiguration config,
                            UserManager<User> userManager,
                            IMapper mapper)
        {
            _config = config;
            _userManager = userManager;
            _mapper = mapper;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"]));
        }
        public async Task<string> CreateToken(UserUpdateDto userUpdateDto)
        {
            // faz o mapeamento de userUpdateDto para user conforme CLASS User
            var user = _mapper.Map<User>(userUpdateDto);

            // claims são afirmações criadas baseadas no usuario
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            // busca todas os papeis do usuario. Ex. Adm, Moderador... etc...
            var roles = await _userManager.GetRolesAsync(user);
            // Adiciona para dentro de cleims varias outras cleims quesão as nossas Roles
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            // creds = credentials
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            // Cria o token baseado nas Claims
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),    // Adiciona as claims
                Expires = DateTime.Now.AddDays(1),      // Define expiração para 1 dia
                SigningCredentials = creds              // Define as Credentials chave de cript/descriptografia
            };

            // Usado para colocar o Token no formato JWT
            var tokenHendler = new JwtSecurityTokenHandler();

            // Cria o Token propriamente dito
            var token = tokenHendler.CreateToken(tokenDescription);
            // Escreve o token no formato JWT
            return tokenHendler.WriteToken(token);


        }
    }
}

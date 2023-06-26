using ProEventos.Domain.Identity;
using System.Security.Claims;

namespace ProEventos.API.Extensions
{
    // Classe usada para extender as funcionalidades de ClaimsPrincipal que vem de ConrollBase
    // Com isso facilitamos para retornar somente o que precisamos

    // Toda vez que estamos criando um metodo de extensão STATIC a classe também tem que ser STATIC
    public static class ClaimsPrincipalExtensions
    {
        // O metodo de extensão tem ha ver com o primeiro parametro que vc passa dentro do metodo crido

        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }


        public static int GetuserId(this ClaimsPrincipal user)
        {
            // NameIdentifier foi usado na definição do Token para identificar o userId

            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

    }
}

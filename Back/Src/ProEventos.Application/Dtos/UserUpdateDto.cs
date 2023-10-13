using ProEventos.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application.Dtos
{
    public class UserUpdateDto
    {
        // Usado para atualizar um usuario 
        
      /* cOM O id no dto ao tentar salvar o registro estava tentanto regravar o ID, gerand erro de 
      integridade referencial*/

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string UserName { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Funcao { get; set; }
        public string Descricao { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}

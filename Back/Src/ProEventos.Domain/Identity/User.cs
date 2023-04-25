using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProEventos.Domain.Enum;

namespace ProEventos.Domain.Identity
{
    public class User: IdentityUser<int>
    {
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public Titulo Titulo { get; set; }
        public string Descricao { get; set; }
        public Funcao Funcao { get; set; }
        public string ImagemPerfil { get; set; }
        [NotMapped]
        public string NomeCompleto {get {
            return this.PrimeiroNome+UltimoNome;
        }}
        // Usado para recuperar as permissões do usuário
        public IEnumerable<UserRole> UseRoles { get; set; }

        
    }
}
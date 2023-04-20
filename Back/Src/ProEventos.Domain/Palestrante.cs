using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain.Identity;

namespace ProEventos.Domain
{
    public class Palestrante
    {
        public int Id { get; set; }
        public string MiniCurriculo { get; set; }
        // Usado para controlar se o usuario é palestrante ou para mudar a função.
        public User User { get; set; }
        public int UserId { get; set; }
        public IEnumerable<RedeSocial> RedeSociais { get; set; }
        public IEnumerable<PalestranteEvento> PalestranteEventos { get; set; }

        // Removido pq vem de Users
        // public string Nome  { get; set; }
        // public string ImagemURL { get; set; }
        // public string Telefone { get; set; }
        // public string Email { get; set; }
    }
}
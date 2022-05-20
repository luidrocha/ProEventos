using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Domain  equivale ao Model da arquitetura MVC

namespace ProEventos.Domain
{
    public class Evento
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public DateTime? DataEvento { get; set; } // ? = pode ser nullo
        public string Tema { get; set; }
        public int QtdPessoa { get; set; }
        public string ImagemUrl { get; set; } 
        public string Telefone { get; set; }
        public string Email { get; set; }
        // Usado para retornar a lista de Lotes e RedeSociais
        public IEnumerable<Lote> Lotes { get; set; } // um evento tem vaios lotes
        public IEnumerable<RedeSocial> RedesSociais {get;set;} // um eventos pode ter varias redes sociais
        // Tabela auxiliar 
        public IEnumerable<PalestranteEvento> PalestranteEvento  { get; set; }
    }
}
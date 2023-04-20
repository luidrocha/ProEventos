using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain.Identity;

// Domain  equivale ao Model da arquitetura MVC

namespace ProEventos.Domain
{
    // [Table("tabEventosDetalhes")] // Define o nome da Tabela
    public class Evento
    {
       //[Key] deve-se usar se o campo de ID tiver nome diferente como CodigoID, IdProduto
        public int Id { get; set; }
        public string Local { get; set; }
        public DateTime? DataEvento { get; set; } // ? = pode ser nullo
        // Campo que n�o mapeado para o banco de dados. Sera usado para exibi��o. N�o vai para tabela
        [NotMapped]
        public int ContagemDias { get; set; }
        public string Tema { get; set; }
        public int QtdPessoa { get; set; }
        public string ImagemUrl { get; set; } 
        public string Telefone { get; set; }
        public string Email { get; set; }
        // Vem de Identity
        public User User     { get; set; }
        public int UserId  { get; set; }
        // Usado para retornar a lista de Lotes e RedeSociais
        public IEnumerable<Lote> Lotes { get; set; } // um evento tem vaios lotes
        public IEnumerable<RedeSocial> RedesSociais {get;set;} // um eventos pode ter varias redes sociais
        // Tabela auxiliar 
        public IEnumerable<PalestranteEvento> PalestranteEvento  { get; set; }
    }
}
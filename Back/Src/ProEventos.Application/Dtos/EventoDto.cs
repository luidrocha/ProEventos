using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "campo {0} é obrigatório"),
         StringLength(30, MinimumLength = 5,
                          ErrorMessage = "o preenchimento do campo {0} deve estar entre 5 e 50 caracteres")]
        public string Local { get; set; }
        [Required(ErrorMessage = "o campo {0} é Obrigatorio dd/MM/aaaa")]
        public string DataEvento { get; set; } // ? = pode ser nullo

        [Required(ErrorMessage = "campo {0} é obrigatório"),
       //MinLength(5, ErrorMessage ="Campo {0} deve ter no Mínimo 5 caracteres"),
       // MaxLength(50, ErrorMessage ="Campo {0} deve ter no máximo 50 caracteres")]
       StringLength(50, MinimumLength = 5,
                        ErrorMessage = "o preenchimento do campo {0} deve estar entre 5 e 50 caracteres")]
        public string Tema { get; set; }

        [Display(Name = "qtd pessoas"),
         Range(1, 500, ErrorMessage = "o campo {0} deve está entre 1 e 500")]
        public int QtdPessoa { get; set; }

        [Display(Name = "Caminho imagem"),
         Required(ErrorMessage = "campo {0} obrigatório"),
         RegularExpression(@".*\.(gif|jpe?g|bmp|png)$",
                           ErrorMessage ="formato de arquiquivo não suportado(gif, jpg, jpeg, bmp ou png")]     
        public string ImagemUrl { get; set; }

        [Required(ErrorMessage = "campom {0} é obrigatório"),
        Phone(ErrorMessage = "o campo {0} contém caracter inválido")]
        public string Telefone { get; set; }
       
        [Required(ErrorMessage = "campo {0} é obrigatório"),
         Display(Name = "e-mail"),
        EmailAddress(ErrorMessage = "campo {0} preenchido com formato inválido")]
        public string Email { get; set; }

        // Usado para retornar a lista de Lotes e RedeSociais

        public IEnumerable<LoteDto> Lotes { get; set; } // um evento tem vaios lotes
        public IEnumerable<RedeSocialDto> RedesSociais { get; set; } // um eventos pode ter varias redes sociais
                                                                     // Tabela auxiliar 
        public IEnumerable<PalestranteDto> Palestrante { get; set; }
    }
}
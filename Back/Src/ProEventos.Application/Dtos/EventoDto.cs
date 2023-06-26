using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "campo {0} � obrigat�rio"),
         StringLength(30, MinimumLength = 5,
                          ErrorMessage = "o preenchimento do campo {0} deve estar entre 5 e 50 caracteres")]
        public string Local { get; set; }

        [Required(ErrorMessage = "o campo {0} � Obrigatorio dd/MM/aaaa")]
        public DateTime? DataEvento { get; set; } // ? = pode ser nullo

        [Required(ErrorMessage = "campo {0} � obrigat�rio"),
       //MinLength(5, ErrorMessage ="Campo {0} deve ter no M�nimo 5 caracteres"),
       // MaxLength(50, ErrorMessage ="Campo {0} deve ter no m�ximo 50 caracteres")]
       StringLength(50, MinimumLength = 5,
                        ErrorMessage = "o preenchimento do campo {0} deve estar entre 5 e 50 caracteres")]
        public string Tema { get; set; }

        [Display(Name = "qtd pessoas"),
         Range(1, 500, ErrorMessage = "o campo {0} deve est� entre 1 e 500")]
        public int QtdPessoa { get; set; }

        [Display(Name = "Caminho imagem"),
        // Required(ErrorMessage = "campo {0} obrigat�rio"),
        RegularExpression(@".*\.(gif|GIF|jpe?g|JPE?G|bmp|BMP|png|PNG)$",
         ErrorMessage = "formato de arquiquivo n�o suportado(gif, jpg, jpeg, bmp ou png")]
        public string ImagemUrl { get; set; }

        [Required(ErrorMessage = "campom {0} � obrigat�rio"),
        Phone(ErrorMessage = "o campo {0} cont�m caracter inv�lido")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "campo {0} � obrigat�rio"),
         Display(Name = "e-mail"),
        EmailAddress(ErrorMessage = "campo {0} preenchido com formato inv�lido")]
        public string Email { get; set; }
        // Campo que será usado como chave estrangeira de ligação
        public UserDto UserDto { get; set; }
        public int UserId { get; set; }

        // Usado para retornar a lista de Lotes e RedeSociais

        public IEnumerable<LoteDto> Lotes { get; set; } // um evento tem vaios lotes
        public IEnumerable<RedeSocialDto> RedesSociais { get; set; } // um eventos pode ter varias redes sociais
                                                                     // Tabela auxiliar 
        public IEnumerable<PalestranteDto> Palestrante { get; set; }
    }
}
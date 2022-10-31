using ProEventos.Application.Dtos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class PalestranteDto
{

    public int Id { get; set; }
    
    [Required]
     public string Nome { get; set; }
   
    [Required(),
    StringLength(500, MinimumLength = 25,
                 ErrorMessage = "o campo {0} deve possuir no minimo 25 e no máximo 500 caracter")]
    public string MiniCurriculo { get; set; }

    [Display(Name = "Caminho imagem"),
         Required(ErrorMessage = "campo {0} obrigatório"),
         RegularExpression(@".*\.(gif|jpe?g|bmp|png)$",
                           ErrorMessage = "formato de arquiquivo não suportado(gif, jpg, jpeg, bmp ou png")]
    public string ImagemURL { get; set; }
   
    [Required(ErrorMessage = "campom {0} é obrigatório"),
        Phone(ErrorMessage = "o campo {0} contém caracter inválido")]
    public string Telefone { get; set; }
    
    [Required(ErrorMessage = "campo {0} é obrigatório"),
          Display(Name = "e-mail"),
         EmailAddress(ErrorMessage = "campo {0} preenchido com formato inválido")]
    public string Email { get; set; }

    public IEnumerable<RedeSocialDto> RedeSociais { get; set; }
    public IEnumerable<PalestranteDto> Palestrantes { get; set; }

}

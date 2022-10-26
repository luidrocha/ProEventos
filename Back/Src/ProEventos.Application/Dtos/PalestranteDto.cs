using ProEventos.Application.Dtos;
using System.Collections.Generic;

public class PalestranteDto
    {

        public int Id { get; set; }
        public string Nome { get; set; }

        public string MiniCurriculo { get; set; }
        public string ImagemURL { get; set; }
        public string Telpefone { get; set; }
        public string Email { get; set; }
       
        public IEnumerable<RedeSocialDto> RedeSociais { get; set; }
        public IEnumerable<PalestranteDto> Palestrantes { get; set; }
    
}

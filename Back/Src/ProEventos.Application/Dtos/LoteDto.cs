using System;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dtos
{
    public class LoteDto
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public decimal Preco { get; set; }
        [Required]
        public string DataInicio { get; set; }
        [Required]
        public DateTime? DataFim { get; set; }
        [Required]
        public int Quantidade { get; set; }
        public EventoDto Evento { get; set; }
        [Required]
        public int EventoId { get; set; }

    }
}

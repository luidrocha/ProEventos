namespace ProEventos.Application.Dtos
{
    public class RedeSocialDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string URL { get; set; }
        public EventoDto Evento { get; set; }
        public int? EventoId { get; set; } // pode ser nullo
        public PalestranteDto Palestrante { get; set; }
        public int? PalestranteId { get; set; }
    }
}

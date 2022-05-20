using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Domain
{
    public class PalestranteEvento
    {
        public Palestrante  Palestrante { get; set; }
        public int PalestranteId { get; set; }
        public Evento Evento { get; set; }
        public int EventoId { get; set; }
    }
}
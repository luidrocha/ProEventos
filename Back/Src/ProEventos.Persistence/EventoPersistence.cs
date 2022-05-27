using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence;
using ProEventos.Persistence.IContratos;
using System.Linq;
using System.Threading.Tasks;

namespace ProEvento.Persistence
{
    // Classe que implenta a interface IProEventosPersistence que tem os metodos Genericospara EVENTOS

    class EventoPersistence : IEventoPersistence
    {
        // faz a injeção de Dependencia do contexto.

        private readonly ProEventosContext _context;
        public EventoPersistence(ProEventosContext context)
        {
            _context = context;
        }
               

        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrantes = false)
        {
            // Faz  a consulta e retorna um Array

            IQueryable<Evento> query = _context.Eventos
                .Include(ev => ev.Lotes)
                .Include(ev => ev.RedesSociais);

            query = query.OrderBy(ev => ev.Id);  // ordena pelo Id

            if (includePalestrantes)
            {
                query = query
                    .Include(pe => pe.PalestranteEvento) // para cada PalestranteEvento, inclua Palestrante
                    .ThenInclude(pe => pe.Palestrante);
            }

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(ev => ev.Lotes)
                .Include(ev => ev.RedesSociais);

            query = query.OrderBy(ev => ev.Id);

            if (includePalestrantes)
            {
                query = query
                    .Include(pe => pe.PalestranteEvento) // Tabela associativa
                    .ThenInclude(pe => pe.Palestrante);  // obj Palestrante
            }

            query = query.OrderBy(ev => ev.Id) // Filta o evento por Tema
                    .Where(ev => ev.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }
        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos;

            query = query
                .Include(ev => ev.Lotes)
                .Include(ev => ev.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include(pe => pe.PalestranteEvento)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query = query
                .OrderBy(ev => ev.Id)
                .Where(ev => ev.Id == eventoId);

            return await query.FirstOrDefaultAsync();


        }




    }
}

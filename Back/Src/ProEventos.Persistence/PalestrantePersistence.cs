using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.IContratos;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Persistence
{


    // Classe que implenta a interface IPalestrantePersistence que tem os metodos Genericospara EVENTOS


    class PalestrantePersistence : IPalestrantePersistence
    {
        // faz a injeção de Dependencia do contexto.

        private readonly ProEventosContext _context;

        public PalestrantePersistence(ProEventosContext context)
        {
            _context = context;
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes;

            query = query.AsNoTracking().Include(pa => pa.RedeSociais);

            if (includeEventos)
            {
                query = query
                        .Include(ev => ev.PalestranteEventos)
                        .ThenInclude(ev => ev.Evento);
            }

            query = query.OrderBy(pe => pe.Id); // Organiza por nome ID

            return await query.ToArrayAsync();

        }
        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes;

            query = query.AsNoTracking().Include(pa => pa.RedeSociais);

            if (includeEventos)
            {
                query = query
                        .Include(ev => ev.PalestranteEventos)
                        .ThenInclude(ev => ev.Evento);
            }

            query = query.OrderBy(pe => pe.Nome)
                    .Where(pe => pe.Nome.ToLower()
                    .Contains(nome.ToLower()));

            return await query.ToArrayAsync();

        }
        public async Task<Palestrante> GetPalestranteByIdAsync(int Id, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes;

            query = query.AsNoTracking().Include(pa => pa.RedeSociais);

            if (includeEventos)
            {
                query = query
                        .Include(ev => ev.PalestranteEventos)
                        .ThenInclude(ev => ev.Evento);
            }

            query = query.OrderBy(pe => pe.Id).Where(pe => pe.Id == Id);

            return await query.FirstOrDefaultAsync();

        }


    }
}

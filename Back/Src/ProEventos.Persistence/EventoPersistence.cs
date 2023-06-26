using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.IContratos;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Persistence
{
    // Classe que implenta a interface IProEventosPersistence que tem os metodos Genericospara EVENTOS

    public class EventoPersistence : IEventoPersistence
    {
        // faz a injeção de Dependencia do contexto.

        private readonly ProEventosContext _context;

        public EventoPersistence(ProEventosContext context)
        {
            _context = context;
            
            // Desta forma a configuração afeta todos os metodos da classe
            // Faz com que os metodos não prendam o processo do objeto para que outro possa utilizar.
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; 
        }


        public async Task<Evento[]> GetAllEventoAsync(int userId, bool includePalestrantes = false)
        {
            // Faz  a consulta e retorna um Array

            IQueryable<Evento> query = _context.Eventos
                .Include(ev => ev.Lotes)
                .Include(ev => ev.RedesSociais);
            // AsNoTracking() para não prender o processo da consulta/objeto
            query = query.AsNoTracking()
                         .Where(ev => ev.UserId ==userId)
                         .OrderBy(ev => ev.Id);  // ordena pelo Id

            if (includePalestrantes)
            {
                query = query
                    .Include(pe => pe.PalestranteEvento) // para cada PalestranteEvento, inclua Palestrante
                    .ThenInclude(pe => pe.Palestrante);
            }

            return await query.ToArrayAsync();
        }
        public async Task<Evento[]> GetAllEventosByTemaAsync(int userId, string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(ev => ev.Lotes)
                .Include(ev => ev.RedesSociais);

            query = query.AsNoTracking().OrderBy(ev => ev.Id);

            if (includePalestrantes)
            {
                query = query
                    .Include(pe => pe.PalestranteEvento) // Tabela associativa
                    .ThenInclude(pe => pe.Palestrante);  // obj Palestrante
            }

            query = query.OrderBy(ev => ev.Id) // Filta o evento por Tema
                    .Where(ev => ev.Tema.ToLower().Contains(tema.ToLower())
                              && ev.UserId ==userId);

            return await query.ToArrayAsync();
        }
        public async Task<Evento> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrantes = false)
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

            query = query.AsNoTracking()
                .OrderBy(ev => ev.Id)
                .Where(ev => ev.Id == eventoId && ev.UserId == userId);

            return await query.FirstOrDefaultAsync();


        }




    }
}

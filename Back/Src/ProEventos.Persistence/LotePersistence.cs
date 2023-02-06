using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.IContratos;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Persistence
{
    // Classe que implenta a interface IProEventosPersistence que tem os metodos Genericospara EVENTOS

    public class LotePersistence : ILotePersistence
    {
        // faz a injeção de Dependencia do contexto.

        private readonly ProEventosContext _context;

        public LotePersistence(ProEventosContext context)
        {
            _context = context;
            
            // Desta forma a configuração afeta todos os metodos da classe
            // Faz com que os metodos não prendam o processo do objeto para que outro possa utilizar.
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; 
        }

        public async Task<Lote> GetLoteByIdsAsync(int eventoId, int loteId)
        {
            IQueryable<Lote> query = _context.Lotes;
            //Se você está apenas consultando um objeto no EF, ou seja,
            //não vai modificar e gravar, use AsNoTracking() sempre que possível.
            query = query.AsNoTracking()
                    .Where(lote => lote.EventoId == eventoId
                                    && lote.Id == loteId);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Lote[]> GetLotesByEventoIdAsync(int eventoId)

            {
            IQueryable<Lote> query = _context.Lotes;

            query = query.AsNoTracking()
                    .Where(lote => lote.EventoId == eventoId);
            return await query.ToArrayAsync();  // Vai retornar um array , todos os lotes
        }
    }
}

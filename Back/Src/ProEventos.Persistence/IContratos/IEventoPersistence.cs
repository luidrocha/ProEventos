using ProEventos.Domain;
using System.Threading.Tasks;

namespace ProEventos.Persistence.IContratos
{
    public interface IEventoPersistence
    {

        //EVENTOS
        //Segundo parametro é opcional será usado caso o usuario desejar trazer o Palestrante ou Evento
        // Metodos para retornar Query
        // Acrescentado campo userId para que o sistema pegue somente o evento do usuario logado.
        Task<Evento[]> GetAllEventosByTemaAsync(int userId, string tema, bool includePalestrantes = false );
        Task<Evento[]> GetAllEventoAsync(int userId, bool includePalestrantes = false);
        Task<Evento> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrantes = false );


    }
}

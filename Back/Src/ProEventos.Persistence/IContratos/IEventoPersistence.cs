using ProEventos.Domain;
using System.Threading.Tasks;

namespace ProEventos.Persistence.IContratos
{
    public interface IEventoPersistence
    {

        //EVENTOS
        //Segundo parametro é opcional será usado caso o usuario desejar trazer o Palestrante ou Evento
        // Metodos para retornar Query

        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false );
        Task<Evento[]> GetAllEventoAsync(bool includePalestrantes = false);
        Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false );


    }
}

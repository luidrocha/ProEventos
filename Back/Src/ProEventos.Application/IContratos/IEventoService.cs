using ProEventos.Domain;
using System.Threading.Tasks;

namespace ProEventos.Application.IContratos
{
    public interface IEventoService
    {
        Task<Evento> AddEventos(Evento model);
        Task<Evento> UpdateEvento(int Id, Evento model);
        Task<bool> DeleteEvento(int Id);

        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false);
        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);
        Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false );

    }
}

using ProEventos.Application.Dtos;
using ProEventos.Domain;
using System.Threading.Tasks;

namespace ProEventos.Application.IContratos
{
    public interface IEventoService
    {
        Task<EventoDto> AddEventos(int userId, EventoDto model);
        Task<EventoDto> UpdateEvento(int userId, int Id, EventoDto model);
        Task<bool> DeleteEvento(int userId,int Id);

        Task<EventoDto[]> GetAllEventosAsync(int userId,bool includePalestrantes = false);
        Task<EventoDto[]> GetAllEventosByTemaAsync(int userId, string tema, bool includePalestrantes = false);
        Task<EventoDto> GetEventoByIdAsync(int userId,int eventoId, bool includePalestrantes = false );

    }
}

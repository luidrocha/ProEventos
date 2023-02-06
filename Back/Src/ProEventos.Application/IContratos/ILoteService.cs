using ProEventos.Application.Dtos;
using ProEventos.Domain;
using System.Threading.Tasks;

namespace ProEventos.Application.IContratos
{
    public interface ILoteService
    {
        
        Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] models);

        Task<bool> DeleteLote(int eventoId, int loteId);

        Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId);
      
        Task<LoteDto> GetLoteByIdsAsync(int eventoId, int loteId );

    }
}

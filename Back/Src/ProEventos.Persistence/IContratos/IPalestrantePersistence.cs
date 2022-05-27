using ProEventos.Domain;
using System.Threading.Tasks;

namespace ProEventos.Persistence.IContratos
{
    interface IPalestrantePersistence
    {
       
        
        Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string tema, bool includeEventos);
        Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos);
        Task<Palestrante> GetPalestranteByIdAsync(int eventoID, bool includeEventos);
    }
}

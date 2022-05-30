using ProEventos.Domain;
using System.Threading.Tasks;

namespace ProEventos.Persistence.IContratos
{
    interface IPalestrantePersistence
    {
       
        
        Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos);
        Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false);
        Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos = false);
    }
}

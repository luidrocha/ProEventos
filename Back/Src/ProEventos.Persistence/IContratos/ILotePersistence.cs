using ProEventos.Domain;
using System.Threading.Tasks;

namespace ProEventos.Persistence.IContratos
{
    public interface ILotePersistence
    {

        //EVENTOS
        //Segundo parametro é opcional será usado caso o usuario desejar trazer o Palestrante ou Evento
        // Metodos para retornar Query

      /// <summary>
      /// Metodo que retornara uma lista de lotes por evento
      /// </summary>
      /// <param name="eventoId"> Codigo chave primaria do evento</param>
      /// <returns> Todos os lotes relacionados</returns>
      /// 
        Task<Lote[]> GetLotesByEventoIdAsync(int eventoId);

        /// <summary>
        /// Metodo que retornará apenas  1 lote
        /// </summary>
        /// <param name="eventoId">Codigo chave da tabela Evento </param>
        /// <param name="loteId">Codigo chave da tabela Lote </param>
        /// <returns>Retorna 1 lote</returns>
        /// 
        Task<Lote> GetLoteByIdsAsync(int eventoId, int loteId );


    }
}

using ProEventos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.IContratos
{
    interface IEventoPersistence
    {

        //EVENTOS
        //Segundo parametro é opcional será usado caso o usuario desejar trazer o Palestrante ou Evento
        // Metodos para retornar Query

        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes);
        Task<Evento[]> GetAllEventoAsync(bool includePalestrantes);
        Task<Evento> GetEventoByIdAsync(int eventoID, bool includePalestrantes);


    }
}

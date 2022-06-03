using ProEventos.Persistence.IContratos;
using ProEventos.Domain;
using System;
using System.Threading.Tasks;
using ProEventos.Application.IContratos;
using System.Linq;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        public readonly IGeralPersistence _geralPersistence;
        public readonly IEventoPersistence _eventoPersistence;


        public EventoService(IGeralPersistence geralPersistence, IEventoPersistence eventoPersistence)
        {

            _geralPersistence = geralPersistence;
            _eventoPersistence = eventoPersistence;



        }

        // Adiciona o evento e retorno o evento para utilização, caso queira..

        public async Task<Evento> AddEventos(Evento model)
        {
            try
            {
                // Adiciona um objeto do tipo Evento que chega em model

                _geralPersistence.Add<Evento>(model);
                if (await _geralPersistence.SaveChangesAsync()) // Retorna True se foi salvo
                {
                    return await _eventoPersistence.GetEventoByIdAsync(model.Id); // pega o evento que acabou de gravar

                }
                return null;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            };
        }

        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                // Temos um get que segura o objeto Evento. Dentro do metodo temos que usar o AsNotracking

                var evento = await _eventoPersistence.GetEventoByIdAsync(eventoId, false);

                if (evento == null) return null;

                model.Id = eventoId;


                // Atualiza o objeto, Evento
                _geralPersistence.Update(model);

                // Retorna true se for gravado
                if (await _geralPersistence.SaveChangesAsync())
                {
                    return await _eventoPersistence.GetEventoByIdAsync(model.Id, false);
                }

                return null;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }






        public async Task<bool> DeleteEvento(int eventoId)
        {

            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(eventoId, false);

                if (evento == null) // esse ponto deveria retornar um boll
                {
                    // Levanta a exceção para o controller tratar

                    throw new Exception("Evento para Delete não encontrado..");


                }
                _geralPersistence.Delete<Evento>(evento);
                return await _geralPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {

            try
            {
                var eventos = await _eventoPersistence.GetAllEventoAsync(includePalestrantes);

                if (eventos == null) return null;

                return eventos;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }


        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetAllEventosByTemaAsync(tema, includePalestrantes);

                if (eventos == null) return null;

                return eventos;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }


        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetEventoByIdAsync(eventoId, includePalestrantes);
    
            if (eventos == null) return null;

                return eventos;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }



        }

    }
}


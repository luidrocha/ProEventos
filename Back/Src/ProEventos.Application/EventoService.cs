using ProEventos.Persistence.IContratos;
using ProEventos.Domain;
using System;
using System.Threading.Tasks;
using ProEventos.Application.IContratos;
using System.Linq;
using AutoMapper;
using ProEventos.Application.Dtos;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        public readonly IGeralPersistence _geralPersistence;
        public readonly IEventoPersistence _eventoPersistence;
        public readonly IMapper _mapper;

        public EventoService(IGeralPersistence geralPersistence, 
                            IEventoPersistence eventoPersistence,
                            IMapper mapper )
        { 
            
            _geralPersistence = geralPersistence;
            _eventoPersistence = eventoPersistence;
            _mapper = mapper;
        }

        // Adiciona o evento e retorno o evento para utilização, caso queira..

        public async Task<EventoDto> AddEventos(EventoDto model)
        {
            
            try
            {
                // Adiciona um objeto do tipo Evento que chega em model
                
                var evento = _mapper.Map<Evento>(model);  // Mapeia de model para Entidade

                _geralPersistence.Add<Evento>(evento);

                if (await _geralPersistence.SaveChangesAsync()) // Retorna True se foi salvo
                {
                   var eventoRetorno = await _eventoPersistence.GetEventoByIdAsync(evento.Id); // pega o evento que acabou de gravar

                    return _mapper.Map<EventoDto>(eventoRetorno); // Mapeia de Entidade para Model
                }
                return null;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            };
        }

        public async Task<EventoDto> UpdateEvento(int eventoId, EventoDto model)
        {
            try
            {
                // Temos um get que segura o objeto Evento. Dentro do metodo temos que usar o AsNotracking

              

                var evento  = await _eventoPersistence.GetEventoByIdAsync(eventoId, false);

                if (evento == null) return null;

                model.Id = eventoId;
                
                _mapper.Map(model, evento); // Mapeamento de objetos e não classes... ou entidades.

                 // Atualiza o objeto, Evento
                _geralPersistence.Update<Evento>(evento);

                // Retorna true se for gravado
                if (await _geralPersistence.SaveChangesAsync())
                {
                    var eventoRetorno = await _eventoPersistence.GetEventoByIdAsync(evento.Id, false);

                    return _mapper.Map<EventoDto>(eventoRetorno);
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

                if (evento == null) // nesse ponto deveria retornar um boll
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

        public async Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes = false)
        {

            try
            {
                var eventos = await _eventoPersistence.GetAllEventoAsync(includePalestrantes);

                if (eventos == null) return null;

                var resultado = _mapper.Map<EventoDto[]>(eventos);

                return resultado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }


        public async Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetAllEventosByTemaAsync(tema, includePalestrantes);

                if (eventos == null) return null;

                var resultado = _mapper.Map<EventoDto[]>(eventos);

                return resultado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }


        public async Task<EventoDto> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(eventoId, includePalestrantes);
    
            if (evento == null) return null;

                // Dto dATA tRANSFER oBJECT
                // sendo retornado - somente os campos desejados sem expor a classe
                // Mapear para EventoDtos o valor retornado por eventos.

                var resultado = _mapper.Map<EventoDto>(evento);
                
                return resultado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }



        }

    }
}


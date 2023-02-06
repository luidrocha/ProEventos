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
    public class LoteService : ILoteService
    {
        public readonly IGeralPersistence _geralPersistence;
        public readonly ILotePersistence _lotePersistence;
        public readonly IMapper _mapper;

        public LoteService(IGeralPersistence geralPersistence, 
                            ILotePersistence lotePersistence,
                            IMapper mapper )
        { 
            
            _geralPersistence = geralPersistence;
            _lotePersistence = lotePersistence;
            _mapper = mapper;
        }

        // Adiciona o lote e retorno o lote para utilização, caso queira..

        //void 
        public async Task AddLote(int eventoId, LoteDto model)
        {
         try
            {
                // Adiciona um objeto do tipo Evento que chega em model
   
                var lote = _mapper.Map<Lote>(model);  // Mapeia de model para Entidade
                lote.EventoId = eventoId;
                _geralPersistence.Add<Lote>(lote);
                await _geralPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            };
        }

        //  Retorna um Array
        public async Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] models)
        {
            try
            {
                // Temos um get que segura o objeto lote. Dentro do metodo temos que usar o AsNotracking
                var lotes  = await _lotePersistence.GetLotesByEventoIdAsync(eventoId);
                if (lotes == null) return null;
                // Verifica se o ID = 0, se for é um registro novo, senão é uma atualiza~]ap
                // para a não salvamos o ID.
                foreach (var model  in models)
                {
                    if (model.Id==0)
                    {
                        await AddLote(eventoId, model);
                    }
                    else
                    {
                        var lote = lotes.FirstOrDefault(lote => lote.Id == model.Id);
                        model.Id = eventoId;
                        _mapper.Map(models, lotes); // Mapeamento de objetos e não classes... ou entidades.
                        // Atualiza o objeto, Evento
                        _geralPersistence.Update<Lote>(lote);
                        // Retorna true se for gravado
                        await _geralPersistence.SaveChangesAsync();
                    };
                }
                    var loteRetorno = await _lotePersistence.GetLotesByEventoIdAsync(eventoId);
                    return _mapper.Map<LoteDto[]>(loteRetorno);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteLote(int eventoId, int loteId)
        {

            try
            {
                var lote = await _lotePersistence.GetLoteByIdsAsync(eventoId, loteId);

                if (lote == null) // nesse ponto deveria retornar um boll
                {
                    // Levanta a exceção para o controller tratar

                    throw new Exception("Lote para Delete não encontrado..");


                }
                _geralPersistence.Delete<Lote>(lote);
                return await _geralPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public async Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId)
        {

            try
            {
                var lotes = await _lotePersistence.GetLotesByEventoIdAsync(eventoId);

                if (lotes == null) return null;

                var resultado = _mapper.Map<LoteDto[]>(lotes);

                return resultado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

   
        public async Task<LoteDto> GetLoteByIdsAsync(int eventoId, int loteId)
        {
            try
            {
                var lote = await _lotePersistence.GetLoteByIdsAsync(eventoId, loteId);
    
            if (lote == null) return null;

                // Dto dATA tRANSFER oBJECT
                // sendo retornado - somente os campos desejados sem expor a classe
                // Mapear para LoteDtos o valor retornado por eventos.

                var resultado = _mapper.Map<LoteDto>(lote);
                
                return resultado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }



        }

       
    }
}


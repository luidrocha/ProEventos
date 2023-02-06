using Microsoft.AspNetCore.Mvc;
using ProEventos.Domain;
using ProEventos.Application.IContratos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using ProEventos.Application.Dtos;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotesController : ControllerBase
    {
        private readonly ILoteService _loteService;

        // Cria 2 objetos no formato JSON


        public LotesController(ILoteService loteService)
        {
            _loteService = loteService;
        }


        [HttpGet("{eventoId}")]

        // Foi usado o IActionResult para RETORNAR , poder usar o retorno dos erros de web como 200, 2001...500
        // poderia ser usado um IEQueryable<Eventos> ou  ActionResult<evento> e ainda poderia trabalhar com os retornos http
        
        public async Task<IActionResult> Get(int eventoId) // Retorna um array
        {
            try
            {
                var lotes = await _loteService.GetLotesByEventoIdAsync(eventoId); // true para que venha os Palestrantes

                // poderia ter usado NotFound("Nenhum evento encontrado."); // StatusCode erro 404
                if (lotes == null) return NoContent(); //representa o mesmo codigo acima, porém, mensagem padrão do HTTP.

               
                return Ok(lotes); //StatusCode 200
            }
            catch (Exception ex)
            {
                // Retorna o erro interno da aplicação concatenando o erro

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro interno ao tentar recuperar Lotes. Erro: {ex.Message} ");
            }
        }

          

        [HttpPut("{eventoId}")]
        public async Task<IActionResult> SaveLotes(int eventoId, LoteDto[] models)
        {
         try
            {
                var lotes = await _loteService.SaveLotes(eventoId, models);

                if (lotes == null) return BadRequest("Erro ao tentar atualizar Lotes");

                return Ok(lotes);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                       $"Erro interno, não foi possivel salvar  Lotes. Erro: {ex.Message} ");
            }
        }

        [HttpDelete("{eventoId}/{loteId}")]
        public async Task<IActionResult> Delete(int eventoId, int loteId)
        {

            try
            {
                // Usamos uma condição TERNARIA

                var lote = await _loteService.GetLoteByIdsAsync(eventoId, loteId);
                if (lote == null) return NoContent();

                return await _loteService.DeleteLote(lote.EventoId, lote.Id)
                     ? Ok(new { message = "Lote Deletado" }) // retorna um obj com a chave Deletado
                     : BadRequest("Erro ao excluir  Lote. Não deletado.");
              
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                       $"Erro interno, não foi possivel deletar o Lote. Erro: {ex.Message} ");
            }

        }

    }
}

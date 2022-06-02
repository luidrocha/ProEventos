using Microsoft.AspNetCore.Mvc;
using ProEventos.Domain;
using ProEventos.Application.IContratos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        // Cria 2 objetos no formato JSON


        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }


        [HttpGet]

        // Foi usado o IActionResult para RETORNAR , poder usar o retorno dos erros de web como 200, 2001...500
        // poderia ser usado um IEQueryable<Eventos> ou  ActionResult<evento> e ainda poderia trabalhar com os retornos http
        public async Task<IActionResult> Get() // Retorna um array
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true); // true para que vinha os Palestrantes

                if (eventos == null) return NotFound("Nenhum evento encontrado."); // StatusCode erro 404

                return Ok(eventos); //StatusCode 200
            }
            catch (Exception ex)
            {
                // Retorna o erro interno da aplicação concatenando o erro

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro interno ao tentar recuperar Eventos. Erro: {ex.Message} ");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int eventoId) // Retorna um array
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(eventoId, true);

                if (evento == null) return NotFound("Nenhum evento com o Id encontrado.");

                return Ok(evento);

                // return await _context.Eventos.FirstOrDefault(evento => evento.Id == id);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro Interno ao tentar recuperar Eventos.Erro {ex.Message}");
            }
        }


        [HttpGet("{tema}/tema")]

        public async Task<IActionResult> getEventosByTema(string tema)
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosByTemaAsync(tema, true);

                if (eventos == null) return NotFound("Nennhum evento com o tema eoncontrado.");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  $"Erro Interno ao tentar recuperar Eventos. Erro: {ex.Message}");
            }
        }



        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                var evento = _eventoService.AddEventos(model);

                if (evento == null) return BadRequest("Erro ao tentar adicionar o Evento");

                return Ok(evento);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                       $"Erro interno, não foi possivel adicionar o Evento. Erro: {ex.Message} ");
            }


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento model)
        {

            try
            {
                var evento = await _eventoService.UpdateEvento(id, model);

                if (evento == null) return BadRequest("Erro ao tentar atualizar o Evento");

                return Ok(evento);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                       $"Erro interno, não foi possivel atualizar o Evento. Erro: {ex.Message} ");
            }


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            try
            {
                // Usamos uma condição TERNARIA

                return await _eventoService.DeleteEvento(id)
                     ? Ok("Deletado")
                     : BadRequest("Erro ao excluir. Não deletado.");
              
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                       $"Erro interno, não foi possivel deletar o Evento. Erro: {ex.Message} ");
            }

        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProEventos.Domain;
using ProEventos.Application.IContratos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using ProEventos.Application.Dtos;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using ProEventos.API.Extensions;

namespace ProEventos.API.Controllers
{
   
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;
        private readonly IAccountService _accountService;

        // faz a injeção de dependencia pra pegar as informações do host
        private readonly IWebHostEnvironment _hostEnvironment;

        // Cria 2 objetos no formato JSON


        public EventosController(IEventoService eventoService,
        IWebHostEnvironment hostEnvironment, IAccountService accountService)
        {
            _eventoService = eventoService;
            _hostEnvironment = hostEnvironment;
            _accountService = accountService;

        }



        // Foi usado o IActionResult para RETORNAR , poder usar o retorno dos erros de web como 200, 2001...500
        // poderia ser usado um IEQueryable<Eventos> ou  ActionResult<evento> e ainda poderia trabalhar com os retornos http

        [HttpGet]
        public async Task<IActionResult> Get() // Retorna um array
        {
            try
            {   // User.GetuserId() vem da classe de extensão que herda de ControlleBase e tem dados do usuario
                // Vai retornar de acordo com o Token do usuario logado

                var eventos = await _eventoService.GetAllEventosAsync(User.GetuserId(), true); // true para que venha os Palestrantes

                // poderia ter usado NotFound("Nenhum evento encontrado."); // StatusCode erro 404
                if (eventos == null) return NoContent(); //representa o mesmo codigo acima, porém, mensagem padrão do HTTP.


                return Ok(eventos); //StatusCode 200
            }
            catch (Exception ex)
            {
                // Retorna o erro interno da aplicação concatenando o erro

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro interno ao tentar recuperar Eventos. Erro: {ex.Message} ");
            }
        }

        [HttpGet("{eventoId}")]
        public async Task<IActionResult> GetById(int eventoId) // Retorna um array
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(User.GetuserId(), eventoId, true);

                if (evento == null) return NoContent();  //NotFound("Nenhum evento com o Id encontrado.");

                return Ok(evento);

                // return await _context.Eventos.FirstOrDefault(evento => evento.Id == id);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro Interno ao tentar recuperar Eventos.Erro {ex.Message}");
            }
        }


        [HttpGet("tema/{tema}")]

        public async Task<IActionResult> getEventosByTema(string tema)
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosByTemaAsync( User.GetuserId(), tema, true);

                if (eventos == null) return NoContent(); //NotFound("Nennhum evento com o tema eoncontrado.");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  $"Erro Interno ao tentar recuperar Eventos. Erro: {ex.Message}");
            }
        }

        [HttpPost("upload-imagem/{eventoId}")]
        public async Task<IActionResult> UploadImagem(int eventoId)
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(User.GetuserId(), eventoId, true);
                if (evento == null) return NoContent();

                // Pega o nome do arquivo recebido
                var file = Request.Form.Files[0];

                if (file.Length > 0)
                {
                    evento.ImagemUrl = await AsyncSaveImage(file);

                }
                var EventoRetorno = await _eventoService.UpdateEvento(User.GetuserId(), eventoId, evento);


                return Ok(EventoRetorno);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                       $"Erro interno, não foi possivel adicionar o Evento. Erro: {ex.Message} ");
            }


        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDto model)
        {
            try
            {
                var evento = await _eventoService.AddEventos(User.GetuserId(), model);

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
        public async Task<IActionResult> Put(int id, EventoDto model)
        {

            try
            {
                var evento = await _eventoService.UpdateEvento(User.GetuserId(), id, model);

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
                var evento = await _eventoService.GetEventoByIdAsync(User.GetuserId(), id, true);
                if (evento == null) return NoContent();
                // Usamos uma condição TERNARIA

                if (await _eventoService.DeleteEvento(User.GetuserId(), id))
                {
                    DeleteImage(evento.ImagemUrl);
                    return Ok(new { message = "Deletado" });
                }
                else
                {

                    // retorna um obj com a chave Deletado
                    throw new Exception("Erro ao tentar excluir o Evento. Não deletado.");

                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                       $"Erro interno, não foi possivel deletar o Evento. Erro: {ex.Message} ");
            }

        }

        // informa ao controle que não é um end-point, não pode ser acessado 
        // Salva a imagem de upload

        [NonAction]
        public async Task<string> AsyncSaveImage(IFormFile imageFile)
        {
            // Pega os 10 primeiro caracteres do nome do arquivo sem a extensão.
            // Replace troca onde tem espaço por underline.
            string imagemName = new string(Path.GetFileNameWithoutExtension(imageFile.FileName)
                                          .Take(10)
                                          .ToArray()
                                          ).Replace(' ', '_');
            // Cria um nome para o arquivo concatenando com data, ano minuto e segundo
            // Path pega informaçoes do arquivo, nesse caso coloca a extensão no final
            imagemName = $"{imagemName}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(imageFile.FileName)}";

            // Monta o caminho da pasta padrão para Salvar o arquivo
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/imagens", imagemName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imagemName;

        }

        // informa ao controle que não é um end-point, não pode ser acessado 
        [NonAction]
        public void DeleteImage(string imagemName)
        {
            // Pega o caminho e o  nome do arquivo a excluir
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources\imagens", imagemName);
            // Verifica se o arquivo existe
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

        }

    }




}

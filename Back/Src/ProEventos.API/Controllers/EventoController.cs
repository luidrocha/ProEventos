using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Model;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {


        private readonly ILogger<EventoController> _logger;

        public EventoController(ILogger<EventoController> logger)
        {

        }

        // Cria 2 objetos no formato JSON
        public IEnumerable<Evento> _evento = new Evento[] {
                new Evento() {

                EventoId=1,
                Tema= "Desenvolvimento .NET 5 Core Com Angular 12",
                Local= "Rio de Janeiro",
                Lote="1º Lote",
                QtdPessoa=250,
                DataEvento=DateTime.Now.AddDays(2).ToString("dd/MM/yyyy"),
                ImagemUrl="foto.png"
                },

                new Evento() {

                EventoId=2,
                Tema= "Desenvolvimento com CSHARP e Visual Studio 19",
                Local= "São Paulo",
                Lote="2º Lote",
                QtdPessoa=350,
                DataEvento=DateTime.Now.AddDays(5).ToString("dd/MM/yyyy"),
                ImagemUrl="foto1.png"
                }


            };


        [HttpGet]

        public IEnumerable<Evento> Get() // Retorna um array
        {

            return _evento;
        }

        [HttpGet("{id}")]

        public IEnumerable<Evento> GetById(int id) // Retorna um array
        {

            return _evento.Where(evento => evento.EventoId == id);
        }


        [HttpPost]
        public string Post()
        {

            return "Exemplo de post";

        }

        [HttpPut("{id}")]
        public string Put(int id)
        {

            return $"Exemplo de Put com id = {id}";

        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {

            return $"Exemplo de Delete com id = {id}";

        }

    }
}

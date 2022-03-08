﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Model;
using ProEventos.API.Data;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        public readonly DataContext _context;

        // Cria 2 objetos no formato JSON
        public EventoController(DataContext context)
        {

            _context = context;


        }






        [HttpGet]

        public IEnumerable <Evento> Get() // Retorna um array
        {

            return _context.Eventos;
        }

        [HttpGet("{id}")]
        public Evento GetById(int id) // Retorna um array
        {

            return  _context.Eventos.FirstOrDefault(evento => evento.EventoId==id);
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

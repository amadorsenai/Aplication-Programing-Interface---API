﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Filmes.WebApi.Domains;
using Senai.Filmes.WebApi.Repository;

namespace Senai.Filmes.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        GeneroRepository generoRepository = new GeneroRepository();

        [HttpGet]
        public List<GeneroDomain> ListarTodos() // Lista todos Jasons de uma lista
        {
            return generoRepository.Listar();
        }

        [HttpGet("{id}")] // Lista um só objeto da lista
        public IActionResult BuscarPorId(int id)
        {
            GeneroDomain genero = generoRepository.BuscarPorId(id);
            if (genero == null)
            {
                return NotFound();
            }
            return Ok(genero);
        }

        [HttpPost] // Poderia passar pela url mas JSON é mais seguro
        public IActionResult Cadastrar(GeneroDomain generoDomain)
        {
            generoRepository.Cadastrar(generoDomain);
            return Ok();
        }

        [HttpDelete("{id}")] // Lista um só objeto da lista
        public IActionResult Deletar(int Id)
        {
            generoRepository.Deletar(Id);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, GeneroDomain genero)
        {
            generoRepository.Atualizar(id, genero);
            return Ok();
        }

        [HttpGet("{nome}/buscar")]
        public List<GeneroDomain> BuscarGenerosPorNome(string nome)
        {
            List<GeneroDomain> generoLs = generoRepository.BuscarPorNome(nome);
            if (generoLs == null)
            {
                NotFound();
            }
            return generoLs;
        }
    }
}
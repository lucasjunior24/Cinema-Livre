using FilmesApi.Services;
using FilmesAPI.Data.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeService filmeService;

        public FilmeController(FilmeService filmeService)
        {
            this.filmeService = filmeService;
        }

        [HttpPost]
        //[Authorize(Roles = "admin")]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            ReadFilmeDto readFilmeDto = filmeService.AdicionaFilme(filmeDto);

            return CreatedAtAction(nameof(RecuperaFilmesPorId), new { Id = readFilmeDto.Id }, readFilmeDto);
        }

        [HttpGet]
        public IActionResult RecuperaFilmes([FromQuery] int? classificacaoEtaria = null)
        {
            List<ReadFilmeDto> readFilmeDto = filmeService.RecuperaFilmes(classificacaoEtaria);

            if (readFilmeDto != null) return Ok(readFilmeDto);

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaFilmesPorId(int id)
        {
            ReadFilmeDto readFilmeDto = filmeService.RecuperaFilmesPorId(id);

            if (readFilmeDto != null) return Ok(readFilmeDto);

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            Result result = filmeService.AtualizaFilme(id, filmeDto);

            if (result.IsFailed) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            Result result = filmeService.DeletaFilme(id);

            if (result.IsFailed) return NotFound();

            return NoContent();
        }

    }
}
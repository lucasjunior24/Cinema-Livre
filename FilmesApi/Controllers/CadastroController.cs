using FilmesApi.Data.Dtos.Requests;
using FilmesApi.Data.Dtos.Usuario;
using FilmesApi.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        private readonly CadastroService cadastroService;

        public CadastroController(CadastroService cadastroService)
        {
            this.cadastroService = cadastroService;
        }

        [HttpPost]
        public IActionResult CadastraUsuario(CreateUsuarioDto createUsuarioDto)
        {
            var result = cadastroService.CadastrarUsuario(createUsuarioDto);

            if (result == null) return StatusCode(500);
            return Ok(result);
        }

        [HttpGet("/ativa")]
        public IActionResult AtivaContaUsuario([FromQuery] AtivaContaRequest ativaConta)
        {
            var result = cadastroService.AtivaContaUsuario(ativaConta);

            if (result == false) return StatusCode(500, "Falha ao ativar conta de Usuário!");
            return Ok("Conta ativada com sucesso!");
        }
    }
}

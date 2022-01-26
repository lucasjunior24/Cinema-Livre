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
        private CadastroService cadastroService;

        public CadastroController(CadastroService cadastroService)
        {
            this.cadastroService = cadastroService;
        }

        [HttpPost]
        public IActionResult Index(CreateUsuarioDto createUsuarioDto)
        {
            Result result = cadastroService.CadastrarUsuario(createUsuarioDto);

            if (result.IsFailed) return StatusCode(500);
            return Ok();
        }
    }
}

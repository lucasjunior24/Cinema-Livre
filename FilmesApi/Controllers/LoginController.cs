using FilmesApi.Data.Dtos.Requests;
using FilmesApi.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly LoginService loginService;

        public LoginController(LoginService loginService)
        {
            this.loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> LogaUsuario(LoginRequest loginRequest)
        {
            var token = await loginService.LogaUsuario(loginRequest);

            if (token == null) return Unauthorized();
            return Ok(token);
        }

        [HttpPost("logout")]
        public IActionResult DeslogaUsuario()
        {
            var result = loginService.DeslogaUsuario();

            if (result == null) return Unauthorized();
            return Ok(result);
        }
    }
}

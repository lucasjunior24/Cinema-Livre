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
        public IActionResult LogaUsuario(LoginRequest loginRequest)
        {
            Result result = loginService.LogaUsuario(loginRequest);

            if (result.IsFailed) return Unauthorized(result.Errors);
            return Ok(result.Successes);
        }

        [HttpPost("logout")]
        public IActionResult DeslogaUsuario()
        {
            Result result = loginService.DeslogaUsuario();

            if (result.IsFailed) return Unauthorized(result.Errors);
            return Ok(result.Successes);
        }
    }
}

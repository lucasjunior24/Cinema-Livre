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
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginService loginService;

        public LoginController(LoginService loginService)
        {
            this.loginService = loginService;
        }

        [HttpPost]
        public IActionResult LogaUsuario(LoginRequest loginRequest)
        {
            Result result = loginService.LogaUsuario(loginRequest);

            if (result.IsFailed) return StatusCode(500);
            return Ok();
        }
    }
}

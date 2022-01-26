using FilmesApi.Data.Dtos.Requests;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesApi.Services
{
    public class LoginService
    {
        private SignInManager<IdentityUser<int>> signInManager;

        public LoginService(SignInManager<IdentityUser<int>> signInManager)
        {
            this.signInManager = signInManager;
        }

        internal Result LogaUsuario(LoginRequest loginRequest)
        {
            var result = signInManager.PasswordSignInAsync(loginRequest.Username, loginRequest.Password, false, false);

            if (result.Result.Succeeded) return Result.Ok();
            return Result.Fail("Login falhou");
        }
    }
}

using FilmesApi.Data.Dtos.Requests;
using FilmesApi.Data.Dtos.Usuario;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace FilmesApi.Services
{
    public class LoginService
    {
        private readonly SignInManager<IdentityUser<int>> signInManager;
        private readonly TokenService tokenService;

        public LoginService(SignInManager<IdentityUser<int>> signInManager, 
                            TokenService tokenService)
        {
            this.signInManager = signInManager;
            this.tokenService = tokenService;
        }

        public Result LogaUsuario(LoginRequest loginRequest)
        {
            var result = signInManager.PasswordSignInAsync(loginRequest.Username, loginRequest.Password, false, false);

            if (result.Result.Succeeded)
            {
                var identity = signInManager.UserManager.Users
                    .FirstOrDefault(usuario => usuario.NormalizedUserName == loginRequest.Username.ToUpper());

                Token token = tokenService.CreateToken(identity);
                return Result.Ok().WithSuccess(token.Value);
            }
            return Result.Fail("Login falhou");
        }

        public Result DeslogaUsuario()
        {
            var result = signInManager.SignOutAsync();
            if (result.IsCompletedSuccessfully) return Result.Ok();
            return Result.Fail("Logout falhou");
        }
    }
}

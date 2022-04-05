using FilmesApi.Controllers;
using FilmesApi.Data.Dtos.Requests;
using FilmesApi.Data.Dtos.Usuario;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<Token> LogaUsuario(LoginRequest loginRequest)
        {
            var result = await signInManager.PasswordSignInAsync(loginRequest.Username, loginRequest.Password, false, false);

            if (result.Succeeded)
            {
                var identity = signInManager.UserManager.Users
                    .FirstOrDefault(usuario => usuario.NormalizedUserName == loginRequest.Username.ToUpper());

                Token token = tokenService.CreateToken(identity, signInManager.UserManager
                    .GetRolesAsync(identity).Result.FirstOrDefault()   
                 );
                return token;
            }
            return null;
        }

        public string SolicitaResetSenhaUsuario(ResetSenha resetSennha)
        {
            var identity = signInManager.UserManager.Users
                    .FirstOrDefault(u => u.NormalizedEmail == resetSennha.Email.ToUpper());
            if(identity != null)
            {
                var codigoRecuperacao = signInManager
                    .UserManager.GeneratePasswordResetTokenAsync(identity).Result;
                return codigoRecuperacao;
            }
            return null;
        }

        public string DeslogaUsuario()
        {
            var result = signInManager.SignOutAsync();
            if (result.IsCompletedSuccessfully) return "Logout realizado com Sucesso";
            return null;
        }
    }
}

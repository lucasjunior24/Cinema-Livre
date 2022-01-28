using AutoMapper;
using FilmesApi.Data.Dtos.Requests;
using FilmesApi.Data.Dtos.Usuario;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesApi.Services
{
    public class CadastroService
    {
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser<int>> userManager;

        public CadastroService(IMapper mapper, UserManager<IdentityUser<int>> userManager)
        {
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public string CadastrarUsuario(CreateUsuarioDto createUsuarioDto)
        {
            Usuario usuario = mapper.Map<Usuario>(createUsuarioDto);
            IdentityUser<int> usuarioIdentity = mapper.Map<IdentityUser<int>>(usuario);
            Task<IdentityResult> resultdoIdentity = userManager
                .CreateAsync(usuarioIdentity, createUsuarioDto.Password);
            if (resultdoIdentity.Result.Succeeded)
            {
                var code = userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity).Result;
                return code;
            }
            return null;
        }

        public bool AtivaContaUsuario(AtivaContaRequest ativaConta)
        {
            var identityUser = userManager.Users.FirstOrDefault(u =>
                u.Id == ativaConta.UsuarioId
            );
            var identityResult = userManager
                .ConfirmEmailAsync(identityUser, ativaConta.CodigoAtivacao).Result;
            if(identityResult.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}

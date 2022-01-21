using AutoMapper;
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
        private IMapper mapper;
        private UserManager<IdentityUser<int>> userManager;

        public CadastroService(IMapper mapper, UserManager<IdentityUser<int>> userManager)
        {
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public Result CadastrarUsuario(CreateUsuarioDto createUsuarioDto)
        {
            Usuario usuario = mapper.Map<Usuario>(createUsuarioDto);
            IdentityUser<int> usuarioIdentity = mapper.Map<IdentityUser<int>>(usuario);
            Task<IdentityResult> resultdoIdentity = userManager.CreateAsync(usuarioIdentity, createUsuarioDto.Password);

            if (resultdoIdentity.Result.Succeeded) return Result.Ok();
            return Result.Fail("Falha ao cadastrar Usuário!");
        }
    }
}

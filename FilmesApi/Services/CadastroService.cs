using AutoMapper;
using FilmesApi.Data.Dtos.Requests;
using FilmesApi.Data.Dtos.Usuario;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FilmesApi.Services
{
    public class CadastroService
    {
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser<int>> userManager;
        private readonly EmailService emailService;
        private readonly RoleManager<IdentityRole<int>> roleManager;

        public CadastroService(IMapper mapper,
                               UserManager<IdentityUser<int>> userManager,
                               EmailService emailService,
                               RoleManager<IdentityRole<int>> roleManager)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.emailService = emailService;
            this.roleManager = roleManager;
        }

        public async Task<string> CadastrarUsuario(CreateUsuarioDto createUsuarioDto)
        {
            Usuario usuario = mapper.Map<Usuario>(createUsuarioDto);
            IdentityUser<int> usuarioIdentity = mapper.Map<IdentityUser<int>>(usuario);
            IdentityResult resultdoIdentity = await userManager
                .CreateAsync(usuarioIdentity, createUsuarioDto.Password);

            await roleManager.CreateAsync(new IdentityRole<int>("admin"));
            await userManager.AddToRoleAsync(usuarioIdentity, "admin");


            if (resultdoIdentity.Succeeded)
            {
                var code = userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity).Result;

                var encodedCode = HttpUtility.UrlEncode(code);
                emailService.EnviarEmail(new[] { usuarioIdentity.Email },
                    "Link de ativação", usuarioIdentity.Id, encodedCode);
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

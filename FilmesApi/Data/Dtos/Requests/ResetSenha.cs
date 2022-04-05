
using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.Dtos.Requests
{
    public class ResetSenha
    {
        [Required]
        public string Email { get; set; }
    }
}
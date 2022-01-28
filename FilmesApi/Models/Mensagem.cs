using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesApi.Models
{
    public class Mensagem
    {
        public Mensagem(IEnumerable<string> destinatario, string assunto, int usuarioId, string codigo)
        {
            Destinatario = new List<MailboxAddress>();
            Destinatario.AddRange(destinatario.Select(d => new MailboxAddress(d)));
            Assunto = assunto;
            Conteudo = $"https://localhost:5001/ativa?UsuarioId={usuarioId}&CodigoAtivacao={codigo}";
        }

        public List<MailboxAddress> Destinatario {get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }
    }
}

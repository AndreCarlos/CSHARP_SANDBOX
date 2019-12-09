using System;

//importamos as namespaces que nos auxiliam a enviar emails
using System.Net;
using System.Net.Mail;

///Helper ou Util são termos arquiteturais. São classes utilitárias,
///são códigos específicos que podem ser utilizadas em todo o projeto
///
namespace LGroup.Helper
{
    public sealed class EmailHelper
    {
        /// <summary>
        /// String é a mesma coisa que string 
        /// Int32 é a mesma coisa que int
        /// temos nomes espeicificos do C# e nomes genéricos (globais)
        /// da plataforma .Net
        /// </summary>
        /// <param name="de_"></param>
        /// <param name="para_"></param>
        /// <param name="assunto_"></param>
        /// <param name="mensagem_"></param>
        public static void Enviar(string de_, string para_, string assunto_, string mensagem_)
        {
            var servidor = new SmtpClient("smtp.gmail.com", 587);
            servidor.EnableSsl = true;
            servidor.Credentials = new NetworkCredential("andre.leite.carlos@gmail.com", "AN28dr&&");

            var email = new MailMessage(de_, para_);
            email.Subject = assunto_;
            email.Body = mensagem_;

            servidor.Send(email);
        }
    }
}

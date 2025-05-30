using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Application.Interfaces;
using Application.Models;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {

        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;   
        }
        public void SendEmail(EmailDto request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("Email:UserName").Value));
            email.To.Add(MailboxAddress.Parse(request.Para));
            email.Subject = request.Asunto;
            email.Body = new TextPart(TextFormat.Html) 
            {
                Text = request.Contenido
            };


            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(
                _config.GetSection("Email:Host").Value,
                 Convert.ToInt32(_config.GetSection("Email:Port").Value),
                 SecureSocketOptions.StartTls
            );

            smtp.Authenticate(_config.GetSection("Email:UserName").Value, _config.GetSection("Email:PassWord").Value);
            smtp.Send(email);
            smtp.Disconnect(true);

        }
    }
}

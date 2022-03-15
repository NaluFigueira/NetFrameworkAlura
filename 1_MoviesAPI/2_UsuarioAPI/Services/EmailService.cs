using System;
using System.Collections.Generic;
using _2_UsuarioAPI.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace _2_UsuarioAPI.Services
{
    public class EmailService
    {
        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendConfirmationEmail(IEnumerable<string> receivers, string subject, int userId, string confirmationCode)
        {
            Message message = new Message(receivers, subject, userId, confirmationCode);
            MimeMessage emailMessage = CreateEmailBody(message);

            Send(emailMessage);
        }

        private void Send(MimeMessage emailMessage)
        {
            using(var client = new SmtpClient())
            {
                try
                {
                    client.Connect(
                        _configuration.GetValue<string>("EmailSettings:SmtpServer"),
                        _configuration.GetValue<int>("EmailSettings:Port"),
                        true
                    );
                    client.AuthenticationMechanisms.Remove("XOUATH2");
                    client.Authenticate(
                        _configuration.GetValue<string>("EmailSettings:From"),
                        _configuration.GetValue<string>("EmailSettings:Password")
                    );
                    client.Send(emailMessage);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private MimeMessage CreateEmailBody(Message message)
        {
            MimeMessage emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(
                "sender", _configuration.GetValue<string>("EmailSettings:From")
            ));
            emailMessage.To.AddRange(message.Receiver);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = message.Body,
            };

            return emailMessage;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MimeKit;

namespace _2_UsuarioAPI.Models
{
    public class Message
    {
        public List<MailboxAddress> Receiver { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public Message(IEnumerable<string> receivers, string subject, int userId, string activationCode)
        {
            Receiver = new List<MailboxAddress>();
            Receiver.AddRange(receivers.Select(receiver => new MailboxAddress("receiver", receiver)));
            Subject = subject;
            Body = $"http://localhost:6000/Activate?Id={userId}&ActivationCode={activationCode}";
        }
    }
}

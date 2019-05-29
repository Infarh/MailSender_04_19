using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MailSender.lib.Entityes;
using MailSender.lib.Services.Interfaces;
using MailMessage = MailSender.lib.Entityes.MailMessage;

namespace MailSender.lib.Services
{
    public class SmtpMailSenderService : IMailSenderService
    {
        public IMailSender CreateSender(Server server) => new SmtpMailSender(server.Address, server.Port, server.UseSSL, server.Login, server.Password);
    }

    internal class SmtpMailSender : IMailSender
    {
        private readonly string _Address;
        private readonly int _Port;
        private readonly bool _UseSsl;
        private readonly string _Login;
        private readonly string _Password;

        public SmtpMailSender(string Address, int Port, bool UseSSL, string Login, string Password)
        {
            _Address = Address;
            _Port = Port;
            _UseSsl = UseSSL;
            _Login = Login;
            _Password = Password;
        }

        public void Send(MailMessage Message, Sender From, Recipient To)
        {
            using(var client = new SmtpClient(_Address, _Port) {  Credentials = new NetworkCredential(_Login, _Password)})
            using (var message = new System.Net.Mail.MailMessage())
            {
                 message.From = new MailAddress(From.Email, From.Name);
                 message.To.Add(new MailAddress(To.Email, To.Name));
                 message.Subject = Message.Subject;
                 message.Body = Message.Body;

                 client.Send(message);
            }
        }

        public void Send(MailMessage Message, Sender From, IEnumerable<Recipient> To)
        {
            foreach (var recipient in To)
                Send(Message, From, recipient);
        }

        public void SendParallel(MailMessage Message, Sender From, IEnumerable<Recipient> To)
        {
            foreach (var recipient in To)
                ThreadPool.QueueUserWorkItem(_ => Send(Message, From, recipient));
        }

        public async Task SendAsync(MailMessage Message, Sender From, Recipient To)
        {
            using (var client = new SmtpClient(_Address, _Port) { Credentials = new NetworkCredential(_Login, _Password) })
            using (var message = new System.Net.Mail.MailMessage())
            {
                message.From = new MailAddress(From.Email, From.Name);
                message.To.Add(new MailAddress(To.Email, To.Name));
                message.Subject = Message.Subject;
                message.Body = Message.Body;

                await client.SendMailAsync(message).ConfigureAwait(false);
            }
        }

        public async Task SendAsync(MailMessage Message, Sender From, IEnumerable<Recipient> To)
        {
            await Task.WhenAll(To.Select(to => SendAsync(Message, From, to)));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.lib.Entityes;

namespace MailSender.lib.Services.Interfaces
{
    public interface IMailSenderService
    {
        IMailSender CreateSender(Server server);
    }

    public interface IMailSender
    {
        void Send(MailMessage Message, Sender From, Recipient To);

        void Send(MailMessage Message, Sender From, IEnumerable<Recipient> To);

        void SendParallel(MailMessage Message, Sender From, IEnumerable<Recipient> To);

        Task SendAsync(MailMessage Message, Sender From, Recipient To);

        Task SendAsync(MailMessage Message, Sender From, IEnumerable<Recipient> To);
    }
}

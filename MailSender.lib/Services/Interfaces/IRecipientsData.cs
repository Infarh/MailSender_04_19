using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.lib.Data.Linq2SQL;

namespace MailSender.lib.Services.Interfaces
{
    public interface IRecipientsData
    {
        IEnumerable<Recipient> GetAll();

        int Create(Recipient recipient);

        void Write(Recipient recipient);

        void SaveChanges();
    }
}

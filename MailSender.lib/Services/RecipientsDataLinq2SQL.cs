using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.lib.Data.Linq2SQL;
using MailSender.lib.Services.Interfaces;

namespace MailSender.lib.Services
{
    public class RecipientsDataLinq2SQL : IRecipientsData
    {
        private readonly MailSenderDB _db;

        public RecipientsDataLinq2SQL(MailSenderDB db)
        {
            _db = db;
        }

        public IEnumerable<Recipient> GetAll()
        {
            return _db.Recipient.ToArray();
        }
    }
}

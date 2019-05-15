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

        public int Create(Recipient recipient)
        {
            if (recipient.Id != 0)
                return recipient.Id;
            _db.Recipient.InsertOnSubmit(recipient);
            SaveChanges();
            return recipient.Id;
        }

        public void Write(Recipient recipient)
        {   
            if(_db.Recipient.Contains(recipient)) return;
            _db.Recipient.InsertOnSubmit(recipient);
        }

        public void SaveChanges() => _db.SubmitChanges();
    }
}

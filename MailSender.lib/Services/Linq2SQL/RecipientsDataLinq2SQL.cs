using System.Collections.Generic;
using System.Linq;
using MailSender.lib.Entityes;
using MailSender.lib.Services.Interfaces;

namespace MailSender.lib.Services.Linq2SQL
{
    public class RecipientsDataLinq2SQL : IRecipientsData
    {
        private readonly Data.Linq2SQL.MailSenderDB _db;

        public RecipientsDataLinq2SQL(Data.Linq2SQL.MailSenderDB db) => _db = db;

        public IEnumerable<Recipient> GetAll() => _db.Recipient.Select(r => new Recipient { Id = r.Id, Name = r.Name, Email = r.Email }).ToArray();

        public Recipient GetById(int id)
        {
            var db_recipient = _db.Recipient.FirstOrDefault(i => i.Id == id);
            return new Recipient
            {
                Id = db_recipient.Id,
                Name = db_recipient.Name,
                Email = db_recipient.Email
            };
        }

        public int Add(Recipient recipient)
        {
            if (_db.Recipient.Any(r => r.Id == recipient.Id))
                return recipient.Id;
            _db.Recipient.InsertOnSubmit(new Data.Linq2SQL.Recipient
            {
                Name = recipient.Name,
                Email = recipient.Email
            });
            SaveChanges();
            return recipient.Id;
        }

        public void Edit(Recipient recipient)
        {
            var db_recipient = _db.Recipient.FirstOrDefault(r => r.Id == recipient.Id);
            if (db_recipient is null)
            {
                Add(recipient);
                return;
            }

            db_recipient.Name = recipient.Name;
            db_recipient.Email = recipient.Email;

            SaveChanges();  
        }

        public void Remove(int id)
        {
            var db_recipient = _db.Recipient.FirstOrDefault(r => r.Id == id);
            if(db_recipient is null) return;

            _db.Recipient.DeleteOnSubmit(db_recipient);
            SaveChanges();
        }

        public void SaveChanges() => _db.SubmitChanges();
    }
}

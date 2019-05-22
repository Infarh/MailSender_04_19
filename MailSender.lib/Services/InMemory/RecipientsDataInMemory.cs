using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.lib.Data;
using MailSender.lib.Entityes;
using MailSender.lib.Services.Interfaces;

namespace MailSender.lib.Services.InMemory
{
    public class RecipientsDataInMemory : DataInMemory<Recipient>, IRecipientsData
    {
        public RecipientsDataInMemory()
        {
            _Items.AddRange(TestData.Senders.Select((s, i) => new Recipient
            {
                Id = i + 1,
                Name = s.Name,
                Email = s.Email
            }));
        }

        public override void Edit(Recipient item)
        {
            var db_item = GetById(item.Id);
            if(db_item is null || ReferenceEquals(db_item, item)) return;

            db_item.Name = item.Name;
            db_item.Email = item.Email;
        }
    }
}

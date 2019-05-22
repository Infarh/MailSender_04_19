using MailSender.lib.Data;
using MailSender.lib.Entityes;
using MailSender.lib.Services.Interfaces;

namespace MailSender.lib.Services.InMemory
{
    public class SendersDataInMemory : DataInMemory<Sender>, ISendersData
    {
        public SendersDataInMemory() => _Items.AddRange(TestData.Senders);

        public override void Edit(Sender item)
        {
            var db_item = GetById(item.Id);
            if (db_item is null || ReferenceEquals(db_item, item)) return;

            db_item.Name = item.Name;
            db_item.Email = item.Email;
        }
    }
}
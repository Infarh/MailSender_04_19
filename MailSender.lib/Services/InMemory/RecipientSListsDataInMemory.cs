using MailSender.lib.Entityes;
using MailSender.lib.Services.Interfaces;

namespace MailSender.lib.Services.InMemory
{
    public class RecipientsListsDataInMemory : DataInMemory<RecipientsList>, IRecipientsListsData
    {
        public override void Edit(RecipientsList item)
        {
            var db_item = GetById(item.Id);
            if (db_item is null || ReferenceEquals(db_item, item)) return;

            db_item.Name = item.Name;
            db_item.Recipients = item.Recipients;
        }
    }
}
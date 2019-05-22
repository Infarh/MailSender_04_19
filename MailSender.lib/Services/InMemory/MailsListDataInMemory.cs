using MailSender.lib.Entityes;
using MailSender.lib.Services.Interfaces;

namespace MailSender.lib.Services.InMemory
{
    public class MailsListDataInMemory : DataInMemory<MailsList>, IMailsListsData
    {
        public override void Edit(MailsList item)
        {
            var db_item = GetById(item.Id);
            if (db_item is null || ReferenceEquals(db_item, item)) return;

            db_item.Name = item.Name;
            db_item.Messages = item.Messages;
        }
    }
}
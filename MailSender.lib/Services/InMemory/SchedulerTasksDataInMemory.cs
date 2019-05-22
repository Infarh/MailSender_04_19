using MailSender.lib.Entityes;
using MailSender.lib.Services.Interfaces;

namespace MailSender.lib.Services.InMemory
{
    public class SchedulerTasksDataInMemory : DataInMemory<SchedulerTask>, ISchedulerTasksData
    {
        public override void Edit(SchedulerTask item)
        {
            var db_item = GetById(item.Id);
            if (db_item is null || ReferenceEquals(db_item, item)) return;

            db_item.Time = item.Time;
            db_item.Sender = item.Sender;
            db_item.Server = item.Server;
            db_item.Messages = item.Messages;
            db_item.Recipients = item.Recipients;
        }
    }
}
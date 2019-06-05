using MailSender.lib.Data.EF;
using MailSender.lib.Entityes;
using MailSender.lib.Services.Interfaces;

namespace MailSender.lib.Services.EF
{
    public class SendersDataInEF : DataInEF<Sender>, ISendersData
    {
        public SendersDataInEF(MailSenderDBContext db) : base(db) { }

        public override void Edit(Sender item)
        {
            var db_item = GetById(item.Id);
            if(db_item is null) return;

            db_item.Name = item.Name;
            db_item.Email = item.Email;
        }
    }
}
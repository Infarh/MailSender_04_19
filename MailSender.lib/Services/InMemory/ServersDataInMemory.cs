using MailSender.lib.Data;
using MailSender.lib.Entityes;
using MailSender.lib.Services.Interfaces;

namespace MailSender.lib.Services.InMemory
{
    public class ServersDataInMemory : DataInMemory<Server>, IServersData
    {
        public ServersDataInMemory() => _Items.AddRange(TestData.Servers);

        public override void Edit(Server item)
        {
            var db_item = GetById(item.Id);
            if (db_item is null || ReferenceEquals(db_item, item)) return;

            db_item.Address = item.Address;
            db_item.Port = item.Port;
            db_item.UseSSL = item.UseSSL;
            db_item.Login = item.Login;
            db_item.Password = item.Password;
        }
    }
}
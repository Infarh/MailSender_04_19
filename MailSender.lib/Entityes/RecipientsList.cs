using System.Collections.Generic;
using MailSender.lib.Data.Linq2SQL;
using MailSender.lib.Entityes.Base;

namespace MailSender.lib.Entityes
{
    public class RecipientsList : BaseEntity
    {
        public string Name { get; set; }

        public IEnumerable<Recipient> Recipients { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.lib.Services.Interfaces
{
    public interface IDataService<T>
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        int Add(T item);

        void Edit(T item);

        void Remove(int id);

        void SaveChanges();
    }
}

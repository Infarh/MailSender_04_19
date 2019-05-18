using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.lib.Entityes.Base;
using MailSender.lib.Services.Interfaces;

namespace MailSender.lib.Services.InMemory
{
    public abstract class DataInMemory<T> : IDataService<T> where T : BaseEntity
    {
        protected readonly List<T> _Items = new List<T>();

        public IEnumerable<T> GetAll() => _Items;

        public T GetById(int id)
        {
            if(id < 0)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Значение id должно быть больше 0");
            return _Items.FirstOrDefault(item => item.Id == id);
        }

        public int Add(T item)
        {
            if(_Items.Contains(item))
                return 0;
            item.Id = _Items.Count == 0 ? 1 : _Items.Max(i => i.Id) + 1;
            _Items.Add(item);
            return item.Id;
        }

        public abstract void Edit(T item);

        public void Remove(int id)
        {
            var item = GetById(id);
            if(item is null) return;
            _Items.Remove(item);
        }

        public void SaveChanges() { }
    }
}

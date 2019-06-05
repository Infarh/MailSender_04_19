using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.lib.Data.EF;
using MailSender.lib.Entityes.Base;
using MailSender.lib.Services.Interfaces;

namespace MailSender.lib.Services.EF
{
    public abstract class DataInEF<T> : IDataService<T>/*, IDisposable*/ where T : BaseEntity
    {
        private MailSenderDBContext _db;
        private DbSet<T> _DbSet;

        protected DataInEF(MailSenderDBContext db)
        {
            _db = db;
            _DbSet = db.Set<T>();
        }

        public IEnumerable<T> GetAll() => _DbSet.AsEnumerable();

        public T GetById(int id) => _DbSet.FirstOrDefault(item => item.Id == id);

        public int Add(T item)
        {
            if (_DbSet.Any(i => i.Id == item.Id)) return item.Id;
            _DbSet.Add(item);
            return item.Id;
        }

        public abstract void Edit(T item);

        public void Remove(int id)
        {
            var db_item = GetById(id);
            if (db_item != null)
                _DbSet.Remove(db_item);
        }

        public void SaveChanges() => _db.SaveChanges();

        //public void Dispose() => _db.Dispose();
    }
}

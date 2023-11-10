using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace AuthoritySignClient.DataBase.Implementations
{
    public class OraDataBaseWrapper : IDataBase
    {
        private DbContextTransaction _transaction = null;
        private OraDbContext _context;

        public OraDataBaseWrapper()
        {
            _context = new OraDbContext(Utils.ConfigSet.ServersConfig.GetInstance().GetConnectionString());
        }

        public override IEnumerable<T> SelectAll<T>()
        {
            return _context.Set<T>();
        }

        public override void Add<T>(IEnumerable<T> objects)
        {
            if (objects == null)
                throw new Exception("Коллекция null не может быть добавлена в базу");

            if (objects.Count() == 0)
                throw new Exception("Нулевая коллекция объектов не может быть добавлена в базу");

            if (_transaction == null)
                BeginTransaction();

            _context.Set<T>().AddRange(objects);
        }

        public override void Add<T>(T obj)
        {
            if (obj == null)
                throw new Exception("Отсутствует экземпляр объекта");

            if (_transaction == null)
                BeginTransaction();

            _context.Set<T>().Add(obj);
        }

        public override void Delete<T>(IEnumerable<T> objects)
        {
            if (objects == null)
                throw new Exception("Коллекция null не может быть удалена");

            if (objects.Count() == 0)
                throw new Exception("Нулевая коллекция объектов не может быть удалена");

            if (_transaction == null)
                BeginTransaction();

            _context.Set<T>().RemoveRange(objects);
        }

        public override void Delete<T>(T obj)
        {
            if (obj == null)
                throw new Exception("Отсутствует экземпляр объекта");

            if (_transaction == null)
                BeginTransaction();

            _context.Set<T>().Remove(obj);
        }

        public override void RefreshObject<T>(T obj)
        {
            _context.Entry(obj)?.Reload();
        }

        public override void OpenConnection()
        {
            if (_context.Database.Connection.State != System.Data.ConnectionState.Open)
                _context.Database.Connection.Open();
        }

        public override IDisposable BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
            return _transaction;
        }

        public override void Rollback()
        {
            _transaction?.Rollback();
        }

        public override void Commit()
        {
            _context.SaveChanges();
            _transaction?.Commit();
            _transaction = null;
        }

        public override void Dispose()
        {
            _context.Dispose();
            base.Dispose();
        }
    }
}

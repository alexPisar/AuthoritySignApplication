using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthoritySignClient.DataBase
{
    public abstract class IDataBase : IDisposable
    {
        public virtual IEnumerable<T> Select<T>(Predicate<T> predicate) where T : IDataBaseObject
        {
            return SelectAll<T>().Where(s => predicate(s));
        }
        public abstract IEnumerable<T> SelectAll<T>() where T : IDataBaseObject;

        public virtual void LoadContext() { }

        #region Добавление
        public abstract void Add<T>(IEnumerable<T> objects) where T : IDataBaseObject;
        public abstract void Add<T>(T obj) where T : IDataBaseObject;
        #endregion

        #region Удаление
        public abstract void Delete<T>(IEnumerable<T> objects) where T : IDataBaseObject;

        public abstract void Delete<T>(T obj) where T : IDataBaseObject;
        #endregion

        #region Загрузка объекта из базы
        public abstract void RefreshObject<T>(T obj) where T : IDataBaseObject;
        #endregion

        #region Коммит
        public abstract void Commit();
        #endregion

        #region Подключение
        public virtual void OpenConnection(){}
        #endregion

        #region Начало транзакции
        public abstract IDisposable BeginTransaction();
        #endregion

        #region Откат
        public abstract void Rollback();
        #endregion

        public virtual void Dispose() { }
    }
}

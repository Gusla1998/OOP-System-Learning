using System;
using System.Collections.Generic;

namespace DAL
{
    public interface IRepository<T> where T : IEntity
    {
        IEnumerable<T> GetAll();
        T Get(int id);

        void Create(T item);
        void Delete(int Id);
        void Change(T item);
        IEnumerable<T> Find(Func<T, bool> predicate);
    }
}

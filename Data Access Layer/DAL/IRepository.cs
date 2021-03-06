using System;
using System.Collections.Generic;

namespace Gym.DAL
{
    public interface IRepository<T> : IDisposable where T: class
    {
        IEnumerable<T> GetAll();
        void Insert(T entity);
        void Delete(T entity);
        void Update(T entity);
        void Save();
    }
}

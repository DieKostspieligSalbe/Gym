using System;
using System.Collections.Generic;

namespace Gym.DAL
{
    public interface IRepository<T> : IDisposable where T: class
    {
        IEnumerable<T> GetAll();
        T GetByID(int id);
        void Insert(T entity);
        void Delete(int id);
        void Update(T student);
        void Save();
    }
}

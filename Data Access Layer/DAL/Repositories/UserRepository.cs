using Gym.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gym.DAL.Repositories
{
    public class UserRepository : IRepository<UserDAL>
    {
        private GeneralContext _context;
        private bool disposed = false;

        public UserRepository(GeneralContext context)
        {
            _context = context;
        }

        public void Delete(UserDAL user)
        {
            UserDAL currentUser = _context.Users.FirstOrDefault(x => x.Login == user.Login);
            _context.Users.Remove(user);
        }

        public IEnumerable<UserDAL> GetAll()
        {
            return _context.Users.ToList();
        }

        public UserDAL GetByID(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public UserDAL GetByLoginPassword(UserDAL user)
        {
            return _context.Users.FirstOrDefault(u => u.Login == user.Login && u.Password == user.Password);
        }

        public UserDAL GetByLogin(string login)
        {
            return _context.Users.FirstOrDefault(u => u.Login == login);
        }

        public void Insert(UserDAL user)
        {
            _context.Users.Add(user);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(UserDAL user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

using Gym.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gym.DAL
{
    public class UserRepository : IRepository<User>
    {
        private UserContext context;
        private bool disposed = false;

        public UserRepository(UserContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            User user = context.Users.FirstOrDefault(x => x.Id == id);
            context.Users.Remove(user);
        }

        public IEnumerable<User> GetAll()
        {
            return context.Users.ToList();
        }

        public User GetByID(int id)
        {
            return context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User GetByLoginPassword(User user)
        {
            return context.Users.FirstOrDefault(u => u.Login == user.Login && u.Password == user.Password);
        }

        public User GetByLogin(string login)
        {
            return context.Users.FirstOrDefault(u => u.Login == login);
        }

        public void Insert(User user)
        {
            context.Users.Add(user);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(User user)
        {
            context.Entry(user).State = EntityState.Modified;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void FillDatabaseWithUsers()
        {
            context.Users.Add(new User() { Login = "Admin", Password = "Password" });
            context.Users.Add(new User() { Login = "User", Password = "Pass" });
            context.SaveChanges();
        }
    }
}

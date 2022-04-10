using Gym.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gym.DAL.Repositories
{
    public class UserRepository : IRepository<UserDAL>
    {
        private GeneralContext context;
        private bool disposed = false;

        public UserRepository(GeneralContext context)
        {
            this.context = context;
        }

        public void Delete(UserDAL user)
        {
            UserDAL currentUser = context.Users.FirstOrDefault(x => x.Login == user.Login);
            context.Users.Remove(user);
        }

        public IEnumerable<UserDAL> GetAll()
        {
            return context.Users.ToList();
        }

        public UserDAL GetByID(int id)
        {
            return context.Users.FirstOrDefault(x => x.Id == id);
        }

        public UserDAL GetByLoginPassword(UserDAL user)
        {
            return context.Users.FirstOrDefault(u => u.Login == user.Login && u.Password == user.Password);
        }

        public UserDAL GetByLogin(string login)
        {
            return context.Users.FirstOrDefault(u => u.Login == login);
        }

        public void Insert(UserDAL user)
        {
            context.Users.Add(user);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(UserDAL user)
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
            context.Users.Add(new UserDAL() { Login = "Admin", Password = "Password" });
            context.Users.Add(new UserDAL() { Login = "User", Password = "Pass" });
            context.SaveChanges();
        }
    }
}

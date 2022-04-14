using Gym.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DAL.DAL.Repositories
{
    public class EquipRepository : IRepository<EquipDAL>
    {
        private GeneralContext _context;
        private bool disposed = false;

        public EquipRepository(GeneralContext context)
        {
            _context = context;
        }
        public void Delete(EquipDAL entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EquipDAL> GetAll()
        {
            return _context.Equipment.ToList();
        }

        public void Insert(EquipDAL entity)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(EquipDAL entity)
        {
            throw new NotImplementedException();
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

using Gym.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DAL.DAL.Repositories
{
    public class TrainingProgramRepository : IRepository<TrainingProgramDAL>
    {
        private GeneralContext _context;
        private bool disposed = false;

        public TrainingProgramRepository(GeneralContext context)
        {
            _context = context;
        }
        public void Delete(TrainingProgramDAL entity)
        {
            TrainingProgramDAL program = _context.TrainingPrograms.FirstOrDefault(x => x.Name == entity.Name && x.Creator == entity.Creator);
            _context.TrainingPrograms.Remove(program);
        }

        public IEnumerable<TrainingProgramDAL> GetAll()
        {
            return _context.TrainingPrograms.ToList();
        }
        public void DeleteIfNoUsers(TrainingProgramDAL entity)
        {
            var foundEntity = _context.TrainingPrograms.FirstOrDefault(x => x.Name == entity.Name && x.Creator == entity.Creator);
            if (foundEntity.Users.Count == 0)
            {
                Delete(foundEntity);
            }
        }

        public void Insert(TrainingProgramDAL entity)
        {
            _context.TrainingPrograms.Add(entity);
        }

        public void InsertWithLists(TrainingProgramDAL entity, TrainingProgramDAL entityForProperties)
        {
            entity.MuscleList = new();
            entity.ExerciseList = new();
            _context.TrainingPrograms.Add(entity);
            _context.SaveChanges();
            entity.MuscleList.AddRange(entityForProperties.MuscleList);
            entity.ExerciseList.AddRange(entityForProperties.ExerciseList);
            _context.SaveChanges();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(TrainingProgramDAL entity)
        {
            _context.Update(entity);//Entry(entity).State = EntityState.Modified;
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

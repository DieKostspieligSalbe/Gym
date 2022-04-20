using AutoMapper;
using Gym.DAL.Models;
using Gym.DAL.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Gym.DAL
{
    public class GetDataFromDAL
    {
        private readonly GeneralContext _context;
        private readonly IMapper _mapper;

        public GetDataFromDAL(GeneralContext context)
        {
            _context = context;
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfileDAL>());
            _mapper = new Mapper(mapperConfig);
        }

        public List<EquipBL> GetEquipment()
        {
            List<EquipDAL> equipUnmappedList = _context.Equipment.Include(e => e.ExercisesList).ToList();
            List<EquipBL> equipMappedList = _mapper.Map<List<EquipDAL>, List<EquipBL>>(equipUnmappedList);
            return equipMappedList;
        }
        public List<ExerciseBL> GetExercises()
        {
            List<ExerciseDAL> exUnmappedList = _context.Exercises.Include(e => e.PrimaryMuscleList).Include(e => e.SecondaryMuscleList).Include(e => e.EquipList).ToList();
            List<ExerciseBL> exMappedList = _mapper.Map<List<ExerciseDAL>, List<ExerciseBL>>(exUnmappedList);
            return exMappedList;
        }
        public List<ExerciseDAL> GetExercisesDAL()
        {
            List<ExerciseDAL> exList = _context.Exercises.Include(e => e.PrimaryMuscleList).Include(e => e.SecondaryMuscleList).Include(e => e.EquipList).ToList();
            return exList;
        }
        public List<MuscleBL> GetMuscles()
        {
            List<MuscleDAL> muscleUnmappedList = _context.Muscles.Include(m => m.PrimaryExList).Include(m => m.SecondaryExList).ToList();
            List<MuscleBL> muscleMappedList = _mapper.Map<List<MuscleDAL>, List<MuscleBL>>(muscleUnmappedList);
            return muscleMappedList;
        }
        public List<MuscleDAL> GetMusclesDAL()
        {
            List<MuscleDAL> muscleList = _context.Muscles.Include(m => m.PrimaryExList).Include(m => m.SecondaryExList).ToList();
            return muscleList;
        }

    }
}

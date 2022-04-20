using AutoMapper;
using Gym.DAL.Models;

namespace Gym.DAL.Mapping
{
    public class MapperProfileDAL : Profile
    {
        public MapperProfileDAL()
        {
            CreateMap<MuscleDAL, MuscleBL>().ReverseMap();
            CreateMap<ExerciseDAL, ExerciseBL>().ReverseMap();
            CreateMap<EquipDAL, EquipBL>().ReverseMap();
            CreateMap<TrainingProgramDAL, TrainingProgramBL>().ReverseMap();
        }
    }
}

using AutoMapper;
using Gym.DAL.Models;
using Gym.MVC.Models;

namespace Gym.MVC.Mapping
{
    public class MapperProfileMVC : Profile
    {
        public MapperProfileMVC() 
        {
            CreateMap<ExerciseDAL, ExerciseViewModel>().ReverseMap();
            CreateMap<EquipDAL, GymEquipViewModel>().ReverseMap();
        }
    }
}

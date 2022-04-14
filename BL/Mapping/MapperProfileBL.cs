using AutoMapper;
using Gym.BL.Models;
using Gym.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BL.Mapping
{
    public class MapperProfileBL : Profile
    {
        public MapperProfileBL()
        {
            CreateMap<ExerciseBL, ExercisePR>().ReverseMap();
            CreateMap<MuscleBL, MusclePR>().ReverseMap();
            CreateMap<ExerciseDAL, ExerciseBL>().ReverseMap(); //remove from here
            CreateMap<MuscleDAL, MuscleBL>().ReverseMap();
            CreateMap<EquipBL, EquipPR>().ReverseMap();
        }
    }
}

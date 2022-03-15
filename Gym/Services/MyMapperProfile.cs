using AutoMapper;
using DAL.DAL;
using Gym.Models;

namespace Gym.Services
{
    public class MyMapperProfile : Profile
    {
        public MyMapperProfile()
        {
            CreateMap<UserLoginViewModel, UserDAL>().ReverseMap();
        }
    }
}

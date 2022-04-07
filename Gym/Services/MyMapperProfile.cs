using AutoMapper;
using Gym.DAL.Models;
using Gym.MVC.Models;

namespace Gym.MVC.Services
{
    public class MyMapperProfile : Profile
    {
        public MyMapperProfile()
        {
            CreateMap<UserLoginViewModel, UserDAL>().ReverseMap();
        }
    }
}

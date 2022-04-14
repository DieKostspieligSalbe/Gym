using AutoMapper;
using Gym.DAL;
using Gym.DAL.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Gym.MVC.Controllers
{
    [Route("[controller]")]
    public class GymEquipmentController : Controller
    {
        private readonly EquipRepository _equipRepository;
        private readonly GeneralContext _context;
        //private readonly IMapper _mapper;
        public GymEquipmentController(GeneralContext context, IMapper mapper)
        {
            _context = context;
            _equipRepository = new EquipRepository(context);
            //_mapper = mapper;
        }
        public IActionResult Index()
        {

            return View(_equipRepository.GetAll());
        }
    }
}

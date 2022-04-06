using Microsoft.AspNetCore.Mvc;

namespace Gym.Controllers
{
    public class TrainingProgramsController : Controller
    {
        public IActionResult Index()
        {
            return View();
            //list of programs from db
        }
    }
}

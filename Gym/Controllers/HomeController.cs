using DAL.DAL;
using Gym.Models;
using Gym.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(); //will be the main page with muscle layout perhaps
        }


        //[HttpGet]
        //[Route("ProcessMuscleClick")]
        //public IActionResult ProcessMuscleClick(int id)
        //{
        //    var sentMuscle = MuscleStorage.muscleList.FirstOrDefault(x => x.MuscleType == (MuscleType)id);
        //    var selectedMuscle = SelectedMuscleStorage.muscleList.FirstOrDefault(x => x == sentMuscle);
        //    if (selectedMuscle is null)
        //    {
        //        SelectedMuscleStorage.muscleList.Add(sentMuscle);
        //    }
        //    else
        //    {
        //        SelectedMuscleStorage.muscleList.Remove(sentMuscle);
        //    }
        //    return PartialView("SelectedMuscles", SelectedMuscleStorage.muscleList);
        //}




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

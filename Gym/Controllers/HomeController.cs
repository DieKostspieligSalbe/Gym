using BL;
using DAL.DAL;
using Gym.Models;
using Gym.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
        private readonly GeneralContext _context;

        public HomeController(ILogger<HomeController> logger, GeneralContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //DbFiller dbFiller = new(_context);
            //dbFiller.FillDatabase();
            return View();
        }


        [HttpPost]
        [Route("ProcessMuscleSubmit")]
        public IActionResult ProcessMuscleSubmit()
        {
            int[] testIdList = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20};
            int[] testList = new[] {9, 10, 11};

            TrainingProgramBuilder builder = new(_context);
            var result = builder.Calculate(testIdList, 1);
            string json = JsonConvert.SerializeObject(result);
            return Json(json);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

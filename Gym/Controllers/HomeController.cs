using Gym.BL;
using Gym.DAL;
using Gym.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace Gym.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, GeneralContext context)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //DbFiller dbFiller = new();
            //dbFiller.FillDatabase();
            return View();
        }


        [HttpPost]
        [Route("ProcessMuscleSubmit")]
        public IActionResult ProcessMuscleSubmit([FromBody] MuscleSubmitModel args)
        {
            bool listSuccess = true;
            int[] idList = Array.ConvertAll(args.IdList, id => {
                bool success = int.TryParse(id, out int result);
                if (success == false)
                {
                    listSuccess = false;
                }
                return result;
                });
            bool intensitySuccess = int.TryParse(args.Intensity, out int intensity);

            if (listSuccess && intensitySuccess)
            {
                TrainingProgramBuilder builder = new();
                var result = builder.Calculate(idList, intensity, out bool calcSuccess);
                if (calcSuccess == false)
                {
                    return BadRequest("Our database didn't manage to find what you wanted :(");
                }
                string json = JsonConvert.SerializeObject(result); //how to deal with eternal loop
                return Ok(json);
            }
            else return BadRequest("The data sent was incorrect");

        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

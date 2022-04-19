using Gym.BL;
using Gym.BL.Models.ModelsView;
using Gym.DAL;
using Gym.DAL.DAL.Repositories;
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
        public IActionResult ProcessMuscleSubmit([FromBody] MuscleSubmitViewModel model)
        {
            if (model.IdList is null || model.Intensity is null)
            {
                return BadRequest("What did you expect?");
            }
            bool checkSuccess = DataCheckerOnPost(model, out int[] idList, out int intensity);
            if (checkSuccess)
            {
                if (idList.Length == 0)
                {
                    return BadRequest("What did you expect?");
                }
                MuscleViewModel muscleModel = new(); //maybe this logic can go into mapper but how? Where this model should be as well?
                muscleModel.IdList = idList;
                muscleModel.Intensity = intensity;
                TrainingProgramBuilder builder = new(new GetDataFromDAL(new GeneralContext())); //what the heck?
                var result = builder.Calculate(muscleModel, out bool calcSuccess);
                if (calcSuccess == false)
                {
                    return BadRequest("Our database didn't manage to find what you wanted :(");
                }
                string json = JsonConvert.SerializeObject(result); 
                return Ok(json);
            }
            else return BadRequest("What did you expect?");

        }

        [HttpPost]
        [Route("SaveProgram")]
        public IActionResult SaveProgram([FromBody] TrainingProgramSaveModel model)
        {
            if (model.IdList is null || model.Name is null || model.Intensity is null)
            {
                return BadRequest("There's either no name or no muscles selected");
            }
            MuscleSubmitViewModel muscleModel = new();
            muscleModel.IdList = model.IdList;
            muscleModel.Intensity = model.Intensity; //use mapper here
            bool checkSuccess = DataCheckerOnPost(muscleModel, out int[] idList, out int intensity);
            string programName = model.Name;

            
            if (checkSuccess && !string.IsNullOrWhiteSpace(programName))
            {
                MuscleViewModel muscleViewModel = new(); //use mapper
                muscleViewModel.IdList = idList;
                muscleViewModel.Intensity = intensity;
                TrainingProgramViewModel trainingProgramModel = new();
                trainingProgramModel.MuscleModel = muscleViewModel;
                trainingProgramModel.Name = programName;
                trainingProgramModel.IsPublic = model.IsPublic;

                TrainingProgramProcessor processor = new(new TrainingProgramRepository(new GeneralContext()));
                bool processSuccess = processor.PutDataIntoDb(trainingProgramModel);
                if (processSuccess)
                {
                    return Ok("all good");
                }
            }
            return BadRequest("Something went wrong :(");
        }

        private bool DataCheckerOnPost(MuscleSubmitViewModel model, out int[] idList, out int intensity)
        {          
            idList = Array.Empty<int>();
            intensity = 0;
            if (model is null || model.IdList.Length == 0)
            {
                return false;
            }
            try
            {
                bool listSuccess = true;
                idList = Array.ConvertAll(model.IdList, id => {
                    bool success = int.TryParse(id, out int result);
                    if (success == false)
                    {
                        listSuccess = false;
                    }
                    return result;
                });
                bool intensitySuccess = int.TryParse(model.Intensity, out intensity);
                if (listSuccess == true && intensitySuccess == true)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

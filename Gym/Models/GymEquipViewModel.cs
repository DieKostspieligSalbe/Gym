using System.Collections.Generic;

namespace Gym.MVC.Models
{
    public class GymEquipViewModel
    {
        public string Name { get; set; }
        public string ImageLink { get; set; }
        public string Description { get; set; }
        public List<ExerciseViewModel> ExercisesList { get; set; }
    }
}

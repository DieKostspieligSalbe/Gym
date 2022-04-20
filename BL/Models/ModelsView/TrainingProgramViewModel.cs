using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BL.Models.ModelsView
{
    public class TrainingProgramViewModel
    {
        public MuscleViewModel MuscleModel { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
    }
}

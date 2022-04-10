using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BL.Models
{
    public class EquipPR
    {
        public string Name { get; set; }
        public List<ExercisePR> ExercisesList { get; set; }

        public string ImageLink { get; set; }
        public string Description { get; set; }
    }
}

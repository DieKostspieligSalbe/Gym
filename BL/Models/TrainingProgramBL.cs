using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BL.Models
{
    public class TrainingProgramBL
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public List<UserBL> Users { get; set; } 
        public List<MuscleBL> MuscleList { get; set; } 
        public List<ExerciseBL> ExerciseList { get; set; } 

        public string? Description { get; set; }

        public int Intensity { get; set; }
        public bool IsPublic { get; set; }
        public string Creator { get; set; }

    }
}

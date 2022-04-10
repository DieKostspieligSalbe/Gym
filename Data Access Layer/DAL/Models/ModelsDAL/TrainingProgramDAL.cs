using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gym.DAL.Models
{
    public class TrainingProgramDAL
    {
        [Required]
        public int Id { get; set; }
        public int Name { get; set; }
        public List<UserDAL> Users { get; set; } 
        public List<MuscleDAL> MuscleList { get; set; }
        public List<ExerciseDAL> ExerciseList { get; set; } 
        

        public string Description { get; set; }

        public int Intensity { get; set; }
        public bool IsPublic { get; set; }
        public string Creator { get; set; }
    }
}

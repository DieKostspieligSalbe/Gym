using System.Collections.Generic;

namespace Gym.DAL.Models
{
    public class TrainingProgramBL
    {
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

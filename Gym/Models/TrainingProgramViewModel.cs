using DAL.DAL;
using System.Collections.Generic;

namespace Gym.Models
{
    public class TrainingProgramViewModel
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public List<MuscleDAL> MuscleList { get; set; } = new();
        public List<ExerciseDAL> ExerciseList { get; set; } = new();

        public string Description { get; set; }

        public int Intensity { get; set; }
        public bool IsPublic { get; set; }
        public string Creator { get; set; }
    }
}

using DAL.DAL;
using System.Collections.Generic;
using System.Linq;

namespace Gym.Services
{
    public static class SelectedMuscleStorage
    {
        public static List<MuscleDAL> muscleList { get; set; } = new();
    }
}

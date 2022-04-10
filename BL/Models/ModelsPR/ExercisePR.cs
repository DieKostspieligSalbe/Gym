namespace Gym.BL.Models
{
    public class ExercisePR
    {
        public string Name { get; set; }
        public List<MusclePR> PrimaryMuscleList { get; set; }
        public List<MusclePR> SecondaryMuscleList { get; set; }

        public List<EquipPR> EquipList { get; set; } 
        public string ImageLink { get; set; }
        public string Description { get; set; }
    }
}

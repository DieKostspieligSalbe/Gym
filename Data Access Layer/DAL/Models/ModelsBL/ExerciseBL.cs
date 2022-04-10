using System.Collections.Generic;

namespace Gym.DAL.Models
{
    public class ExerciseBL
    {
        public string Name { get; set; }

        public List<MuscleBL> PrimaryMuscleList { get; set; }
        public List<MuscleBL> SecondaryMuscleList { get; set; }

        public List<EquipBL> EquipList { get; set; };
        public string ImageLink { get; set; }
        public string Description { get; set; }

        public bool IsCompound { get; set; }
        public bool IsEssential { get; set; }
    }
}

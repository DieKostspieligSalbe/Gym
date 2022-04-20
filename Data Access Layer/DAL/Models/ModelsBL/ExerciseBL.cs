using System.Collections.Generic;

namespace Gym.DAL.Models
{
    public class ExerciseBL
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<MuscleBL> PrimaryMuscleList { get; set; }
        public List<MuscleBL> SecondaryMuscleList { get; set; }

        public List<EquipBL> EquipList { get; set; }
        public string ImageLink { get; set; }
        public string Description { get; set; }

        public bool IsCompound { get; set; }
        public bool IsEssential { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            ExerciseBL exercise = obj as ExerciseBL;
            if (exercise == null) return false;
            else return Equals(exercise);
        }
        public override int GetHashCode()
        {
            return Id;
        }
        public bool Equals(ExerciseBL otherExercise)
        {
            if (otherExercise == null) return false;
            return (this.Name.Equals(otherExercise.Name));
        }
    }
}

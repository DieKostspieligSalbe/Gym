using Gym.Common.Enum;
using System.Collections.Generic;

namespace Gym.DAL.Models
{
    public class MuscleBL
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MuscleType MuscleType { get; set; }
        public MovementType MovementType { get; set; }
        public BodySectionType BodySectionType { get; set; }
        public BodyPartType BodyPartType { get; set; }

        public List<ExerciseBL> PrimaryExList { get; set; }
        public List<ExerciseBL> SecondaryExList { get; set; }
        public string ImageLink { get; set; }
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            MuscleBL muscle = obj as MuscleBL;
            if (muscle == null) return false;
            else return Equals(muscle);
        }
        public override int GetHashCode()
        {
            return Id;
        }
        public bool Equals(MuscleBL otherMuscle)
        {
            if (otherMuscle == null) return false;
            return (this.Name.Equals(otherMuscle.Name));
        }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gym.Common.Enum;

namespace Gym.DAL.Models
{
    public class MuscleDAL
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public MuscleType MuscleType { get; set; }
        public MovementType MovementType { get; set; }
        public BodySectionType BodySectionType { get; set; }
        public BodyPartType BodyPartType { get; set; }

        public List<ExerciseDAL> PrimaryExList { get; set; } 
        public List<ExerciseDAL> SecondaryExList { get; set; }

        public List<TrainingProgramDAL> InvolvedInPrograms { get; set; } 

        public string ImageLink { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
    }
}

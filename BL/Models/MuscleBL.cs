using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gym.Common.Enum;

namespace Gym.BL.Models
{
    public class MuscleBL
    {
        public string Name { get; set; }
        public MuscleType MuscleType { get; set; }
        public MovementType MovementType { get; set; }
        public BodySectionType BodySectionType { get; set; }
        public BodyPartType BodyPartType { get; set; }

        public List<ExerciseBL> PrimaryExList { get; set; } 
        public List<ExerciseBL> SecondaryExList { get; set; } 

        public List<TrainingProgramBL> InvolvedInPrograms { get; set; } 

        public string ImageLink { get; set; }
        public string Description { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.DAL
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

        public List<ExerciseDAL> PrimaryExList { get; set; } = new();
        public List<ExerciseDAL> SecondaryExList { get; set; } = new();
        public List<EquipDAL> PrimaryMachineList { get; set; } = new();
        public List<EquipDAL> SecondaryMachineList { get; set; } = new();
        public List<TrainingProgramDAL> InvolvedInPrograms { get; set; } = new();

        public string ImageLink { get; set; }
        public string Description { get; set; }
    }


}

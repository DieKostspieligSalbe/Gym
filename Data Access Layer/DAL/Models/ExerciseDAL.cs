using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gym.DAL.Models
{
    public class ExerciseDAL
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public List<MuscleDAL> PrimaryMuscleList { get; set; }
        public List<MuscleDAL> SecondaryMuscleList { get; set; } 

        public List<EquipDAL> EquipList { get; set; } = new();
        public List<TrainingProgramDAL> UsedInPrograms { get; set; } 
        public string ImageLink { get; set; }
        public string Description { get; set; }

        public bool IsCompound { get; set; }
        public bool IsEssential { get; set; }

    }


}

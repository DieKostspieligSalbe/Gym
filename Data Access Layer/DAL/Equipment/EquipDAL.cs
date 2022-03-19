using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.DAL
{
    public class EquipDAL
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<MuscleDAL> PrimaryMusclesList { get; set; } = new();
        public List<MuscleDAL> SecondaryMusclesList { get; set; } = new();
        public List<ExerciseDAL> ExercisesList { get; set; } = new();

        public string ImageLink { get; set; }
        public string Description { get; set; }

        public EquipType EquipmentType { get; set; }


    }


}

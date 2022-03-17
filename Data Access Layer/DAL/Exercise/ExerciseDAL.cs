﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.DAL
{
    public class ExerciseDAL
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public List<MuscleDAL> PrimaryMuscleList { get; set; } = new();
        public List<MuscleDAL> SecondaryMuscleList { get; set; } = new();

        public List<MachineDAL> MachineList { get; set; } = new();
        public List<TrainingProgramDAL> UsedInPrograms { get; set; } = new();

        public bool IsCompound { get; set; }
        public bool IsEssential { get; set; }

    }


}
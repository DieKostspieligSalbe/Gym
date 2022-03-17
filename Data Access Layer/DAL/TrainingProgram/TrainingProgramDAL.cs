﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.DAL
{
    public class TrainingProgramDAL
    {
        [Required]
        public int Id { get; set; }
        public int Name { get; set; }
        public List<UserDAL> Users { get; set; } = new();
        public List<MuscleDAL> MuscleList { get; set; } = new();
        public List<ExerciseDAL> ExerciseList { get; set; } = new();
        public int Intensity { get; set; }
        public bool IsPublic { get; set; }

    }


}

using Gym.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gym.DAL.Models
{
    public class EquipDAL
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }   
        public List<ExerciseDAL> ExercisesList { get; set; }

        public string ImageLink { get; set; }
        public string Description { get; set; }
        public bool IsEssential { get; set; }

        public EquipType EquipmentType { get; set; }


    }


}

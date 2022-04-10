using Gym.Common.Enum;
using System.Collections.Generic;

namespace Gym.DAL.Models
{
    public class EquipBL
    {
        public string Name { get; set; }
        public List<ExerciseBL> ExercisesList { get; set; }

        public string ImageLink { get; set; }
        public string Description { get; set; }
        public bool IsEssential { get; set; }

        public EquipType EquipmentType { get; set; }

    }
}

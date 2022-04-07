using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gym.Common.Enum;

namespace Gym.BL.Models
{
    public class EquipBL
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ExerciseBL> ExercisesList { get; set; }

        public string ImageLink { get; set; }
        public string Description { get; set; }
        public bool IsEssential { get; set; }

        public EquipType EquipmentType { get; set; }

    }
}

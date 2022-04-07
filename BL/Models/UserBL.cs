using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BL.Models
{
    public class UserBL
    {
        public int Id { get; set; }
        public string Login { get; set; }

        public List<TrainingProgramBL> ProgramList { get; set; }
    }
}

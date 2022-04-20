using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gym.DAL.Models
{
    public class UserDAL
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }

        public List<TrainingProgramDAL> ProgramList { get; set; } 

    }

   
}

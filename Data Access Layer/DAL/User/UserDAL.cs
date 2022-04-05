using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.DAL
{
    public class UserDAL
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }

        public List<TrainingProgramDAL> ProgramList { get; set; } = new();
        public UserProfileDAL UserProfile { get; set; }
    }

   
}

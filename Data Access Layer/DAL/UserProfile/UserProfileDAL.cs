using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.DAL
{
    public class UserProfileDAL
    {
        [Required]
        public int Id { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }

        public int UserId { get; set; }
        public UserDAL User { get; set; }
    }


}


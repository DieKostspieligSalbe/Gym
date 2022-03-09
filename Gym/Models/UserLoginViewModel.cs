using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gym.Models
{
    public class UserLoginViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        [Remote(action: "CheckUserLogin", controller: "Login", ErrorMessage = "That login already exists")]
        public string Login { get; set; }
        [Required,DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
    }

   
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gym.Models
{
    public class User
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Login { get; set; }
        [Required,DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
    }

    public static class UserRepository
    {
        public static List<User> userList { get; set; } = new();

        static UserRepository()
        {
            userList.Add(new User() { Login = "Admin", Password = "Password" });
            userList.Add(new User() { Login = "User", Password = "Pass" });
        }

    }
}

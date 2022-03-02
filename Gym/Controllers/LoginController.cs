using Gym.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Gym.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index() //page where you login
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessLogin([FromForm] User user)
        {
            if (ModelState.IsValid)
            {
                User foundUser = UserRepository.userList.FirstOrDefault(u => u.Login == user.Login && u.Password == user.Password);
                if (foundUser != null)
                {
                    await Authenticate(user.Login); 

                    return View("AuthSuccess", user);  //will lead to personal training plans
                }
                ModelState.AddModelError("", "Incorrect login or password");
            }
            return View("FailedAuth");          
        }

        private async Task Authenticate(string userLogin)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userLogin)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

    }
}

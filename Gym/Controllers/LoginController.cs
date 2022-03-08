using Gym.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Gym.DAL;
using Microsoft.AspNetCore.Authorization;

namespace Gym.Controllers
{
    public class LoginController : Controller
    {
        private UserRepository userRepository;

        public LoginController()
        {
            this.userRepository = new UserRepository(new UserContext());
            //userRepository.FillDatabaseWithUsers();
        }
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
                User foundUser = userRepository.GetByLoginPassword(user);
                if (foundUser != null)
                {
                    await Authenticate(user.Login);
                    return RedirectToAction("UserList", user);  //will lead to personal training plans
                }
                ModelState.AddModelError("", "Incorrect login or password");
            }
            return View("FailedAuth");          
        }

        public IActionResult CreateUser()
        {
            return View();
        }

        public IActionResult ProcessUserCreation([FromForm] User user)
        {
            userRepository.Insert(user);
            userRepository.Save();
            return RedirectToAction("UserList");
        }

        [Authorize]
        [HttpGet]
        public IActionResult UserList(User user)
        {
            ViewBag.UserName = user.Login;
            return View(userRepository.GetAll());
        }

        public IActionResult Delete(int id)
        {
            userRepository.Delete(id);
            userRepository.Save();
            return RedirectToAction("UserList");
        }

        public IActionResult CheckUserLogin(string login)
        {
            User foundUser = userRepository.GetByLogin(login);
            if (foundUser != null)
            {
                return Json(false);
            }
            else return Json(true);
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

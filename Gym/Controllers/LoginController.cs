using Gym.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using DAL.DAL;

namespace Gym.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private UserRepository userRepository;
        public LoginController()
        {
            this.userRepository = new UserRepository(new UserContext());
            //userRepository.FillDatabaseWithUsers();
        }

        [Route("Index")]
        [HttpGet]
        public IActionResult Index() //page where you login
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessLogin([FromForm] UserLoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                UserDAL newUser = new UserDAL 
                { 
                    Login = user.Login,
                    Password = user.Password
                };

                UserDAL foundUser = userRepository.GetByLoginPassword(newUser);
                if (foundUser != null)
                {
                    await Authenticate(user.Login);
                    return RedirectToAction("UserList", user);  //will lead to personal training plans
                }
                ModelState.AddModelError("", "Incorrect login or password");
            }
            return View("FailedAuth");          
        }

        [Route("CreateUser")]
        public IActionResult CreateUser()
        {
            return View();
        }

        public IActionResult ProcessUserCreation([FromForm] UserLoginViewModel user)
        {
            UserDAL newUser = new UserDAL
            {
                Login = user.Login,
                Password = user.Password
            };
            userRepository.Insert(newUser);
            userRepository.Save();
            return RedirectToAction("UserList");
        }

        [Authorize]
        [HttpGet]
        [Route("UserList")]
        public IActionResult UserList(UserLoginViewModel user)
        {
            ViewBag.UserName = user.Login;
            return View(userRepository.GetAll());
        }

        [Route("Delete")]
        public IActionResult Delete(UserLoginViewModel user)
        {
            UserDAL newUser = new UserDAL
            {
                Login = user.Login,
                Password = user.Password
            };
            userRepository.Delete(newUser);
            userRepository.Save();
            return RedirectToAction("UserList");
        }

        public IActionResult CheckUserLogin(string login)
        {
            UserDAL foundUser = userRepository.GetByLogin(login);
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

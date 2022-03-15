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
using AutoMapper;

namespace Gym.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private UserRepository userRepository;
        private UserContext userContext;
        private readonly IMapper mapper;
        public LoginController(UserContext context, IMapper mapper)
        {
            userContext = context;
            userRepository = new UserRepository(userContext);
            this.mapper = mapper;
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
                //UserDAL newUser = new UserDAL 
                //{ 
                //    Login = user.Login,
                //    Password = user.Password
                //};
                var userDAL = mapper.Map<UserDAL>(user);

                UserDAL foundUser = userRepository.GetByLoginPassword(userDAL);
                if (foundUser != null)
                {
                    await Authenticate(userDAL.Login);
                    return RedirectToAction("UserList", mapper.Map<UserLoginViewModel>(userDAL));  //will lead to personal training plans
                }
                ModelState.AddModelError("", "Incorrect login or password");
            }
            return View("FailedAuth");          
        }

        [HttpGet]
        [Route("CreateUser")]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [Route("ProcessUserCreation")]
        public IActionResult ProcessUserCreation([FromForm] UserLoginViewModel user)
        {       
            var userDAL = mapper.Map<UserDAL>(user);
            userRepository.Insert(userDAL);
            userRepository.Save();
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        [Route("UserList")]
        public IActionResult UserList(UserLoginViewModel user)
        {
            ViewBag.UserName = user.Login;
            return View(userRepository.GetAll());
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(UserLoginViewModel user)
        {
            var userDAL = mapper.Map<UserDAL>(user);
            userRepository.Delete(userDAL);
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

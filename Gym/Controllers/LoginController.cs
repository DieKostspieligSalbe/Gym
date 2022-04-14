using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Gym.DAL.Repositories;
using Gym.DAL;
using Gym.MVC.Models;
using Gym.DAL.Models;

namespace Gym.MVC.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private UserRepository userRepository;
        private readonly GeneralContext _context;
        private readonly IMapper _mapper;
        public LoginController(GeneralContext context, IMapper mapper)
        {
            _context = context;
            userRepository = new UserRepository(context);
            _mapper = mapper;
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
                // var userDAL = _mapper.Map<UserDAL>(user);

                UserDAL foundUser = userRepository.GetByLoginPassword(newUser);
                if (foundUser != null)
                {
                    await Authenticate(newUser.Login);
                    return RedirectToAction("UserList", _mapper.Map<UserLoginViewModel>(newUser));  //will lead to personal training plans
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
            //var userDAL = _mapper.Map<UserDAL>(user);
            UserDAL newUser = new UserDAL
            {
                Login = user.Login,
                Password = user.Password
            };
            userRepository.Insert(newUser);
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
            var userDAL = _mapper.Map<UserDAL>(user);
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

using Microsoft.AspNetCore.Mvc;
using T3PersonalWkSpcApp.Models;
using T3PersonalWkSpcApp.Services;

namespace T3PersonalWkSpcApp.Controllers
{
    public class UserController : Controller
    {
        private LoginService _loginService;

        public UserController(LoginService loginService)
        {
            _loginService = loginService;
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                user.Role = "Shopper";
                User usr = await _loginService.Register(user);
                HttpContext.Session.SetString("token", usr.Token);
                //return RedirectToAction("Index", "Customer");
                return RedirectToAction("Login", "User");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            try
            {
                user.Role = "";
                User usr = await _loginService.Login(user);
                //string username = user.Username;  //get userid here
                //var userFullDetails = await _loginService.GetUserDetails(username);
                HttpContext.Session.SetString("token", usr.Token);   //set userid next
                //HttpContext.Session.SetString("token", usr.Token);
                if(usr.Role == "Shopper")
                    return RedirectToAction("IndexShopper", "Product");
                else
                    return RedirectToAction("IndexAdmin", "Product");
            }
            catch
            {
                return View();
            }
        }
    }
}

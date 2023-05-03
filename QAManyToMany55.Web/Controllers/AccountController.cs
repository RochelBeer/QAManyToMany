using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using QAManyToMany.Data;
using System.Security.Claims;

namespace QAManyToMany55.Web.Controllers
{
    public class AccountController : Controller
    {
        private string _connectionString;
        public AccountController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(string email, string password, string returnurl)
        {
            UserRepo repo = new(_connectionString);
            User user = repo.LogIn(email,password);

            if(user == null)
            {
                TempData["Error"] = "Invalid Login";
                return Redirect("/account/login");
            }
            //this code logs in the current user!
            var claims = new List<Claim>
            {
                new Claim("user", email)
            };

            HttpContext.SignInAsync(new ClaimsPrincipal(
                new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();

            return Redirect(returnurl);
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(User user, string password)
        {
            UserRepo repo = new(_connectionString);
            repo.AddUser(user, password);
            return RedirectToAction("login");
        }
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync().Wait();
            return Redirect("/");
        }
    }
}

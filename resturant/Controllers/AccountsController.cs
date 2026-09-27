using Microsoft.AspNetCore.Mvc;
using resturant.Data;
using resturant.Migrations;
using resturant.Repositories;

namespace resturant.Controllers
{
    public class AccountsController : Controller
    {
       

        private readonly IUserRepository _userRepository;

        public AccountsController( IUserRepository userRepository)
        {
            
            _userRepository= userRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.HashPassword))
            {
                // Authentication successful
                // You can set up session or authentication cookie here
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Authentication failed
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }
        }

    }
}

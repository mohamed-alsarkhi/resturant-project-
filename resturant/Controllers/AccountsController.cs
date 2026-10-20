using Microsoft.AspNetCore.Mvc;
using resturant.Application.Services.Base;


namespace resturant.Controllers
{
    public class AccountsController : Controller
    {
       

        private readonly IUserServices _userServices;

        public AccountsController(IUserServices userServices)
        {

            _userServices = userServices;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
           
            if (_userServices.login(username, password))
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

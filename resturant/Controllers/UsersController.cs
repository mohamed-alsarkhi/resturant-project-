using Microsoft.AspNetCore.Mvc;
using resturant.Application.Dtos;
using resturant.Application.Services.Base;
using resturant.Domain.Models;

namespace resturant.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserServices _userServices;

        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public IActionResult Index()
        {
            var users = _userServices.GetAllUsers();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _userServices.CreateUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public IActionResult Edit(int id)
        {
            var userDto = _userServices.GetForEdit(id);
            if (userDto == null)
            {
                return NotFound();
            }

            return View(userDto);
        }

        [HttpPost]
        public IActionResult Edit(UpdateUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                _userServices.UpdateUser(userDto);
                return RedirectToAction("Index");
            }
            return View(userDto);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _userServices.DeleteUser(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ManageRoles(int id)
        {
            var model = _userServices.GetUserRoles(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult ManageRoles(UserRolesVM model)
        {
            _userServices.UpdateUserRoles(model);
            return RedirectToAction("Index");
        }

        public IActionResult ManageFiles(int UserId)
        {
            var userName = _userServices.GetUserName(UserId);
            if (userName == null)
            {
                return NotFound();
            }

            ViewBag.Files = _userServices.GetUserFiles(UserId);
            ViewBag.userName = userName;

            var userFile = new UserFile
            {
                UserId = UserId
            };

            return View(userFile);
        }

        [HttpPost]
        public IActionResult ManageFiles(UserFile userFile, IFormFile file)
        {
            if (file == null)
            {
                ViewBag.Files = _userServices.GetUserFiles(userFile.UserId);
                ViewBag.userName = _userServices.GetUserName(userFile.UserId);
                return View(userFile);
            }

            if (!ModelState.IsValid)
            {
                return View(userFile);
            }

            _userServices.AddUserFile(userFile, file);
            return RedirectToAction("ManageFiles", new { UserId = userFile.UserId });
        }

        [HttpPost]
        public IActionResult DeleteFile(int id)
        {
            var userId = _userServices.DeleteUserFile(id);
            if (userId == null)
            {
                return NotFound();
            }

            return RedirectToAction("ManageFiles", new { UserId = userId });
        }
    }
}
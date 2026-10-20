using Microsoft.AspNetCore.Mvc;
using resturant.Application.Dtos;
using resturant.Application.Services.Base;

namespace resturant.Controllers
{
    public class PermissionsController : Controller
    {
        private readonly IPermissionServices _permissionServices;

        public PermissionsController(IPermissionServices permissionServices)
        {
            _permissionServices = permissionServices;
        }

        public IActionResult Index()
        {
            var permissions = _permissionServices.GetAllPermissions();
            return View(permissions);
        }

        [HttpPost]
        public IActionResult Create(CreatePermissionDto permissionDto)
        {
            if (ModelState.IsValid)
            {
                _permissionServices.CreatePermission(permissionDto);
                return RedirectToAction("Index");
            }
            return View(permissionDto);
        }

        [HttpPost]
        public IActionResult Edit(UpdatePermissionDto permissionDto)
        {
            if (ModelState.IsValid)
            {
                _permissionServices.UpdatePermission(permissionDto);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _permissionServices.DeletePermission(id);
            return RedirectToAction("Index");
        }
    }
}
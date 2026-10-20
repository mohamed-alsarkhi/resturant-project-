using Microsoft.AspNetCore.Mvc;
using resturant.Application.Dtos;
using resturant.Application.Services.Base;

namespace resturant.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleServices _roleServices;

        public RoleController(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }

        public IActionResult Index()
        {
            var roles = _roleServices.GetAllRoles();
            return View(roles);
        }

        [HttpPost]
        public IActionResult Create(CreateRoleDto rolesdto)
        {
            if (ModelState.IsValid)
            {
                _roleServices.CreateRole(rolesdto);
                return RedirectToAction("Index");
            }
            return View(rolesdto);
        }

        [HttpPost]
        public IActionResult Edit(UpdateRoleDto rolesdto)
        {
            if (ModelState.IsValid)
            {
                _roleServices.UpdateRole(rolesdto);
                return RedirectToAction("Index");
            }
            return View(rolesdto);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _roleServices.DeleteRole(id);
            return RedirectToAction("Index");
        }

        public IActionResult AssignPermissions(int roleId)
        {
            var role = _roleServices.GetRoleWithPermissions(roleId);

            if (role == null)
            {
                return NotFound();
            }

            ViewBag.AllPermissions = _roleServices.GetAllPermissions();

            ViewBag.AssignedPermissions = role.Permissions
                .Select(p => p.Id)
                .ToList();

            return View(role);
        }

        [HttpPost]
        public IActionResult AssignPermissions(int roleId, List<int> permissionIds)
        {
            var role = _roleServices.GetRoleWithPermissions(roleId);

            if (role == null)
            {
                return NotFound();
            }

            _roleServices.AssignPermissions(roleId, permissionIds);

            return RedirectToAction("Index");
        }
    }
}
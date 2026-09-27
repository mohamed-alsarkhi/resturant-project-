using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using resturant.Models;
using resturant.Repositories;

namespace resturant.Controllers
{
    public class RoleController : Controller
    {
        
        private readonly IRoleRepository _roleRepository;

        private readonly IPermissionRepository _permissionRepository;

        public RoleController( IRoleRepository roleRepository , IPermissionRepository permissionRepository)
        {
            _roleRepository = roleRepository;

            _permissionRepository = permissionRepository;
            
        }

        public IActionResult Index()
        {
            IEnumerable<Role> roles = _roleRepository.GetAll();
            return View(roles);
        }

     

        

        [HttpPost]
        public IActionResult Create(Role roles)
        {
            if (ModelState.IsValid)
            {
                _roleRepository.Add(roles);
                _roleRepository.Save();
                return RedirectToAction("Index");

            }
            return View();
        }


        
        [HttpPost]
        public IActionResult Edit(Role roles)
        {
            if (ModelState.IsValid)
            {
                _roleRepository.Update(roles);
                _roleRepository.Save();
                return RedirectToAction("Index");

            }
            return View();
        }

        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var role = _roleRepository.GetById(id);
            if (role == null)
            {
                return NotFound();
            }

            _roleRepository.Delete(role);
            _roleRepository.Save();
            return RedirectToAction("Index");
           
        }

        public IActionResult AssignPermissions(int roleId)
        {
            var role = _roleRepository.GetRoleWithPermissions(roleId);

            if (role == null)
            {
                return NotFound();
            }

            var permissions = _permissionRepository.GetAll();

            ViewBag.AllPermissions = permissions;

            ViewBag.AssignedPermissions = role.Permissions
                .Select(p => p.Id)
                .ToList();

            return View(role);
        }


        [HttpPost]
        public IActionResult AssignPermissions(int roleId, List<int> permissionIds)
        {
            var role = _roleRepository.GetRoleWithPermissions(roleId);

            if (role == null)
            {
                return NotFound();
            }


            // Remove old permissions
            role.Permissions.Clear();


            // Get selected permissions
            var permissions = _permissionRepository.GetSelectedPermissions(permissionIds);


            // Add selected permissions
            foreach (var permission in permissions)
            {
                role.Permissions.Add(permission);
            }


            _roleRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

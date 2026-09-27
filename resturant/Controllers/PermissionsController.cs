using Microsoft.AspNetCore.Mvc;
using resturant.Data;
using resturant.Models;
using resturant.Repositories;

namespace resturant.Controllers
{
    public class PermissionsController : Controller
    {
       
        private readonly IPermissionRepository _permissionRepository;

        public PermissionsController( IPermissionRepository permissionRepository)
        {
            
            _permissionRepository = permissionRepository;

        }

        public IActionResult Index()
        {
            IEnumerable<Permission> Permission = _permissionRepository.GetAll();
            return View(Permission);
        }





        [HttpPost]
        public IActionResult Create(Permission Permission)
        {
            if (ModelState.IsValid)
            {
                _permissionRepository.Add(Permission);
                _permissionRepository.Save();
                return RedirectToAction("Index");

            }
            return View();
        }



        [HttpPost]
        public IActionResult Edit(Permission Permission)
        {
            if (ModelState.IsValid)
            {
                _permissionRepository.Update(Permission);
                _permissionRepository.Save();
                return RedirectToAction("Index");

            }
            return View();
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var per = _permissionRepository.GetById(id);
            if (per == null)
            {
                return NotFound();
            }

            _permissionRepository.Delete(per);
            _permissionRepository.Save();
            return RedirectToAction("Index");

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using resturant.Application.Dtos;
using resturant.Application.Services.Base;

namespace resturant.Controllers
{
    public class CategoryController : Controller
    {

       
        private readonly ICategoryServices _categoryServices;
        public CategoryController(ICategoryServices categoryServices)
        {
            
            _categoryServices = categoryServices;

        }

        public void SelectListForCategories() {

            var categories = _categoryServices.GetAllCategories();
            SelectList categorySelectList = new SelectList(categories, "Id", "Name");
            ViewBag.Categories = categorySelectList;
        }

        public IActionResult Index()
        {
            var categories = _categoryServices.GetAllCategories();
            return View(categories);
        }

        public IActionResult Create() {

            SelectListForCategories();

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryDto categoriesDto)
        {
            if (ModelState.IsValid)
            {
                _categoryServices.CreateCategory(categoriesDto);
                return RedirectToAction("Index");

            }
            return View(categoriesDto);
        }
        /// ------------------------------------------------------------------------
        public IActionResult Edit(int id)
        {
            var dto = _categoryServices.GetForEdit(id);

            if (dto == null) {
                return NotFound();
            }
            SelectListForCategories();
            
            return View(dto);
        }

        [HttpPost]
        public IActionResult Edit (UpdateCategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                _categoryServices.UpdateCategory(dto);
                return RedirectToAction("Index");

            }
            return View(dto);
        }
        /// ------------------------------------------------------------------------
        public IActionResult Delet(int id )
        {
            var cat = _categoryServices.GetForDelete(id);

            if (cat == null) {
                return NotFound();
            }

            return View(cat);
        }

        [HttpPost]
        public IActionResult DeletConfirmed(int id)
        {
            _categoryServices.DeleteCategory(id);
                return RedirectToAction("Index");

        }
        /// ------------------------------------------------------------------------
    }
}

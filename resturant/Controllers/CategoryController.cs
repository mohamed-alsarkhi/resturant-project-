using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using resturant.Data;
using resturant.Dtos;
using resturant.Models;
using resturant.Repositories;

namespace resturant.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

        }

        public void SelectListForCategories() {

            IEnumerable<Category> categories = _categoryRepository.GetAll();
            SelectList categorySelectList = new SelectList(categories, "Id", "Name");
            ViewBag.Categories = categorySelectList;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _categoryRepository.GetAll(); 
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
                var categories = new Category
                {
                    Name = categoriesDto.Name

                };

                _categoryRepository.Add(categories);
                _categoryRepository.Save();
                return RedirectToAction("Index");

            }
            return View(categoriesDto);
        }
        /// ------------------------------------------------------------------------
        public IActionResult Edit(int id)
        {
            var cat = _categoryRepository.GetById(id);

            if (cat ==null) {
                return NotFound();
            }
            SelectListForCategories();
            return View(cat);
        }

        [HttpPost]
        public IActionResult Edit (Category categories)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Update(categories);
                _categoryRepository.Save();
                return RedirectToAction("Index");

            }
            return View();
        }
        /// ------------------------------------------------------------------------
        public IActionResult Delet(int id )
        {
            var cat = _categoryRepository.GetById(id);

            if (cat == null) {
                return NotFound();
            }

            return View(cat);
        }

        [HttpPost]
        public IActionResult Delet(Category categories)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Delete(categories);
                _categoryRepository.Save();
                return RedirectToAction("Index");

            }
            return View();
        }
        /// ------------------------------------------------------------------------
    }
}

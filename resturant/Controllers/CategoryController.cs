using Microsoft.AspNetCore.Mvc;
using resturant.Data;
using resturant.Models;

namespace resturant.Controllers
{
    public class CategoryController : Controller
    {

        private readonly AppDbContext _db;
        public CategoryController(AppDbContext db)
        {
            _db = db;

        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _db.Categories.ToList();
            return View(categories);
        }

        public IActionResult Create() {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Category categories)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(categories);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }
        /// ------------------------------------------------------------------------
        public IActionResult Edit(int id)
        {
            var cat = _db.Categories.Find(id);

            if (cat ==null) {
                return NotFound();
            }

            return View(cat);
        }

        [HttpPost]
        public IActionResult Edit (Category categories)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(categories);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }
        /// ------------------------------------------------------------------------
        public IActionResult Delet(int id )
        {
            var cat = _db.Categories.Find(id);

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
                _db.Categories.Remove(categories);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }
        /// ------------------------------------------------------------------------
    }
}

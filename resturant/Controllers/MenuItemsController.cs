using Microsoft.AspNetCore.Mvc;
using resturant.Data;
using resturant.Models;

namespace resturant.Controllers
{
    public class MenuItemsController : Controller
    {

        private readonly AppDbContext _db;
        public MenuItemsController(AppDbContext db)
        {
            _db = db;

        }

        public IActionResult Index()
        {

            IEnumerable<MenuItem> menu = _db.Menu.ToList();
            return View(menu);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MenuItem item)
        {
            if (ModelState.IsValid) {
                _db.Menu.Add(item);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
                return View();
        }


        public IActionResult Edit(int Id)
        {
            var item = _db.Menu.Find(Id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(MenuItem item)
        {
            if (ModelState.IsValid)
            {
                _db.Menu.Update(item);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }

        public IActionResult Delet(int Id)
        {
            var item = _db.Menu.Find(Id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [HttpPost]
        public IActionResult Delet(MenuItem item)
        {
            if (ModelState.IsValid)
            {
                _db.Menu.Remove(item);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }

        //public List<MenuItem> menu()
        //{

        //    List<MenuItem> menu = new List<MenuItem>();
        //    menu.Add(new MenuItem { id = 1, name = "salamon", price = 50 });
        //    return menu;
        //}




    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using resturant.Application.Dtos;
using resturant.Application.Services.Base;

namespace resturant.Controllers
{
    public class MenuItemsController : Controller
    {

       
        
        private readonly IMenuItemsServices _menuItemServices;



        public MenuItemsController(IMenuItemsServices menuItemServices)
        {
            
            _menuItemServices = menuItemServices;
        }

        public IActionResult Index()
        {

            var menu = _menuItemServices.GetAllWithCategory();
            return View(menu);
        }

        public IActionResult Create()
        {
            SelectListForCategories();
            return View();
        }


        private void SelectListForCategories()
        {
            //IEnumerable<Category> categories = _db.Categories.ToList();
            var categories = _menuItemServices.GetAllCategories();
            SelectList categorySelectList = new SelectList(categories, "Id", "Name");
            ViewBag.Categories = categorySelectList;
        }

        [HttpPost]
        public IActionResult Create(CreateMenuItemDto itemDto)
        {
            if (ModelState.IsValid) {

                _menuItemServices.CreateMenuItemDto(itemDto);
                return RedirectToAction("Index");
                    
            }
                return View(itemDto);
        }


        public IActionResult Edit(int Id)
        {
            var itemDto = _menuItemServices.GetForEdit(Id);
            if (itemDto == null)
            {
                return NotFound();
            }
         
            SelectListForCategories();
            return View(itemDto);
        }
        [HttpPost]
        public IActionResult Edit(UpdateMenuItemDto itemDto)
        {
            if (ModelState.IsValid)
            {
                _menuItemServices.UpdateMenuItem(itemDto);
                return RedirectToAction("Index");

            }
            return View();
        }

        public IActionResult Delet(int Id)
        {
            var item = _menuItemServices.GetForDelete(Id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [HttpPost]
        public IActionResult DeletConfirmed(int id)
        {
            _menuItemServices.DeleteMenuItem(id);
            return RedirectToAction("Index");
        }

        //public List<MenuItem> menu()
        //{

        //    List<MenuItem> menu = new List<MenuItem>();
        //    menu.Add(new MenuItem { id = 1, name = "salamon", price = 50 });
        //    return menu;
        //}




    }
}

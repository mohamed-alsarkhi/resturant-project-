using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using resturant.Data;
using resturant.Dtos;
using resturant.Models;
using resturant.Repositories;

namespace resturant.Controllers
{
    public class MenuItemsController : Controller
    {

        private readonly IMenuItemRepository _menuItemRepository;
        private readonly ICategoryRepository _categoryRepository; 

        
        public MenuItemsController(IMenuItemRepository menuItemRepository, ICategoryRepository categoryRepository)
        {
            _menuItemRepository = menuItemRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {

            var menu = _menuItemRepository.GetAllWithCategory();
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
            var categories = _categoryRepository.GetAll();
            SelectList categorySelectList = new SelectList(categories, "Id", "Name");
            ViewBag.Categories = categorySelectList;
        }

        [HttpPost]
        public IActionResult Create(CreateMenuItemDto itemDto)
        {
            if (ModelState.IsValid) {

                var item = new MenuItem() ;

                
                item.name = itemDto.name;
                item.price = itemDto.price;
                item.description = itemDto.description;
                item.enabled = itemDto.enabled;
                item.categoryid = itemDto.categoryid;

                _menuItemRepository.Add(item);
                _menuItemRepository.Save();
                return RedirectToAction("Index");
                    
            }
                return View(itemDto);
        }


        public IActionResult Edit(int Id)
        {
            var item = _menuItemRepository.GetById(Id);
            if (item == null)
            {
                return NotFound();
            }
            var itemDto = new UpdateMenuItemDto
            {

                id = item.id,
                name = item.name,
                price = item.price,
                description = item.description,
                enabled = item.enabled,
                categoryid = item.categoryid
            };

            SelectListForCategories();
            return View(itemDto);
        }
        [HttpPost]
        public IActionResult Edit(UpdateMenuItemDto itemDto)
        {
            if (ModelState.IsValid)
            {
                var item = new MenuItem
                {
                    id = itemDto.id,
                    name = itemDto.name,
                    price = itemDto.price,
                    description = itemDto.description,
                    enabled = itemDto.enabled,
                    categoryid = itemDto.categoryid

                };

                _menuItemRepository.Update(item);
                _menuItemRepository.Save();
                return RedirectToAction("Index");

            }
            return View();
        }

        public IActionResult Delet(int Id)
        {
            var item = _menuItemRepository.GetById(Id);
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
                _menuItemRepository.Delete(item);
                _menuItemRepository.Save();
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

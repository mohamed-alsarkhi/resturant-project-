using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using resturant.Application.Dtos;
using resturant.Application.Services.Base;

namespace resturantApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class menuController : ControllerBase
    {

        private readonly IMenuItemsServices _menuItemsServices;

        public menuController(IMenuItemsServices menuItemsServices)
        {
            _menuItemsServices = menuItemsServices;
        }


        [HttpGet]
        public IActionResult Getmenuitem()
        {
            var menuitem = _menuItemsServices.GetAllWithCategory();
            return Ok(menuitem);
        }


        [HttpGet("categorys")]
        public IActionResult GetCategorys ()
        {
            var categorys = _menuItemsServices.GetAllCategories();
            return Ok(categorys);
        }
    }
}

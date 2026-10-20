using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using resturant.Application.Dtos;
using resturant.Application.Services;
using resturant.Application.Services.Base;

namespace resturantApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartServices _cartServices;

        public CartController(ICartServices cartServices)
        {
            _cartServices = cartServices;
        }

        [HttpGet("getcartbyuser")]
        public IActionResult Getcartbyuser(int userid)
        {
            var cartitems = _cartServices.GetCartByUserId(userid);
            
            
            return Ok(cartitems);
        }

        [HttpPost("addinCart")]
        public IActionResult AddinCart(AddToCartDto itemdto)
        {
            var item = _cartServices.AddToCart(itemdto);

            if (!item) {
                return NotFound("Product not found");
            }

            return Ok(new { message = "Added to cart" });
        }


        [HttpDelete("removeFromCart")]
        public IActionResult RemoveFromCart(int userId, int menuItemId)
        {
            var success = _cartServices.RemoveFromCart(userId, menuItemId);
            if (!success)
            {
                return NotFound(new { message = "the prodect not in the caet" });
            }

            return Ok(new { message = "remove product from cart sucsesful" });
        }

        [HttpDelete("clearCart")]
        public IActionResult ClearCart(int userId)
        {
             _cartServices.ClearCart(userId);
            
            return Ok(new { message = "remove cart sucsesful" });
        }


    }
}

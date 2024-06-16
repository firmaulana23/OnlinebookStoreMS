using Microsoft.AspNetCore.Mvc;
using OnlineBookstoreMS.Models.Entity;
using OnlineBookstoreMS.Interface;
using OnlineBookstoreMS.RequestSchema;
using Microsoft.AspNetCore.Authorization;

namespace OnlineBookstoreMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // [HttpGet]
        // public async Task<IActionResult> GetCart()
        // {
        //     var userId = int.Parse(User.Identity.Name);
        //     var cart = await _cartService.GetCartByUserId(userId);
        //     return Ok(cart);
        // }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] CartRequest req)
        {
            var userId = int.Parse(User.Identity.Name);
            await _cartService.AddToCart(userId, req.BookId, req.Quantity);
            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCartItem([FromBody] UpdateCartRequest req)
        {
            var userId = int.Parse(User.Identity.Name);
            await _cartService.UpdateCartItem(userId, req.CartItemId, req.Quantity);
            return Ok();
        }

        [HttpDelete("remove/{cartItemId}")]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            var userId = int.Parse(User.Identity.Name);
            await _cartService.RemoveFromCart(userId, cartItemId);
            return Ok();
        }

        [HttpGet("total")]
        public IActionResult GetCartTotal()
        {
            var userId = int.Parse(User.Identity.Name);
            var total = _cartService.GetCartTotalPrice(userId);
            return Ok(new { TotalPrice = total });
        }
    }


}
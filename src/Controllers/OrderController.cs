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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var userId = int.Parse(User.Identity.Name);
            var order = await _orderService.PlaceOrder(userId);
            return Ok(order);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await _orderService.GetOrderById(orderId);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetOrdersByUser()
        {
            var userId = int.Parse(User.Identity.Name);
            var orders = await _orderService.GetOrdersByUserId(userId);
            return Ok(orders);
        }
    }

}

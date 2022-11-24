using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider OrdersProvider;

        public OrdersController(IOrdersProvider OrdersProvider)
        {
            this.OrdersProvider = OrdersProvider;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrdersAsync(int id)
        {
            var result = await OrdersProvider.GetOrdersAsync(id);
            if (result.isSuccess)
            {
                return Ok(result.Orders);
            }
            return NotFound();
        }
    }
}

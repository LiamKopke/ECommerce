using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    [Produces("application/json")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider OrdersProvider;

        public OrdersController(IOrdersProvider OrdersProvider)
        {
            this.OrdersProvider = OrdersProvider;
        }

        /// <summary>
        /// Get order by the provided id.
        /// </summary>
        /// <param name="id">The order id to get</param>
        /// <returns>An IActionResult</returns>
        /// <response code="200">Returns the requested order</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

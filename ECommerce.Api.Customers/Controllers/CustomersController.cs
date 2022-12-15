using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Controllers
{
    [ApiController]
    [Route("api/customers")]
    [Produces("application/json")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersProvider CustomersProvider;

        public CustomersController(ICustomersProvider CustomersProvider)
        {
            this.CustomersProvider = CustomersProvider;
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns>An IActionResult</returns>
        /// <response code="200">Returns all the customers</response>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await CustomersProvider.GetCustomersAsync();
            if (result.isSuccess)
            {
                return Ok(result.Customers);
            }
            return NotFound();
        }

        /// <summary>
        /// Get customer by the provided id.
        /// </summary>
        /// <param name="id">The customer id to get</param>
        /// <returns>An IActionResult</returns>
        /// <response code="200">Returns the requested customer</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomersAsync(int id)
        {
            var result = await CustomersProvider.GetCustomersAsync(id);
            if (result.isSuccess)
            {
                return Ok(result.Customer);
            }
            return NotFound();
        }
    }
}

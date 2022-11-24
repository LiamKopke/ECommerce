using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersProvider CustomersProvider;

        public CustomersController(ICustomersProvider CustomersProvider)
        {
            this.CustomersProvider = CustomersProvider;
        }

        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await CustomersProvider.GetCustomersAsync();
            if (result.isSuccess)
            {
                return Ok(result.Customers);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
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

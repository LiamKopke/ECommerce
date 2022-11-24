using AutoMapper;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext dbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;
        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        public async Task<(bool isSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync(int id)
        {
            try
            {
                var orders = await dbContext.Orders
                    .Where(o => o.CustomerId == id)
                    .Include(o => o.Items)
                    .ToListAsync();
                if (orders != null && orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }


        private void SeedData()
        {
            if (!dbContext.Orders.Any())
            {
                dbContext.Orders.Add(new Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = DateTime.Now,
                    Total = 100,
                    Items = new List<OrderItem>() { 
                        new OrderItem() { Id = 1, OrderId = 1, ProductId = 1, Quantity = 3, UnitPrice = 5 },
                        new OrderItem() { Id = 2, OrderId = 1, ProductId = 2, Quantity = 7, UnitPrice = 45 },
                        new OrderItem() { Id = 3, OrderId = 1, ProductId = 3, Quantity = 4, UnitPrice = 22 } 
                    }
                });

                dbContext.Orders.Add(new Order()
                {
                    Id = 2,
                    CustomerId = 2,
                    OrderDate = DateTime.MinValue,
                    Total = 50,
                    Items = { 
                        new OrderItem() { Id = 4, OrderId = 2, ProductId = 4, Quantity = 5, UnitPrice = 3 },
                        new OrderItem() { Id = 5, OrderId = 2, ProductId = 2, Quantity = 2, UnitPrice = 16 },
                        new OrderItem() { Id = 6, OrderId = 2, ProductId = 1, Quantity = 8, UnitPrice = 29 }
                    }
                });

                dbContext.Orders.Add(new Order()
                {
                    Id = 3,
                    CustomerId = 3,
                    OrderDate = DateTime.MaxValue,
                    Total = 25,
                    Items = { 
                        new OrderItem() { Id = 7, OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 90 },
                        new OrderItem() { Id = 8, OrderId = 3, ProductId = 2, Quantity = 6, UnitPrice = 100 },
                        new OrderItem() { Id = 9, OrderId = 3, ProductId = 4, Quantity = 9, UnitPrice = 110 } 
                    }
                });
                dbContext.SaveChanges();
            }
        }        
    }
}

using ECommerce.Api.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool isSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsAsync();
        Task<(bool isSuccess, Product Product, string ErrorMessage)> GetProductsAsync(int id);
    }
}

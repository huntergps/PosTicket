using System.Collections.Generic;
using System.Threading.Tasks;
using PosTicket.Repository.Interface;
using PosTicket.Repository.WebService;

namespace PosTicket.Models
{
    sealed class ReadProductResponse
    {
        public IProductRepository Repository { get; private set; }
        public async Task<ProductCategory> GetProductCategoryAsync()
        {
            Repository = new ProductRepository();
            return await Repository.GetProductCategoryAsync();
        }
        public async Task<Product> GetProductAsync()
        {
            Repository = new ProductRepository();
            return await Repository.GetProductAsync();
        }
    }
}

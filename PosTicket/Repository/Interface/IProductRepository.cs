using System.Collections.Generic;
using System.Threading.Tasks;

namespace PosTicket.Repository.Interface
{
    public interface IProductRepository
    {
        Task<ProductCategory> GetProductCategoryAsync();
        Task<Product> GetProductAsync();
        Task<ProductPrice> GetProductPrice(ProductPriceRequest productPriceRequest);
    }
}

using Stock.Entities.Entities;

namespace Stock.Services.Abstract
{
    public interface IProductService
    {
        Task<Product> InsertAsync(Product product);
    }
}

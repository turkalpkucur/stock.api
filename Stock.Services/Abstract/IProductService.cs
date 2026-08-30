using Microsoft.EntityFrameworkCore;
using Stock.Entities.Dtos.Product;
using Stock.Entities.Entities;

namespace Stock.Services.Abstract
{
    public interface IProductService
    {
        Task<Product> InsertAsync(Product product);

        Task<Product> UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task<List<ProductResponse>> ListAsync();
    }
}

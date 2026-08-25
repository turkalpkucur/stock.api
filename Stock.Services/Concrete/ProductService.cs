using Stock.Entities.Entities;
using Stock.Services.Abstract;
using Stock.Services.Data;

namespace Stock.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly StockDbContext _context;

        public ProductService(StockDbContext context)
        {
            _context = context;
        }

        public async Task<Product> InsertAsync(Product product)
        {
            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return product;
        }
    }
}

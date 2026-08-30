using Microsoft.EntityFrameworkCore;
using Stock.Entities.Dtos.Product;
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

        public async Task<Product> UpdateAsync(Product product)
        {
            _context.Products.Update(product);

            await _context.SaveChangesAsync();

            return product;
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                throw new InvalidOperationException("Product not found");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductResponse>> ListAsync()
        {
            var products = await _context.Products.ToListAsync();
            List<ProductResponse> productResponses = products.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                ProductGroupId = p.ProductGroupId,
                Description = p.Description
            }).ToList ();

            foreach (var productResponse in productResponses)
            {
                var productGroup = await _context.ProductGroups.FindAsync(productResponse.ProductGroupId);
                if (productGroup != null)
                {
                    productResponse.ProductGroupName = productGroup.Name;
                }
            }   
            return productResponses.OrderBy(g=>g.Name).ToList();
        }
    }
}

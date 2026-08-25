using Stock.Entities.Entities;
using Stock.Services.Abstract;
using Stock.Services.Data;

namespace Stock.Services.Concrete
{
    public class ProductGroupService : IProductGroupService
    {
        private readonly StockDbContext _context;

        public ProductGroupService(StockDbContext context)
        {
            _context = context;
        }

        public async Task<ProductGroup> InsertAsync(ProductGroup productGroup)
        {
            _context.ProductGroups.Add(productGroup);

            await _context.SaveChangesAsync();

            return productGroup;
        }
    }
}

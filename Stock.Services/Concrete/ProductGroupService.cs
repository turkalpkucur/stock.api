using Microsoft.EntityFrameworkCore;
using Stock.Entities.Dtos;
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
            await _context.ProductGroups.AddAsync(productGroup);

            await _context.SaveChangesAsync();

            return productGroup;
        }

        public async Task<List<ProductGroupResponse>> ListAsync()
        {
            List<ProductGroupResponse> productGroups = await _context.ProductGroups
                 .Select(pg => new ProductGroupResponse
                 {
                     Name = pg.Name,
                     Id=pg.Id
                 })
                 .ToListAsync();
            return productGroups;
        }
    }
}

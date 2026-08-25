using Stock.Entities.Entities;

namespace Stock.Services.Abstract
{
    public interface IProductGroupService
    {
        Task<ProductGroup> InsertAsync(ProductGroup productGroup);
    }
}

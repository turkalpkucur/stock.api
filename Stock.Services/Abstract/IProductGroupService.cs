using Stock.Entities.Dtos;
using Stock.Entities.Entities;

namespace Stock.Services.Abstract
{
    public interface IProductGroupService
    {
        Task<ProductGroup> InsertAsync(ProductGroup productGroup);
        Task<List<ProductGroupResponse>> ListAsync();
    }
}

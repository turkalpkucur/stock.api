using Microsoft.EntityFrameworkCore;
using Stock.Entities.Dtos;
using Stock.Entities.Entities;

namespace Stock.Services.Abstract
{
    public interface IProductGroupService
    {
        Task<ProductGroup> InsertAsync(ProductGroup productGroup);

        Task<ProductGroup> UpdateAsync(ProductGroup productGroup);
        Task DeleteAsync(int id);
        Task<List<ProductGroupResponse>> ListAsync();
    }
}

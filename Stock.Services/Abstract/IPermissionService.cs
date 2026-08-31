using Stock.Entities.Dtos.Permission;
using Stock.Entities.Entities;

namespace Stock.Services.Abstract
{
    public interface IPermissionService
    {
        Task<Permission> InsertAsync(Permission permission);
        Task<Permission> UpdateAsync(Permission permission);
        Task DeleteAsync(int id);
        Task<List<PermissionResponse>> ListAsync();
    }
}

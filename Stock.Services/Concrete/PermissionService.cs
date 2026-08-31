using Microsoft.EntityFrameworkCore;
using Stock.Entities.Dtos.Permission;
using Stock.Entities.Entities;
using Stock.Services.Abstract;
using Stock.Services.Data;

namespace Stock.Services.Concrete
{
    public class PermissionService : IPermissionService
    {
        private readonly StockDbContext _context;
        public PermissionService(StockDbContext context)
        {
            _context = context;
        }

        public async Task<List<PermissionResponse>> ListAsync()
        {
            var permissions = await _context.Permissions.ToListAsync();
            List<PermissionResponse> permissionResponses = permissions.Select(p => new PermissionResponse
            {
                Id = p.Id,
                PageLink = p.PageLink,
                UserProfileId = p.UserProfileId,
                UserProfileName = _context.UserProfiles.FirstOrDefault(up => up.Id == p.UserProfileId)?.Name
            }).ToList();

            return permissionResponses.OrderBy(g => g.UserProfileName).ToList();
        }

        public async Task<Permission> InsertAsync(Permission permission)
        {
            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();
            return permission;
        }
        public async Task<Permission> UpdateAsync(Permission permission)
        {
            _context.Permissions.Update(permission);
            await _context.SaveChangesAsync();
            return permission;
        }
        public async Task DeleteAsync(int id)
        {
            var permission = await _context.Permissions.FindAsync(id);
            if (permission == null)
                throw new InvalidOperationException("Permission not found");

            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();
        }

    }
}


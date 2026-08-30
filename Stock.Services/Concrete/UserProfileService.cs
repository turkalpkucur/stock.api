using Microsoft.EntityFrameworkCore;
using Stock.Entities.Dtos.UserProfile;
using Stock.Entities.Entities;
using Stock.Services.Abstract;
using Stock.Services.Data;

namespace Stock.Services.Concrete
{
    public class UserProfileService : IUserProfileService
    {
        private readonly StockDbContext _context;
        public UserProfileService(StockDbContext context)
        {
            _context = context;
        }
        public async Task<List<UserProfileResponse>> ListAsync()
        {
            List<UserProfileResponse> userProfiles = await _context.UserProfiles
                 .Select(up => new UserProfileResponse
                 {
                     Name = up.Name,
                     Id = up.Id
                 })
                 .OrderBy(g => g.Name).ToListAsync();
            return userProfiles;
        }

        public async Task<UserProfile> InsertAsync(UserProfile userProfile)
        {
            await _context.UserProfiles.AddAsync(userProfile);

            await _context.SaveChangesAsync();

            return userProfile;
        }

        public async Task<UserProfile> UpdateAsync(UserProfile userProfile)
        {
            UserProfile existingUserProfile = await _context.UserProfiles.Where(u => u.Id == userProfile.Id).FirstOrDefaultAsync();
            existingUserProfile.Name = userProfile.Name;
            _context.UserProfiles.Update(existingUserProfile);
            await _context.SaveChangesAsync();
            return existingUserProfile;
        }

        public async Task DeleteAsync(int id)
        {
            UserProfile existingUserProfile = await _context.UserProfiles.Where(u => u.Id == id).FirstOrDefaultAsync();
            _context.UserProfiles.Remove(existingUserProfile);
            await _context.SaveChangesAsync();
        }


    }
}

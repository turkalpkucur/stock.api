using Stock.Entities.Dtos.UserProfile;
using Stock.Entities.Entities;

namespace Stock.Services.Abstract
{
    public interface IUserProfileService
    {
        Task<UserProfile> InsertAsync(UserProfile userProfile);
        Task<UserProfile> UpdateAsync(UserProfile userProfile);
        Task DeleteAsync(int id);
        Task<List<UserProfileResponse>> ListAsync();
    }
}

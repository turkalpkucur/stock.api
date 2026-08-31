using Stock.Entities.Dtos.User;
using Stock.Entities.Entities;

namespace Stock.Services.Abstract
{
    public interface IUserService
    {
        Task<User> InsertAsync(User user);

        Task<User> UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task<List<UserResponse>> ListAsync();
    }
}

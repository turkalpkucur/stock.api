using Microsoft.EntityFrameworkCore;
using Stock.Entities.Dtos.User;
using Stock.Entities.Entities;

namespace Stock.Services.Abstract
{
    public interface IUserService
    {
        Task<User> GetByIdAsync(int id);
        Task<User> InsertAsync(User user);
        Task<User> UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task<List<UserResponse>> ListAsync();
        Task<LoginUserDto> GetActiveUserByMailLoginUserDto(string email);
        Task AuthenticateUser(string password, User user);
    }
}

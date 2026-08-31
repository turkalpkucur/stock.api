using Microsoft.EntityFrameworkCore;
using Stock.Entities.Dtos.User;
using Stock.Entities.Entities;
using Stock.Services.Abstract;
using Stock.Services.Data;

namespace Stock.Services.Concrete
{
    public class UserService :IUserService
    {
        private readonly StockDbContext _context;
        public UserService(StockDbContext context)
        {
            _context = context;
        }
        public async Task<User> InsertAsync(User user)
        {
             await _context.Users.AddAsync(user);
             await _context.SaveChangesAsync();
             return user;
        }
        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new InvalidOperationException("User not found");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        public async Task<List<UserResponse>> ListAsync()
        {
            var users = await _context.Users.ToListAsync();
            List<UserResponse> userResponses = users.Select(u => new UserResponse
            {
                Id = u.Id,
                UserProfileId = u.UserProfileId,
                Email = u.Email
            }).ToList();

            foreach (var userResponse in userResponses)
            {
                var user=  await _context.Users.FirstOrDefaultAsync(u => u.Id == userResponse.Id);
                if (user != null)
                {
                    userResponse.UserProfileId = user.UserProfileId;
                }
            }
            return userResponses.OrderBy(u => u.Email).ToList();
        }
    }
}
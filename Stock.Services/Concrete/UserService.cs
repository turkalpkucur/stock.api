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
        public async Task AuthenticateUser(string password, User user)
        {
            bool result = await VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);
            if (!result)
                throw new Exception("Wrong password!");
        }
        private async Task<bool> VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
                if (computedHash[i] != passwordHash[i]) return false;

            return true;
        }



        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public async Task<LoginUserDto> GetActiveUserByMailLoginUserDto(string email)
        {
           
                LoginUserDto user = _context.Users
                .Where(x => x.Email == email ).Select(t => new LoginUserDto()
                {
                    Email = t.Email,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Id = t.Id,
                    PasswordSalt = t.PasswordSalt,
                    PasswordHash = t.PasswordHash,
             
                }).FirstOrDefault();

                user.UserProfileId = _context.UserProfiles.Where(g => g.Id == user.UserProfileId).FirstOrDefault().Id;

                //if (!(user.OperationClaimId == 1 || user.OperationClaimId == 6 || user.OperationClaimId == 7))
                //    throw new Exception("User cannot be found.");

                if (user == null)
                    throw new Exception("User cannot be found.");
                return user;
        }
    }
}
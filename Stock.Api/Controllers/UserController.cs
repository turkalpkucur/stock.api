using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Core.Security.Encryption;
using Stock.Entities.Dtos.User;
using Stock.Entities.Entities;
using Stock.Services.Abstract;

namespace Stock.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        //[HttpPost]
        //public async Task<IActionResult> InsertAsync(UserInsertRequestDto user)
        //{
        //    User newUser = _mapper.Map<User>(user);
        //    User result = await _userService.InsertAsync(newUser);
        //    return Ok(result);
        //}

        //[HttpPut]
        //public async Task<IActionResult> UpdateAsync(UserUpdateRequestDto user)
        //{
        //    User newUser = _mapper.Map<User>(user);
        //    User result = await _userService.UpdateAsync(newUser);
        //    return Ok(result);
        //}

        //[HttpDelete]
        //public async Task<IActionResult> DeleteAsync(int id)
        //{
        //    await _userService.DeleteAsync(id);
        //    return Ok();
        //}

        [HttpGet("List")]
        public async Task<IActionResult> ListAsync()
        {
            List<UserResponse> users = await _userService.ListAsync();
            return Ok(users);
        }


        [HttpPost("Save")]
        public async Task<IActionResult> SaveUser(
    UserInsertRequestDto M
    )
        {
            try
            {
                User user = await _userService.GetActiveUserByMailLoginUserDto(M.Email);
                if (user == null)
                {
                    byte[] PasswordHash;
                    byte[] PasswordSalt;

                    HashingHelper.CreatePasswordHash(M.Password, out PasswordHash, out PasswordSalt);
                    user = _mapper.Map<User>(M);
                    user.PasswordHash = PasswordHash;
                    user.PasswordSalt = PasswordSalt;
                    await _userService.InsertAsync(user);
                }
                else
                {
                    user.FirstName = M.FirstName;
                    user.LastName = M.LastName;
                    await _userService.UpdateAsync(user);
                }
                return Ok(new { success = true, message = "Success.." });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }
    }
}

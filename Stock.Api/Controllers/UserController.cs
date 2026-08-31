using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Entities.Dtos.Product;
using Stock.Entities.Dtos.User;
using Stock.Entities.Entities;
using Stock.Services.Abstract;
using Stock.Services.Concrete;

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

        [HttpPost]
        public async Task<IActionResult> InsertAsync(UserInsertRequestDto user)
        {
            User newUser = _mapper.Map<User>(user);
            User result = await _userService.InsertAsync(newUser);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UserUpdateRequestDto user)
        {
            User newUser = _mapper.Map<User>(user);
            User result = await _userService.UpdateAsync(newUser);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _userService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            List<UserResponse> users = await _userService.ListAsync();
            return Ok(users);
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Entities.Dtos.UserProfile;
using Stock.Entities.Entities;
using Stock.Services.Abstract;

namespace Stock.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfileController : BaseController
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IMapper _mapper;
        public UserProfileController(IUserProfileService userProfileService, IMapper mapper)
        {
            _userProfileService = userProfileService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            var result = await _userProfileService.ListAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync(UserProfileInsertRequestDto userProfile        )
        {
            UserProfile newUserProfile = _mapper.Map<UserProfile>(userProfile);

            var result =
                await _userProfileService.InsertAsync(newUserProfile);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UserProfileUpdateRequestDto userProfile)
        {
            UserProfile updatedUserProfile = _mapper.Map<UserProfile>(userProfile);

            var result = await _userProfileService.UpdateAsync(updatedUserProfile);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _userProfileService.DeleteAsync(id);

            return Ok();
        }
    }
}

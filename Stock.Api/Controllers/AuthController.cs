using Microsoft.AspNetCore.Mvc;
using Stock.Entities.Dtos.CustomDtos;
using Stock.Entities.Dtos.Permission;
using Stock.Entities.Dtos.User;
using Stock.Entities.Entities;
using Stock.Services.Abstract;
using System.Diagnostics;
using Stock.Core;


namespace Stock.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;
        public AuthController(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }


        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("user");
            return Ok(true);
        }

        //[HttpGet]
        //public async Task<IActionResult> SignOut()
        //{
        //    ControllerContext.HttpContext.Session.Clear();
        //    return Ok(new { success = true, message = "Signed out successfully." });
        //}

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserForLoginDto req)
        {
            try
            {
                if (string.IsNullOrEmpty(req.Email))
                {
                    return Ok(new { success = false, message = "Email giriş yapınız.." });
                }

                if (string.IsNullOrEmpty(req.Password))
                {
                    return Ok(new { success = false, message = "Şifre giriş yapınız.." });
                }

                LoginUserDto user = await _userService.GetActiveUserByMailLoginUserDto(req.Email); //1

                await _userService.AuthenticateUser(req.Password,
                     new User()
                     {
                         PasswordHash = user.PasswordHash,
                         PasswordSalt = user.PasswordSalt
                     });  
                List<PermissionDto> Permissions = await _permissionService.ReturnPermissionsByProfileId(
                    user.UserProfileId);
                user.Permissions = Permissions;
                HttpContext.Session.SetObject("user", user);
                return Ok(new { success = true, message = "Success.." });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = "There is no user." });
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("Error")]
        public async Task<IActionResult> Error()
        {
            return Ok(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}

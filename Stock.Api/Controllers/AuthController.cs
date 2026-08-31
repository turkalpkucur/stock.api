using Microsoft.AspNetCore.Mvc;
using Stock.Entities.Dtos.CustomDtos;
using Stock.Entities.Dtos.Permission;
using Stock.Entities.Dtos.User;
using Stock.Entities.Entities;
using System.Diagnostics;

namespace Stock.Api.Controllers
{
    public class AuthController : Controller
    {


        [HttpGet]
        public JsonResult Logout()
        {
            HttpContext.Session.Remove("areaadminuser");
            return Json(true);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserForLoginDto req)
        {
            try
            {
                if (string.IsNullOrEmpty(req.Email))
                {
                    return Json(new { success = false, message = "Email giriş yapınız.." });
                }

                if (string.IsNullOrEmpty(req.Password))
                {
                    return Json(new { success = false, message = "Şifre giriş yapınız.." });
                }

                LoginUserDto user = _userService.GetActiveUserByMailLoginUserDto(req.Email); //1

                _userService.AuthenticateUser(req.Password,
                   new User()
                   {
                       PasswordHash = user.PasswordHash,
                       PasswordSalt = user.PasswordSalt
                   }); //2

                // hem 1 hem 2 de login kontrolü yapılıyor....
                // 
                UserOperationClaim Usp = _userOperationClaimsService.ReturnByUserId(user.UserId);
                List<PermissionDto> Permissions = _permissionService.ReturnPermissionsByOperationClaimId(
                    user.OperationClaimId, user.FirmId);
                user.Permissions = Permissions;
                HttpContext.Session.SetObject("areaadminuser", user);
                UserHub.CurrentUserIdForUserHub = user.UserId;
                return Json(new { success = true, message = "Success.." });
            }
            catch
            {
                return Json(new { success = false, message = "There is no user." });
                //  ViewBag.Error = ex.Message;
                //  return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult SignOut()
        {
            ControllerContext.HttpContext.Session.Clear();

            return RedirectToAction("index", "account");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}

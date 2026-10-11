using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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


        [HttpPost]
        public JsonResult SaveUser(
    Entities.Concrete.User M,
    int OperationClaimId,
    string StatusStr,
    string Password,
    string ActivePassiveStr,
    string IsDeletedStr,
    string IsDemoStr,
    string DemoFirmName,
    string DemoDueDateStr,
    string InfotecFirm
    )
        {
            try
            {
                M.IsActive = ActivePassiveStr == "true";
                M.IsDeleted = IsDeletedStr == "true";
                M.Status = StatusStr == "true";
                if (InfotecFirm == "true")
                {
                    M.FirmId = Convert.ToInt32(Business.Concrete.Enums.Enums.AdminPanelFirms.Infotec);
                }

                if (M.UserId == 0)
                {
                    bool IsUserCanBeAdded = _firmDetailService.IsUserCanBeAdded(M.FirmId.Value);
                    if (IsUserCanBeAdded == false)
                    {
                        throw new Exception("Active user limit reached..");
                    }

                    byte[] PasswordHash;
                    byte[] PasswordSalt;

                    Core.Utilities.Security.Hashing.HashingHelper.CreatePasswordHash(Password, out PasswordHash, out PasswordSalt);
                    M.PasswordHash = PasswordHash;
                    M.PasswordSalt = PasswordSalt;
                    M.Status = true;
                    M.CurrencyId = 3;
                    M.DailyVolume = 1;
                    M.DailyRevenue = 0;
                    _userService.InsertUser(M);

                    Entities.Concrete.UserOperationClaim Up = new Entities.Concrete.UserOperationClaim()
                    {
                        UserId = M.UserId,
                        OperationClaimId = OperationClaimId
                    };
                    _userOperationClaimsService.InsertUserOperationClaims(Up);

                    if (IsDemoStr == "true")
                    {
                        DateTime DueDateConverted = Convert.ToDateTime(DemoDueDateStr);
                        DateTime DueDateWithTime = new DateTime(DueDateConverted.Year, DueDateConverted.Month, DueDateConverted.Day, 23, 59, 0);
                        DemoUser D = new DemoUser()
                        {
                            DemoFirmName = DemoFirmName,
                            DueDate = DueDateWithTime,
                            IsDemo = true,
                            UserId = M.UserId
                        };
                        _demoUserService.InsertDemoUser(D);
                    }
                    _emailService.SendNewUserRecordTemplateEmailToUser(M.Email, Password);
                }
                else
                {
                    Entities.Concrete.User Usr = Task.Run(() => _userService.GetUserById(M.UserId)).Result;

                    if (Usr.IsActive == false && ActivePassiveStr == "true")
                    {
                        bool IsUserCanBeAdded = _firmDetailService.IsUserCanBeAdded(M.FirmId.Value);
                        if (IsUserCanBeAdded == false)
                        {
                            throw new Exception("Active user limit reached..");
                        }
                    }
                    Firm Frm = Task.Run(() => _firmService.GetFirmById(Usr.FirmId.Value)).Result;
                    if (Frm == null || Frm.IsDeleted == true)
                        throw new Exception("In order to activate user, please activate firm " + Frm.Name + " ,first.");

                    Usr.Email = M.Email;
                    Usr.FirstName = M.FirstName;
                    Usr.LastName = M.LastName;
                    Usr.Phone = M.Phone;
                    Usr.IsDeleted = M.IsDeleted;
                    Usr.IsActive = M.IsActive;

                    _userService.UpdateUser(Usr);

                    Entities.Concrete.UserOperationClaim Up = _userOperationClaimsService.ReturnByUserId(Usr.UserId);
                    Up.OperationClaimId = OperationClaimId;
                    _userOperationClaimsService.UpdateUserOperationClaims(Up);

                    if (IsDemoStr == "true")
                    {
                        DateTime DueDateConverted = Convert.ToDateTime(DemoDueDateStr);
                        DateTime DueDateWithTime = new DateTime(DueDateConverted.Year, DueDateConverted.Month, DueDateConverted.Day, 23, 59, 0);
                        DemoUser D = _demoUserService.ReturnDemoUserByUserId(Usr.UserId);
                        if (D == null)
                        {
                            D = new DemoUser()
                            {
                                DemoFirmName = DemoFirmName,
                                DueDate = DueDateWithTime,
                                IsDemo = true,
                                UserId = Usr.UserId
                            };
                            _demoUserService.InsertDemoUser(D);
                        }
                        else
                        {
                            D.DemoFirmName = DemoFirmName;
                            D.DueDate = DueDateWithTime;
                            D.IsDemo = true;
                            _demoUserService.UpdateDemoUser(D);
                        }
                    }
                    else
                    {
                        _demoUserService.RemoveDemoUserByUserIdIfExists(Usr.UserId);
                    }
                }
                return Json(new { success = true, message = "Success.." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}

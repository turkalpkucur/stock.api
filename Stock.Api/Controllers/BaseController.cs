using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Stock.Core;
using Stock.Entities.Dtos.CustomDtos;
using Stock.Entities.Dtos.Permission;
using Stock.Entities.Dtos.User;
using System.Diagnostics;
using System.Reflection;
namespace Stock.Api.Controllers
{
    public class BaseController : Controller
    {
        public LoginUserDto CurrentUser
        {
            get { return ControllerContext.HttpContext.Session.GetObject<LoginUserDto>("user"); }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            LoginUserDto user = ControllerContext.HttpContext.Session.GetObject<LoginUserDto>("user");

            if (user == null && context.ActionDescriptor.RouteValues["controller"] != "Admin")
            {
                context.Result = (ActionResult)new RedirectToActionResult("login", "admin", null);
            }
            else
            {

                if (context.ActionDescriptor.RouteValues["controller"] != "Admin")
                {
                    var auth = (ControllerActionDescriptor)context.ActionDescriptor;
                    string CurrentPath = "/Admin/" + auth.ControllerName + "/" + auth.ActionName;

                    Type type = typeof(AppPermission);

                    List<string> requirePermisson = type
.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
.Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
.Select(x => (string)x.GetRawConstantValue())
.ToList();

                    LoginUserDto Obj = context.HttpContext.Session.GetObject<LoginUserDto>("areaadminuser");
                    List<PermissionDto> permissionNames = Obj.Permissions;
                    permissionNames = permissionNames.Where(x => requirePermisson.Contains(x.PageLink)).ToList();
                    permissionNames = permissionNames.Where(x => x.PageLink == CurrentPath).ToList();

                    if (permissionNames.Count == 0)
                    {
                        context.Result = RedirectToAction("Admin", "Admin");
                    }

                }
                else
                {
                    base.OnActionExecuting(context);
                }
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(/*string status*/)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

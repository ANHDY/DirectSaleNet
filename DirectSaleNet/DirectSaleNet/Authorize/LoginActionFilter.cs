using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace DirectSaleNet.Authorize
{
    //IActionFilter是操作连接，操作过滤
    public class LoginActionFilter:IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //获取描述，并转换成permissionAttribute的对象
            var attribute = ((ControllerActionDescriptor)context.ActionDescriptor).MethodInfo.GetCustomAttribute<PermissionAttribute>();
            if (attribute == null)
                return;
            if(attribute.IsFilter)
            {
                byte[] result;
                context.HttpContext.Session.TryGetValue("LoginUser", out result);
                if (result == null)
                    context.Result = new RedirectResult("/UserLogin/Login");
                else
                {
                    LoginUser user = Serize<LoginUser>.ByteToObject(result);
                    if (user.HasPermission(attribute.PermissionID))
                        return;
                    else
                        context.Result = new RedirectResult("/Home/Error/?ErrorMas= no permission");

                }
            }
            else
            {
                return;
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}

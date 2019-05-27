using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DirectSaleNet.Authorize;

namespace DirectSaleNet.Component
{
    [ViewComponent(Name ="TreeMenu")]
    //继承制视图组键的操作
    public class TreeMenuViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            byte[] result;
            HttpContext.Session.TryGetValue("LoginUser", out result);//得到登录用户
            if (result != null)
            {
                LoginUser user = Serize<LoginUser>.ByteToObject(result);
                return View(user.Permissions);//返回该用户的所有权限
            }
            else
                return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DirectSaleNet.Models;
using DirectSaleNet.Authorize;
using Microsoft.EntityFrameworkCore;
//控制器寻找视图是从同名的文件夹内寻找的，如果没有，就到shared中找
namespace DirectSaleNet.Controller
{
    public class UserLoginController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly DirectSaleContext _context;
        private readonly IHttpContextAccessor httpContext;
        public UserLoginController(DirectSaleContext context,IHttpContextAccessor httpContext)
        {
            _context = context;
            this.httpContext = httpContext;
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]//当点击按钮的时候调用下面的Login
        public ActionResult Login(IFormCollection form)
        {
            //查询表达式，当调用firstorDefault方法是才是调用了数据库
            var user = (from u in _context.User where (u.UserId == form["UserID"] && u.Pwd == form["Pwd"]) select u).FirstOrDefault();
            if(user==null)
            {
                ModelState.AddModelError("", "用户名或密码错误");
                return View();
            }
            else
            {
                //登录当前浏览页面，Session(缓存的)会话保存登录信息，通过Session.setString保存user.UserId，保存到UserID这个参数名中
                httpContext.HttpContext.Session.SetString("UserID", user.UserId);//只保存了编号

                LoginUser loginUser = new LoginUser();
                loginUser.user = user;
                //loginUser.Permissions = _context.Permissions.FromSql($"select * from permissions where permissionid in(select permissionid from authorationuser where userid={user.UserId}) or permissionid in(select permissionid from authorationrole a,userrole u where a.roleid=u.roleid and u.userid={user.UserId})  ").ToList();//查询权限
                loginUser.Permissions = _context.Permissions.FromSql($"execute GetPermission {user.UserId} ").ToList();
                httpContext.HttpContext.Session.Set("LoginUser", Serize<LoginUser>.ObjectToByte(loginUser));//保存了完整信息

                return RedirectToAction("/UserMain");
            }
        }
        public ActionResult UserMain()
        {
            ViewData["Title"] = "用户主页";
            return View();
        }
    }
}
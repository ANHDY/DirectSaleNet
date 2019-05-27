using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DirectSaleNet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using DirectSaleNet.Authorize;
namespace DirectSaleNet.Controller
{
    public class ManufactorController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly DirectSaleContext _context;
        public ManufactorController(DirectSaleContext context)
        {
            _context = context;
        }
        //public override void OnActionExecuted(ActionExecutedContext context)
       // {
          //  if(context.HttpContext.Session.GetString("UserID")==null)
            //    context.Result = new RedirectResult("../UserLogin/Login");
         //   else
             //   return;
            //base.OnActionExecuted(context);
        //}
        [Permission("厂商列表",PermissionID ="0102",PermissionName ="厂商查询")]
        [ServiceFilter(typeof(LoginActionFilter))]
        public async Task<IActionResult> ManufactorList(string Province,string Status,string ManufactorName,int? PageIndex,int? id)
        {
            if(id!=null)
            {
                var manu = _context.Manufactor.Find(id);
                if(manu!=null)
                {
                    _context.Manufactor.Remove(manu);
                    _context.SaveChanges();
                }
               
            }
            var manufactor = from m in _context.Manufactor select m;
            if (!string.IsNullOrEmpty(Province))
                manufactor = manufactor.Where(m => m.Province == Province);
            if (!string.IsNullOrEmpty(Status))
                manufactor = manufactor.Where(m => m.Status == Status);
            if (!string.IsNullOrEmpty(ManufactorName))
                manufactor = manufactor.Where(m => m.CompanyName.Contains(ManufactorName));
            ManufactorMode mode = new ManufactorMode();
            int count =await manufactor.CountAsync();
            mode.PageCount = (int)Math.Ceiling(count / (double)mode.PageSize);
            int pageIndex = PageIndex ?? 1;
            mode.PageIndex = (pageIndex >= mode.PageCount ? mode.PageCount : pageIndex);
            //await 异步调用
            mode.manufactors = await manufactor.Skip((pageIndex - 1)*mode.PageSize).Take(mode.PageSize).ToListAsync();
            // _context.Manufactor.FromSql($"select * from Manufactor where province={ProvinceName} and Status={Status} and CompanyName like'%{ManufactorName}%'").ToList();
            var citys = from c in _context.Citys select new { Province = c.Province };
            mode.Provinces = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(citys.Distinct().ToList(), "Province", "Province");
            return View(mode);
        }
        [Permission("厂商注册",PermissionID ="0101",PermissionName = "厂商注册",IsFilter =false)]
        [ServiceFilter(typeof(LoginActionFilter))]
        public ActionResult ManufactorEdit(int? id)
        {
            if(id==null)
            {
                ViewData["Title"] = "厂商注册";
                return View();
            }
            else
            {
                ViewData["Title"] = "编辑厂商信息";
                return View(_context.Manufactor.Find(id));
            }
        }
        [HttpPost]//也就是说点保存的时候调用的，（下面的）  
        public ActionResult ManufactorEdit(int? id,IFormCollection form)
        {
            if(ModelState.IsValid)
            {
                if (id == null)
                {
                    Manufactor manufactor = new Manufactor();
                    manufactor.CompanyName = form["CompanyName"];
                    manufactor.Address = form["Address"];
                    manufactor.Province = form["Province"];
                    manufactor.City = form["City"];
                    manufactor.ContactTel = form["ContactTel"];
                    //manufactor.RegistDate = Convert.ToDateTime(form["RegistDate"]);
                    manufactor.RegistDate = DateTime.Parse(form["RegistDate"]);
                    manufactor.Status = form["Status"];
                    _context.Manufactor.Add(manufactor);
                    _context.SaveChanges();
                }
                else
                {
                    Manufactor manufactor = _context.Manufactor.Find(id);
                    manufactor.CompanyName = form["CompanyName"];
                    manufactor.Address = form["Address"];
                    manufactor.Province = form["Province"];
                    manufactor.City = form["City"];
                    manufactor.ContactTel = form["ContactTel"];
                    //manufactor.RegistDate = Convert.ToDateTime(form["RegistDate"]);
                    manufactor.RegistDate = DateTime.Parse(form["RegistDate"]);
                    manufactor.Status = form["Status"];

                    _context.SaveChanges();
                }
                return RedirectToAction("ManufactorList");
            }
            else
            {
                ModelState.AddModelError("", "数据输入错误，请检查");
                return View();
            }
            
        }
    }
}
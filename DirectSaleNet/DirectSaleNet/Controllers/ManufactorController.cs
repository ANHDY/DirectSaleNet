using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DirectSaleNet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DirectSaleNet.Controllers
{
    public class ManufactorController : Controller
    {
        private readonly DirectSaleContext _context;
        public ManufactorController(DirectSaleContext context)
        {
            _context = context;
        }
        public IActionResult ManufactorList(string Province,string Status,string ManufactorName,int? PageIndex,int? id)
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
            int count = manufactor.Count();
            mode.PageCount = (int)Math.Ceiling(count / (double)mode.PageSize);
            int pageIndex = PageIndex ?? 1;
            mode.PageIndex = (pageIndex >= mode.PageCount ? mode.PageCount : pageIndex);
            mode.manufactors = manufactor.Skip((pageIndex - 1)*mode.PageSize).Take(mode.PageSize).ToList();
            // _context.Manufactor.FromSql($"select * from Manufactor where province={ProvinceName} and Status={Status} and CompanyName like'%{ManufactorName}%'").ToList();
            var citys = from c in _context.Citys select new { Province = c.Province };
            mode.Provinces = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(citys.Distinct().ToList(), "Province", "Province");
            return View(mode);
        }
        public  ActionResult ManufactorEdit(int? id)
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
                    ViewData["Title"] = "厂商注册";
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
                    return RedirectToAction(nameof(ManufactorList));
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
                    return RedirectToAction(nameof(ManufactorList));
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
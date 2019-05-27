using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DirectSaleNet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DirectSaleNet.Controller
{
    public class ProductController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly Models.DirectSaleContext _context;
        public ProductController(Models.DirectSaleContext context)
        {
            _context = context;
        }
        // GET: Product
        public ActionResult Index() //获取所有商品
        {
            return View(_context.Product.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)  //根据主键 获取明细信息
        {
            return View(_context.Product.Find(id));
        }

        // GET: Product/Create    
        //默认是 GET
        public ActionResult Create() //添加一个商品  insert
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Product product = new Product();
                product.ProductName = collection["ProductName"];
                product.Brand = collection["Brand"];
                product.Price = decimal.Parse(collection["Price"]);
                product.StockQuantity = int.Parse(collection["StockQuantity"]);
                product.Spec = collection["Spec"];
                product.ManufactorId = int.Parse(collection["ManufactorId"]);
                product.Description = collection["Description"];
                _context.Product.Add(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            //Product product = _context.Product.Find(id);
            //Product product = _context.Product.FirstOrDefault(m => m.ProductId == id);

            //return View(product);

            return View(_context.Product.Find(id));
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                Product product = _context.Product.Find(id);
                product.ProductName = collection["ProductName"];
                product.Brand = collection["Brand"];
                product.Price = decimal.Parse(collection["Price"]);
                product.StockQuantity = int.Parse(collection["StockQuantity"]);
                product.Spec = collection["Spec"];
                product.ManufactorId = int.Parse(collection["ManufactorId"]);
                product.Description = collection["Description"];
                _context.SaveChanges();
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_context.Product.Find(id));
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Product product = _context.Product.Find(id);
                if (product != null)
                {
                    _context.Product.Remove(product);
                    _context.SaveChanges();
                }
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
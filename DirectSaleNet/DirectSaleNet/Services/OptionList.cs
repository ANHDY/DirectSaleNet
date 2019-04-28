using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using DirectSaleNet.Models;

namespace DirectSaleNet.Services
{
    public class OptionList
    {
        private readonly DirectSaleContext _context;

        public OptionList(DirectSaleContext context)
        {
            _context = context;
        }
        public SelectList Provinces
        {
            get
            {
                var citys = from c in _context.Citys select new { Province = c.Province };
                SelectList list = new SelectList(citys.Distinct().ToList(), "Province", "Province");
                return list;

            }
        }
        public SelectList Citys
        {
            get
            {
                var citys = from c in _context.Citys select new { City = c.City };
                return new SelectList(citys.Distinct().ToList(), "City", "City");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace DirectSaleNet.Models
{
    public class ManufactorMode
    {
        public List<Manufactor> manufactors;
        public string ManufactorName { get; set; }
        //筛选的集合列表
        public string Status { get; set; } = "2";
        public string Province { get; set; }
        public SelectList Provinces { get; set; }
        //下拉选项
        public int? PageIndex { get; set; }
        public int PageSize { get; set; } = 3;
        public int PageCount { get; set; }
        public bool IsFirst{ get { return PageIndex == 1; } }
        public bool IsLast { get { return PageIndex >= PageCount; } }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace DirectSaleNet.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        [DisplayName("商品名称")]
        public string ProductName { get; set; }
        [DisplayName("品牌")]
        public string Brand { get; set; }
        [DisplayName("库存数量")]
        public int? StockQuantity { get; set; }
        public decimal? Price { get; set; }
        public string Spec { get; set; }
        public string Description { get; set; }
        public int ManufactorId { get; set; }
    }
}

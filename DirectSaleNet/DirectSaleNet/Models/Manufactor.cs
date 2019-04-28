using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DirectSaleNet.Models
{
    public partial class Manufactor
    {
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        [Phone]
        public string ContactTel { get; set; }
        [DisplayName("注册日期")]
        [DataType("DateTime",ErrorMessage ="日期错误")]
        
        public DateTime? RegistDate { get; set; }
        public string Status { get; set; }
    }
}

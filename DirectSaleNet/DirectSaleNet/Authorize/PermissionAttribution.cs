using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
namespace DirectSaleNet.Authorize
{
    public class PermissionAttribute:DescriptionAttribute
    {
        public PermissionAttribute(string description) : base(description) { }
        public string PermissionID { get; set; }
        public string PermissionName { get; set; }
        public bool IsFilter { get; set; } = true;
    }
}

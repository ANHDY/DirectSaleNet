using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace DirectSaleNet.Models
{
    public class Permissions
    {
        [Key]
        public string PermissionID { get; set; }
        public string PermissionName { get; set; }
        public string Url { get; set; }
        public string TopID { get; set; }
        public string Cata { get; set; }
        public string Target { get; set; }
    }
}

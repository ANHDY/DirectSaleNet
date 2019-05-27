using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DirectSaleNet.Models;
namespace DirectSaleNet.Authorize
{
    public class LoginUser
    {
        public User user { get; set; }
        public DateTime LastTime { get; set; } = DateTime.Now;
        public List<Permissions> Permissions { get; set; }
        public bool HasPermission(string PermissionID)
        {
            if (Permissions.Where(p => p.PermissionID == PermissionID).FirstOrDefault() != null)
                return true;
            else
                return false;
        }

    }
}

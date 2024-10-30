using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMERPSystem.Classes
{
    public class Admin
    {
        public int AdminId { get; set; }
        public bool IsAdmin { get; set; }
        public Admin(int adminId, bool isAdmin)
        {
            AdminId = adminId;
            IsAdmin = isAdmin;
        }
    }
}

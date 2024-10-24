using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMERPSystem.Classes.User
{
    public class Admin : Employee
    {
        public int AdminId { get; set; }
        public bool IsAdmin { get; set; }
        public Admin(int adminId, bool isAdmin,int employeeId, string emplyeePassword, string title, string workMail, string workTlf
            ,string adminPassword,int personId, string firstName, string lastName, string cprNumber, PersonaLInfo pInfo) :
            base(employeeId, emplyeePassword, title, workMail, workTlf, adminPassword, personId, firstName, lastName, cprNumber, pInfo)  
        {
            AdminId = adminId;
            IsAdmin = isAdmin;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMERPSystem.Classes.User
{
    public class Moderator : Employee
    {
        public int ModeratorId { get; set; }
        public bool IsMod { get; set; }
        public Moderator(int modId, bool isMod,int employeeId, string emplyeePassword, string title, string workMail, string workTlf
            ,string adminPassword,int personId, string firstName, string lastName, string cprNumber, PersonaLInfo pInfo) :
            base(employeeId,emplyeePassword,title,workMail,workTlf,adminPassword,personId, firstName, lastName, cprNumber, pInfo) 
        {
            ModeratorId = modId;
            IsMod = isMod;
        }
    }
}

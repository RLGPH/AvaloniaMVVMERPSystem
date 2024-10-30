using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMERPSystem.Classes
{
    public class User : Person
    {
        public int UserId { get; set; }
        public string UPassWord { get; set; }

        public User(int userId, string UpassWord, int personId, string firstName, string lastName, string cprNumber, PersonaLInfo pInfo) 
            :base(personId,firstName,lastName,cprNumber,pInfo)
        { 
            UserId = userId;
            UPassWord = UpassWord;
        }
    }
}

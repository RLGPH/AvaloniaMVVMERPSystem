using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMERPSystem.Classes.User
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CPRNumber { get; set; }
        public PersonaLInfo PInfo { get; set; }

        public Person(int personId, string firstName, string lastName, string cprNumber, PersonaLInfo pInfo) 
        {
            PersonId = personId;
            FirstName = firstName;
            LastName = lastName;
            CPRNumber = cprNumber;
            PInfo = pInfo;
        } 
    }
}

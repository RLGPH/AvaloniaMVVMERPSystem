using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMERPSystem.Classes
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public PersonaLInfo PInfo { get; set; }

        public Person(int personId, string firstName, string lastName, PersonaLInfo pInfo) 
        {
            PersonId = personId;
            FirstName = firstName;
            LastName = lastName;
            PInfo = pInfo;
        }
    }
}

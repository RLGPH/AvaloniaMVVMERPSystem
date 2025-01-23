using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMERPSystem.Classes
{
    public class PersonaLInfo
    {
        public int PersonalInfoId { get; set; }
        public string Mail { get; set; }
        public string Tlf { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string RoadName { get; set; }
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CPRNumber { get; set; }

        public PersonaLInfo(int personalInfoId, string mail, string tlf, string address, string postalCode,
            string roadName, string houseNumber, string city, string country, string cprNumber) 
        {
            PersonalInfoId = personalInfoId;
            Mail = mail;
            Tlf = tlf;
            Address = address;
            PostalCode = postalCode;
            RoadName = roadName;
            HouseNumber = houseNumber;
            City = city;
            Country = country;
            CPRNumber = cprNumber;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMERPSystem.Classes
{
    public class Location
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string LCountry { get; set; }
        public string LCity { get; set; }
        public string LStreet { get; set; }
        public string LZipCode { get; set; }
        public int StorageSpaceLeft {  get; set; }
    }
}

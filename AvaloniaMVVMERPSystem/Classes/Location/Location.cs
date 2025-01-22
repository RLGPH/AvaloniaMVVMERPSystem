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
        public float StorageSpaceLeft {  get; set; }
        public float MaxStorageSpace {  get; set; }

        public Location(int locationId, string locationName, string lCountry, string lCity, string lStreet, 
            string lZipCode, float storageSpaceLeft, float maxStorageSpace) 
        {
            LocationId = locationId;
            LocationName = locationName;
            LCountry = lCountry;
            LCity = lCity;
            LStreet = lStreet;
            LZipCode = lZipCode;
            StorageSpaceLeft = storageSpaceLeft;
            MaxStorageSpace = maxStorageSpace;
        }
    }
}

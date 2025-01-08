using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMERPSystem.Classes
{
    public class CombinedItemLocation
    {
        public int CominedID { get; set; }
        public Location Location { get; set; }
        public Item item { get; set; }
        
        public CombinedItemLocation(int cominedID, Location location, Item thing)
        {
            CominedID = cominedID;
            Location = location;
            item = thing;
        }
    }
}

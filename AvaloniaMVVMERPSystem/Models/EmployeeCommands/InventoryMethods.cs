using AvaloniaMVVMERPSystem.Classes;
using AvaloniaMVVMERPSystem.DataBase;
using AvaloniaMVVMERPSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMERPSystem.Models
{
    public partial class ModelCommands
    {
        public ObservableCollection<Location> GetLocations() 
        {
            Database database = new();
            
            return database.GetLocations(); 
        }

        public string AddInventory(Location location, string ItemName, string ItemDesctription, Database database, float StorageSpaceTaken)
        {
            Item item = new(0,ItemName,ItemDesctription);
            CombinedItemLocation combinedItemLocation = new(0,location,item);
            if (combinedItemLocation != null)
            {
                int itemId = database.AddItem(item);
                database.AddCombinedLocation(combinedItemLocation.Location.LocationId, itemId);
                return "Succes fully added new Item";
            }
            return "Failed to add the item";
        }
        public string AddLocation(Database database, string LocationName, string LCountry, string LCity,
            string LStreet, string LZipCode, float StorageSpaceLeft)
        {
            Location location = new(0, LocationName, LCountry, LCity, LStreet, LZipCode, StorageSpaceLeft);
            database.AddLocation(location);
            return "Succes fully added new Location";
        }
        
    }
}

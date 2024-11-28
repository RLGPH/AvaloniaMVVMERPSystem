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

        public string AddInventory(Location location, string ItemName, string ItemDesctription, Database database)
        {
            Item item = new(0,ItemName,ItemDesctription);
            CombinedItemLocation combinedItemLocation = new(0,location,item);
            if (combinedItemLocation != null)
            {
                database.AddItem(combinedItemLocation.item);
                database.AddCombinedLocation(combinedItemLocation.Location.LocationId, combinedItemLocation.item.Id);
                return "Succes fully added new Item";
            }
            return "Failed to add the item";
        }
        public void AddLocation(Database database)
        {

        }
    }
}

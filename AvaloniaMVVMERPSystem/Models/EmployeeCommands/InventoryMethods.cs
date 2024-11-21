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
    }
}

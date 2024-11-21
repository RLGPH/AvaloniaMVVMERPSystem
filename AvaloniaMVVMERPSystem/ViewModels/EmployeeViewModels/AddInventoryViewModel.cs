using AvaloniaMVVMERPSystem.Classes;
using AvaloniaMVVMERPSystem.DataBase;
using AvaloniaMVVMERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMERPSystem.ViewModels
{
    public class AddInventoryViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _MainWindowViewModel;
        private readonly Database _Database;
        private readonly ModelCommands _ModelCommands;
        private readonly Employee _employee;
        private readonly Location _location;

        public string LCountry { get; set; }
        public string LCity { get; set; }
        public string LStreet { get; set; }
        public string LZipCode { get; set; }
        public string StorageSpaceLeft { get; set; }
        public AddInventoryViewModel(MainWindowViewModel mainWindowViewModel, Database database, ModelCommands modCommands, Employee employee)
        {
            _MainWindowViewModel = mainWindowViewModel;
            _Database = database;
            _ModelCommands = modCommands;
            _employee = employee;


        }
    }
}

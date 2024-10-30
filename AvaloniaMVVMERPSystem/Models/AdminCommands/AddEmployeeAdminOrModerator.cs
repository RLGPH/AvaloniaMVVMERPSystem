using AvaloniaMVVMERPSystem.Classes;
using AvaloniaMVVMERPSystem.DataBase;
using AvaloniaMVVMERPSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMERPSystem.Models.AdminCommands
{
    public partial class ModelCommands
    {
        public void AddEmployee(Database _Database, MainWindowViewModel _MainWindowViewModel, ModelCommands modelCommands, Employee employee)
        {
            _MainWindowViewModel.SwitchViewModel(new AddEmployeeViewModel(_MainWindowViewModel, _Database, modelCommands, employee));
        }
    }
}

using AvaloniaMVVMERPSystem.Classes;
using AvaloniaMVVMERPSystem.DataBase;
using AvaloniaMVVMERPSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMERPSystem.Models
{
    public partial class ModelCommands
    {
        public void SwitchToNormLogin(Database database, MainWindowViewModel _MainWindowViewModel, ModelCommands modelCommands)
        {
            _MainWindowViewModel.SwitchViewModel(new LoginViewModel(_MainWindowViewModel, database, modelCommands));
        }
        public void SwitchToAdminMenu(Database dataBase, MainWindowViewModel _MainWindowViewModel, ModelCommands modelCommands, Employee employee)
        {
            _MainWindowViewModel.SwitchViewModel(new AdminModViewModel(_MainWindowViewModel, dataBase, modelCommands, employee));
        }
        public void SwitchToEmployeeLogin(Database _Database, MainWindowViewModel _MainWindowViewModel, ModelCommands modelCommands)
        {
            _MainWindowViewModel.SwitchViewModel(new EmployeeLoginViewModel(_MainWindowViewModel, _Database, modelCommands));
        }
        public void LoginAsEmployee(Database dataBase, MainWindowViewModel _MainWindowViewModel, ModelCommands modelCommands, Employee employee)
        {
            _MainWindowViewModel.SwitchViewModel(new EmployeeMainMenuViewModel(_MainWindowViewModel, dataBase, modelCommands, employee));
        }
        public void SwitchToAdminLogin(Database _database, MainWindowViewModel _MainWindowViewModel, ModelCommands modelCommands)
        {
            _MainWindowViewModel.SwitchViewModel(new AdminLoginViewModel(_MainWindowViewModel, _database, modelCommands));
        }
        public void LoginAsAdmin(Database _database, MainWindowViewModel _MainWindowViewModel, ModelCommands modelCommands, Employee employee)
        {
            _MainWindowViewModel.SwitchViewModel(new AdminModViewModel(_MainWindowViewModel, _database, modelCommands, employee));
        }
        public void SwitchToEditAccountA(Database _database, Employee _employee, MainWindowViewModel _mainWindowViewModel, ModelCommands modelCommands, Employee editEmployee)
        {
            _mainWindowViewModel.SwitchViewModel(new EditAccountViewModel(_mainWindowViewModel, _database, modelCommands, _employee, editEmployee));
        }
        public void SwitchToAdminEmployeeList(Database _database, Employee _employee, MainWindowViewModel _mainWindowViewModel, ModelCommands modelCommands)
        {
            _mainWindowViewModel.SwitchViewModel(new AdminEmployeeListViewModel(_mainWindowViewModel, _database, modelCommands, _employee));
        }
        public void SwitchToAddInventory(Database _dataBase, MainWindowViewModel _mainWindowViewModel, ModelCommands modelCommands, Employee _employee)
        {
            _mainWindowViewModel.SwitchViewModel(new AddInventoryViewModel(_mainWindowViewModel,_dataBase, modelCommands, _employee));
        }
    }
}

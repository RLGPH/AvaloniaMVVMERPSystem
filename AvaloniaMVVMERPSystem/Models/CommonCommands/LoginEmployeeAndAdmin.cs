using AvaloniaMVVMERPSystem.Classes;
using AvaloniaMVVMERPSystem.DataBase;
using AvaloniaMVVMERPSystem.ViewModels;


namespace AvaloniaMVVMERPSystem.Models
{
    public partial class ModelCommands
    {
        public void SwitchToEmployeeLogin(Database _Database, MainWindowViewModel _MainWindowViewModel, ModelCommands modelCommands)
        {
            _MainWindowViewModel.SwitchViewModel(new EmployeeLoginViewModel(_MainWindowViewModel, _Database, modelCommands));
        }
        
        public void LoginAsEmployee()
        {

        }

        public void SwitchToAdminLogin(Database _Database, MainWindowViewModel _MainWindowViewModel, ModelCommands modelCommands)
        {
            _MainWindowViewModel.SwitchViewModel(new AdminLoginViewModel(_MainWindowViewModel, _Database, modelCommands));
        }

        public void LoginAsAdmin(Database _Database, MainWindowViewModel _MainWindowViewModel, ModelCommands modelCommands, Employee employee)
        {
            _MainWindowViewModel.SwitchViewModel(new AdminModViewModel(_MainWindowViewModel, _Database, modelCommands, employee));
        }
    }
}

using System.Reactive;
using AvaloniaMVVMERPSystem.DataBase;
using ReactiveUI;
using AvaloniaMVVMERPSystem.Models;

namespace AvaloniaMVVMERPSystem.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _MainWindowViewModel;
        private readonly Database _Database;
        private ModelCommands _modelCommands;

        public ReactiveCommand<Unit, Unit> EmployeeLoginCommand { get; }
        public ReactiveCommand<Unit, Unit> AdminLoginCommand { get; }
        public ReactiveCommand<Unit, Unit> RegristorOpenCommand { get; }

        public LoginViewModel(MainWindowViewModel mainWindowViewModel, Database database, ModelCommands modCommands)
        {
            _MainWindowViewModel = mainWindowViewModel;
            _Database = database;
            _modelCommands = modCommands;

            // Initialize the command
            EmployeeLoginCommand = ReactiveCommand.Create(() => modCommands.SwitchToEmployeeLogin(database, mainWindowViewModel, modCommands));
            AdminLoginCommand = ReactiveCommand.Create(() => modCommands.SwitchToAdminLogin(database, mainWindowViewModel, modCommands));
            RegristorOpenCommand = ReactiveCommand.Create(() => modCommands.SwitchToRegristor(database, mainWindowViewModel, modCommands));
        }
    }
}

using System.Reactive;
using AvaloniaMVVMERPSystem.DataBase;
using ReactiveUI;
using AvaloniaMVVMERPSystem.Models;
using AvaloniaMVVMERPSystem.Classes;
using System.Collections.ObjectModel;

namespace AvaloniaMVVMERPSystem.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _MainWindowViewModel;
        private readonly Database _Database;
        private ModelCommands _modelCommands;

        private string _firstName;
        private string _lastName;
        private string _Password;
        public string FirstName
        {
            get => _firstName;
            set => this.RaiseAndSetIfChanged(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => this.RaiseAndSetIfChanged(ref _lastName, value);
        }
        public string Password
        {
            get => _Password;
            set => this.RaiseAndSetIfChanged(ref _Password, value);
        }

        public ReactiveCommand<Unit, Unit> LoginCommand { get; set; }
        public ReactiveCommand<Unit, Unit> EmployeeLoginCommand { get; }
        public ReactiveCommand<Unit, Unit> AdminLoginCommand { get; }
        public ReactiveCommand<Unit, Unit> RegristorOpenCommand { get; }

        private ObservableCollection<Employee> _employees;

        public LoginViewModel(MainWindowViewModel mainWindowViewModel, Database database, ModelCommands modCommands)
        {
            _MainWindowViewModel = mainWindowViewModel;
            _Database = database;
            _modelCommands = modCommands;

            _employees = database.GetAllEmployees();
            if (_employees.Count <= 0)
            {
                Classes.Admin admin = new(0, true);
                Classes.Moderator mod = new(0, true);
                Classes.PersonaLInfo pinfo = new(0, "???", "???", "???", "???", "???", "???", "???", "???", "???");
                Classes.Employee employee = new(0, "Pass123", "Standard Admin", "???", "???", "APass123", 0, "Admini", "Strator", pinfo, admin, mod);
                modCommands.FirstTimeBoot(employee, database);
            }

            // Initialize the command
            EmployeeLoginCommand = ReactiveCommand.Create(() => modCommands.SwitchToEmployeeLogin(database, mainWindowViewModel, modCommands));
            
            AdminLoginCommand = ReactiveCommand.Create(() => modCommands.SwitchToAdminLogin(database, mainWindowViewModel, modCommands));

            RegristorOpenCommand = ReactiveCommand.Create(() => modCommands.SwitchToRegristor(database, mainWindowViewModel, modCommands));

            LoginCommand = ReactiveCommand.Create(() => modCommands.SwitchToUserMainPage(FirstName,LastName,Password,database,mainWindowViewModel,modCommands));
        }
    }
}

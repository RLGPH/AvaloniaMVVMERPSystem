using AvaloniaMVVMERPSystem.DataBase;
using ReactiveUI;
using AvaloniaMVVMERPSystem.Models;
using AvaloniaMVVMERPSystem.Classes;
using System.Reactive;


namespace AvaloniaMVVMERPSystem.ViewModels
{
    public class AdminLoginViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _MainWindowViewModel;
        private readonly Database _Database;
        private ModelCommands _modelCommands;

        private string _firstName;
        private string _lastName;
        private string _employeePassword;
        private string _adminPassword;
        private int _employeeId;

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

        public string EmployeePassword
        {
            get => _employeePassword;
            set => this.RaiseAndSetIfChanged(ref _employeePassword, value);
        }

        public string AdminPassword
        {
            get => _adminPassword;
            set => this.RaiseAndSetIfChanged(ref _adminPassword, value);
        }

        public int EmployeeId
        {
            get => _employeeId;
            set => this.RaiseAndSetIfChanged(ref _employeeId, value);
        }

        public ReactiveCommand<Unit, Unit> LoginCommand { get; }

        public ReactiveCommand<Unit, Unit> BackToNormlogin { get; }

        public AdminLoginViewModel(MainWindowViewModel mainWindowViewModel, Database database, ModelCommands modCommands)
        {
            _MainWindowViewModel = mainWindowViewModel;
            _Database = database;
            _modelCommands = modCommands;

            LoginCommand = ReactiveCommand.Create(() => modCommands.LoginAuthentication(EmployeeId, FirstName, LastName, EmployeePassword, AdminPassword, mainWindowViewModel, database, modCommands));

            BackToNormlogin = ReactiveCommand.Create(() => modCommands.SwitchToNormLogin(database, mainWindowViewModel, modCommands));
        }
    }
}

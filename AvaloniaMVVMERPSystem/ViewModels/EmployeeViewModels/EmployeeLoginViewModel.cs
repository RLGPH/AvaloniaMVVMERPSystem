using AvaloniaMVVMERPSystem.DataBase;
using ReactiveUI;
using System.Reactive;
using AvaloniaMVVMERPSystem.Models;

namespace AvaloniaMVVMERPSystem.ViewModels
{
    public class EmployeeLoginViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _MainWindowViewModel;
        private readonly Database _Database;
        private ModelCommands _ModelCommands;
        /*
        private string _firstName;
        private string _lastName;
        private string _employeePassword;
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

        public int EmployeeId
        {
            get => _employeeId;
            set => this.RaiseAndSetIfChanged(ref _employeeId, value);
        }
        */

        ReactiveCommand<Unit,Unit> EmployeeLogin {  get; set; }
        ReactiveCommand<Unit, Unit> BackToNormlogin { get; set; }

        public EmployeeLoginViewModel(MainWindowViewModel mainWindowViewModel, Database database, ModelCommands modCommands)
        {
            _MainWindowViewModel = mainWindowViewModel;
            _Database = database;
            _ModelCommands = modCommands;

            //EmployeeLogin = ReactiveCommand.Create(() => modCommands.EmployeeLogin(database,mainWindowViewModel);

            BackToNormlogin = ReactiveCommand.Create(() => modCommands.SwitchToNormLogin(database, mainWindowViewModel, modCommands));
        }
    }
}

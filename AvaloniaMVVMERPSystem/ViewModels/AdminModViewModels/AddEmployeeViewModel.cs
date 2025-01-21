using AvaloniaMVVMERPSystem.Classes;
using AvaloniaMVVMERPSystem.DataBase;
using AvaloniaMVVMERPSystem.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMERPSystem.ViewModels
{
#pragma warning disable
    public class AddEmployeeViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _MainWindowViewModel;
        private readonly Database _Database;
        private ModelCommands _ModelCommands;
        private Employee _employee;

        // Properties for form fields
        public string CPRNumber { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string RoadName { get; set; }
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PersonalMail { get; set; }
        public string PersonalPhone { get; set; }
        public string WorkMail { get; set; }
        public string WorkPhone { get; set; }
        public string Title { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsMod { get; set; }
        private string _password;
        private string _reenterPassword;
        private string _adminPassword;
        private string _reenterAdminPassword;
        private string _passwordError;
        private string _adminPasswordError;

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public string ReenterPassword
        {
            get => _reenterPassword;
            set => this.RaiseAndSetIfChanged(ref _reenterPassword, value);
        }

        public string AdminPassword
        {
            get => _adminPassword;
            set => this.RaiseAndSetIfChanged(ref _adminPassword, value);
        }

        public string ReenterAdminPassword
        {
            get => _reenterAdminPassword;
            set => this.RaiseAndSetIfChanged(ref _reenterAdminPassword, value);
        }

        public string PasswordError
        {
            get => _passwordError;
            private set => this.RaiseAndSetIfChanged(ref _passwordError, value);
        }

        public string AdminPasswordError
        {
            get => _adminPasswordError;
            private set => this.RaiseAndSetIfChanged(ref _adminPasswordError, value);
        }

        public ObservableCollection<string> CountryCodes { get; } = new ObservableCollection<string>
        {
            "+1", "+45", "+91", "+61", "N/A"
        };

        private string _personalPhoneCountryCode;
        public string PersonalPhoneCountryCode
        {
            get => _personalPhoneCountryCode;
            set => this.RaiseAndSetIfChanged(ref _personalPhoneCountryCode, value);
        }

        private string _workPhoneCountryCode;
        public string WorkPhoneCountryCode
        {
            get => _workPhoneCountryCode;
            set => this.RaiseAndSetIfChanged(ref _workPhoneCountryCode, value);
        }

        public ReactiveCommand<Unit, Unit> BackToMenu { get; }

        public ReactiveCommand<Unit, Unit> AddEmployee { get; }

        public AddEmployeeViewModel(MainWindowViewModel mainWindowViewModel, Database database, ModelCommands modCommands, Employee employee)
        {
            _MainWindowViewModel = mainWindowViewModel;
            _Database = database;
            _ModelCommands = modCommands;
            _employee = employee;

            BackToMenu = ReactiveCommand.Create(() => modCommands.SwitchToAdminMenu(database, mainWindowViewModel, modCommands, employee));

            // Password validation
            this.WhenAnyValue(x => x.Password, x => x.ReenterPassword)
                .Subscribe(_ => ValidatePasswords());

            this.WhenAnyValue(x => x.AdminPassword, x => x.ReenterAdminPassword)
                .Subscribe(_ => ValidateAdminPasswords());

            // Command to add employee
            AddEmployee = ReactiveCommand.Create(() =>
            {
                _ModelCommands.AddEmployee(
                    employeeId: 0,
                    emplyeePassword: Password,
                    title: Title,
                    workMail: WorkMail,
                    workTlf: WorkPhoneCountryCode + WorkPhone,
                    adminPassword: AdminPassword,
                    personId: 0,
                    firstName: FirstName,
                    lastName: LastName,
                    cprNumber: CPRNumber,
                    personalInfoId: 0,
                    mail: PersonalMail,
                    tlf: PersonalPhoneCountryCode + PersonalPhone,
                    address: Address,
                    postalCode: PostalCode,
                    roadName: RoadName,
                    houseNumber: HouseNumber,
                    city: City,
                    country: Country,
                    modId: 0,
                    isMod: IsMod,
                    adminId: 0,
                    isAdmin: IsAdmin,
                    _Database,
                    _MainWindowViewModel,
                    _ModelCommands,
                    _employee);
            });
        }

        private void ValidatePasswords()
        {
            PasswordError = Password == ReenterPassword ? string.Empty : "Passwords do not match.";
        }

        private void ValidateAdminPasswords()
        {
            AdminPasswordError = AdminPassword == ReenterAdminPassword ? string.Empty : "Admin passwords do not match.";
        }
    }
}

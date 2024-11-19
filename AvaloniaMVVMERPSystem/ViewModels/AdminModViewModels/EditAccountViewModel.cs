using AvaloniaMVVMERPSystem.Classes;
using AvaloniaMVVMERPSystem.DataBase;
using AvaloniaMVVMERPSystem.Models;
using ReactiveUI;
using System;
using System.Reactive;

namespace AvaloniaMVVMERPSystem.ViewModels
{
    public class EditAccountViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _MainWindowViewModel;
        private readonly Database _Database;
        private readonly ModelCommands _ModelCommands;
        private readonly Employee _employee;

        private string _firstName;
        private string _lastName;
        private string _password;
        private string _reenterPassword;
        private string _cprNumber;
        private string _postalCode;
        private string _address;
        private string _roadName;
        private string _houseNumber;
        private string _city;
        private string _country;
        private string _personalMail;
        private string _personalPhone;
        private string _workMail;
        private string _workPhone;
        private string _title;
        private bool _isAdmin;
        private bool _isMod;
        private string _adminPassword;
        private string _reenterAdminPassword;

        // Reactive properties
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
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public string ReenterPassword
        {
            get => _reenterPassword;
            set => this.RaiseAndSetIfChanged(ref _reenterPassword, value);
        }

        public string CPRNumber
        {
            get => _cprNumber;
            set => this.RaiseAndSetIfChanged(ref _cprNumber, value);
        }

        public string PostalCode
        {
            get => _postalCode;
            set => this.RaiseAndSetIfChanged(ref _postalCode, value);
        }

        public string Address
        {
            get => _address;
            set => this.RaiseAndSetIfChanged(ref _address, value);
        }

        public string RoadName
        {
            get => _roadName;
            set => this.RaiseAndSetIfChanged(ref _roadName, value);
        }

        public string HouseNumber
        {
            get => _houseNumber;
            set => this.RaiseAndSetIfChanged(ref _houseNumber, value);
        }

        public string City
        {
            get => _city;
            set => this.RaiseAndSetIfChanged(ref _city, value);
        }

        public string Country
        {
            get => _country;
            set => this.RaiseAndSetIfChanged(ref _country, value);
        }

        public string PersonalMail
        {
            get => _personalMail;
            set => this.RaiseAndSetIfChanged(ref _personalMail, value);
        }

        public string PersonalPhone
        {
            get => _personalPhone;
            set => this.RaiseAndSetIfChanged(ref _personalPhone, value);
        }

        public string WorkMail
        {
            get => _workMail;
            set => this.RaiseAndSetIfChanged(ref _workMail, value);
        }

        public string WorkPhone
        {
            get => _workPhone;
            set => this.RaiseAndSetIfChanged(ref _workPhone, value);
        }

        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        public bool IsAdmin
        {
            get => _isAdmin;
            set => this.RaiseAndSetIfChanged(ref _isAdmin, value);
        }

        public bool IsMod
        {
            get => _isMod;
            set => this.RaiseAndSetIfChanged(ref _isMod, value);
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

        // Commands
        public ReactiveCommand<Unit, Unit> BackToMenu { get; }
        public ReactiveCommand<Unit, Unit> EditEmployee { get; }

        public EditAccountViewModel(MainWindowViewModel mainWindowViewModel, Database database, ModelCommands modCommands, Employee employee, Employee editEmployee)
        {
            _MainWindowViewModel = mainWindowViewModel;
            _Database = database;
            _ModelCommands = modCommands;
            _employee = employee;

            // Initialize commands
            BackToMenu = ReactiveCommand.Create(() => modCommands.SwitchToAdminMenu(database, mainWindowViewModel, modCommands, employee));
            EditEmployee = ReactiveCommand.Create(() => UpdateEmployee(editEmployee));

            // Load initial data
            FirstName = editEmployee.FirstName;
            LastName = editEmployee.LastName;
            Password = "*********";
            ReenterPassword = "*********";
            AdminPassword = "*********";
            ReenterAdminPassword = "*********";
            CPRNumber = editEmployee.CPRNumber;
            PostalCode = editEmployee.PInfo.PostalCode;
            Address = editEmployee.PInfo.Address;
            RoadName = editEmployee.PInfo.RoadName;
            HouseNumber = editEmployee.PInfo.HouseNumber;
            City = editEmployee.PInfo.City;
            Country = editEmployee.PInfo.Country;
            PersonalMail = editEmployee.PInfo.Mail;
            PersonalPhone = editEmployee.PInfo.Tlf;
            WorkMail = editEmployee.WorkMail;
            WorkPhone = editEmployee.WorkTlf;
            Title = editEmployee.Title;
            IsAdmin = editEmployee._admin.IsAdmin;
            IsMod = editEmployee._moderator.IsMod;
        }

        private void UpdateEmployee(Employee editEmployee)
        {
            var info = new PersonaLInfo(editEmployee.PInfo.PersonalInfoId, PersonalMail, PersonalPhone, Address, PostalCode, RoadName,
                HouseNumber, City, Country);
            var admin = new Admin(editEmployee._admin.AdminId, IsAdmin);
            var mod = new Moderator(editEmployee._moderator.ModeratorId, IsMod);

            var newEmployee = new Employee(editEmployee.EmployeeId, Password, Title, WorkMail, WorkPhone, AdminPassword,
                editEmployee.PersonId, FirstName, LastName, CPRNumber, info, admin, mod);

            _ModelCommands.EditAccount(newEmployee, editEmployee, _Database, _MainWindowViewModel, _ModelCommands);
        }
    }
}

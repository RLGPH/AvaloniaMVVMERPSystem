using AvaloniaMVVMERPSystem.Classes;
using AvaloniaMVVMERPSystem.DataBase;
using AvaloniaMVVMERPSystem.Models;
using ReactiveUI;
using System;
using System.ComponentModel.DataAnnotations;
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
        private string _oldPassword;
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
        private string _oldAdminPassword;
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
        public string OldPassword
        {
            get => _oldPassword;
            set => this.RaiseAndSetIfChanged(ref _oldPassword, value);
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
        public string OldAdminPassword
        {
            get => _oldAdminPassword;
            set => this.RaiseAndSetIfChanged(ref _oldAdminPassword, value);
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

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => this.RaiseAndSetIfChanged(ref _isPopupOpen, value);
        }

        // Commands
        public ReactiveCommand<Unit, Unit> BackAsAdmin { get; }
        public ReactiveCommand<Unit, Unit> EditEmployee { get; }
        public ReactiveCommand<Unit, Unit> BackAsEmployee {  get; }
        public ReactiveCommand<Unit, Unit> ContinueCommand { get; }
        public ReactiveCommand<Unit, Unit> CancelCommand { get; }

        public EditAccountViewModel(MainWindowViewModel mainWindowViewModel, Database database, ModelCommands modCommands, Employee employee, Employee editEmployee)
        {
            _MainWindowViewModel = mainWindowViewModel;
            _Database = database;
            _ModelCommands = modCommands;
            _employee = employee;

            // Initialize commands
            BackAsAdmin = ReactiveCommand.Create(() => { IsPopupOpen = true; });

            EditEmployee = ReactiveCommand.Create(() => 
            {
                PersonaLInfo personaLInfo = new(editEmployee.PInfo.PersonalInfoId,PersonalMail,PersonalPhone,Address,
                    PostalCode, RoadName, HouseNumber, City, Country);
                Employee ChangedEm = new(editEmployee.EmployeeId, editEmployee.EmployeePassword, Title, WorkMail, WorkPhone,
                    editEmployee.AdminPassword, editEmployee.PersonId, FirstName, LastName, editEmployee.CPRNumber,personaLInfo,editEmployee._admin,editEmployee._moderator);
                if (Password != null && Password == ReenterPassword && PasswordHasher.VerifyPassword(OldPassword, editEmployee.EmployeePassword))
                {
                    ChangedEm.EmployeePassword = PasswordHasher.HashPassword(Password);
                }
                if (AdminPassword != null && AdminPassword == ReenterAdminPassword && PasswordHasher.VerifyPassword(OldAdminPassword, editEmployee.AdminPassword))
                {
                    ChangedEm.AdminPassword = PasswordHasher.HashPassword(AdminPassword);
                }
                    modCommands.EditAccount(ChangedEm, employee, database, mainWindowViewModel, modCommands);
            });

            BackAsEmployee = ReactiveCommand.Create(() => modCommands.LoginAsEmployee(database, _MainWindowViewModel, modCommands, employee));
            ContinueCommand = ReactiveCommand.Create(() =>
            {
                if (AdminPassword == ReenterAdminPassword && PasswordHasher.VerifyPassword(AdminPassword, employee.AdminPassword))
                {
                    modCommands.SwitchToAdminMenu(database, mainWindowViewModel, modCommands, employee);
                    IsPopupOpen = false;
                }
            });

            CancelCommand = ReactiveCommand.Create(() => { IsPopupOpen = false; });
            // Load initial data
            FirstName = editEmployee.FirstName;
            LastName = editEmployee.LastName;
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
    }
}

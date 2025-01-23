using AvaloniaMVVMERPSystem.Classes;
using AvaloniaMVVMERPSystem.DataBase;
using AvaloniaMVVMERPSystem.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMERPSystem.ViewModels
{
    public class EditUserAccountViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _MainWindowViewModel;
        private readonly Database _Database;
        private ModelCommands _ModelCommands;
        private User _user;

        private string _status;
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
        private float _balance;

        // Reactive properties
        public string Status
        {
            get => _status;
            set => this.RaiseAndSetIfChanged(ref _status, value);
        }
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
        public float Balance
        {
            get => _balance;
            set => this.RaiseAndSetIfChanged(ref _balance, value);
        }

        // Commands
        public ReactiveCommand<Unit, Unit> SaveEdit { get; }
        public ReactiveCommand<Unit, Unit> BackToMain { get; }

        public EditUserAccountViewModel(MainWindowViewModel mainWindowViewModel, Database database, ModelCommands modCommands, User user)
        {
            _MainWindowViewModel = mainWindowViewModel;
            _Database = database;
            _ModelCommands = modCommands;
            _user = user;
            
            FirstName = user.FirstName;
            LastName = user.LastName;
            Balance = user.Balance;
            CPRNumber = user.PInfo.CPRNumber;
            PostalCode = user.PInfo.PostalCode;
            Address = user.PInfo.Address;
            RoadName = user.PInfo.RoadName;
            HouseNumber = user.PInfo.HouseNumber;
            City = user.PInfo.City;
            Country = user.PInfo.Country;
            PersonalPhone = user.PInfo.Tlf;
            PersonalMail = user.PInfo.Mail;

            SaveEdit = ReactiveCommand.Create(() =>
            {
                Status = modCommands.SaveUserEdit(
                    FirstName, LastName, CPRNumber, PostalCode, Address, RoadName, HouseNumber, City, Country, PersonalPhone, PersonalMail, Balance,
                    Password, ReenterPassword, OldPassword, user, database
                );
            });
            BackToMain = ReactiveCommand.Create(() => modCommands.SwitchToUserMainNoLogin(database, mainWindowViewModel, modCommands, user));
        }
    }
}

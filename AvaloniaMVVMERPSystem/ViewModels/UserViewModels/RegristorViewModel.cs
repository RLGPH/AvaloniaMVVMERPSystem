using AvaloniaMVVMERPSystem.Classes;
using AvaloniaMVVMERPSystem.DataBase;
using AvaloniaMVVMERPSystem.Models;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive;

namespace AvaloniaMVVMERPSystem.ViewModels
{
    public class RegristorViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _MainWindowViewModel;
        private readonly Database _Database;
        private ModelCommands _ModelCommands;

        // Properties for form fields

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CPRNumber { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string RoadName { get; set; }
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PersonalMail { get; set; }
        public string PersonalPhone { get; set; }

        private string _password;
        private string _reenterPassword;
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

        public ReactiveCommand<Unit, Unit> RegisterBTN { get; }
        public ReactiveCommand<Unit, Unit> BackToNormlogin { get; }

        public RegristorViewModel(MainWindowViewModel mainWindowViewModel, Database database, ModelCommands modCommands)
        {
            _MainWindowViewModel = mainWindowViewModel;
            _Database = database;
            _ModelCommands = modCommands;

            RegisterBTN = ReactiveCommand.Create(() =>
            {
                PersonaLInfo Pinfo = new(0,PersonalMail,PersonalPhoneCountryCode + PersonalPhone,Address,PostalCode,RoadName,HouseNumber,City,Country, CPRNumber);
                User user = new(0,Password,0,0,FirstName,LastName,Pinfo);
                modCommands.AddUser(database, mainWindowViewModel, modCommands,user,ReenterPassword);   
                
            });

            BackToNormlogin = ReactiveCommand.Create(() => modCommands.SwitchToNormLogin(database, mainWindowViewModel, modCommands));
        }

    }
}

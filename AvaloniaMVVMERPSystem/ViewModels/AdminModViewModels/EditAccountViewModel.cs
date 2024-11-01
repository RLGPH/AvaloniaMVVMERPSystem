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
    public class EditAccountViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _MainWindowViewModel;
        private readonly Database _Database;
        private ModelCommands _ModelCommands;
        private Employee _employee;

        // Properties for form fields
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ReenterPassword { get; set; }
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
        public string AdminPassword { get; set; }
        public string ReenterAdminPassword { get; set; }

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

        public ReactiveCommand<Unit, Unit> EditEmployee { get; }

        public EditAccountViewModel(MainWindowViewModel mainWindowViewModel, Database database, ModelCommands modCommands, Employee employee)
        {
            _MainWindowViewModel = mainWindowViewModel;
            _Database = database;
            _ModelCommands = modCommands;
            _employee = employee;
        }
    }
}

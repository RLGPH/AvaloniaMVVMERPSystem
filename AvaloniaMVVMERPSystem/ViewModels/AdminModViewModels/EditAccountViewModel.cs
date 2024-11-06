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
using Tmds.DBus.Protocol;

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

        public ReactiveCommand<Unit, Unit> BackToMenu { get; }

        public ReactiveCommand<Unit, Unit> EditEmployee { get; }

        public EditAccountViewModel(MainWindowViewModel mainWindowViewModel, Database database, ModelCommands modCommands, Employee employee)
        {
            _MainWindowViewModel = mainWindowViewModel;
            _Database = database;
            _ModelCommands = modCommands;
            _employee = employee;

            BackToMenu = ReactiveCommand.Create(() => modCommands.SwitchToAdminMenu(database, mainWindowViewModel, modCommands, employee));

            //the pre assigned data
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            Password = "*********";
            ReenterPassword = "*********";
            AdminPassword = "*********";
            ReenterAdminPassword = "*********";
            CPRNumber = employee.CPRNumber;
            PostalCode = employee.PInfo.PostalCode;
            Address = employee.PInfo.Address;
            RoadName = employee.PInfo.RoadName;
            HouseNumber = employee.PInfo.HouseNumber;
            City = employee.PInfo.City;
            Country = employee.PInfo.Country;
            PersonalMail = employee.PInfo.Mail;
            PersonalPhone = employee.PInfo.Tlf;
            WorkMail = employee.WorkMail;
            WorkPhone = employee.WorkTlf;
            Title = employee.Title;
            IsAdmin = employee._admin.IsAdmin;
            IsMod = employee._moderator.IsMod;

            PersonaLInfo info = new(employee.PInfo.PersonalInfoId, PersonalMail, PersonalPhone, Address, PostalCode, RoadName,
                HouseNumber, City, Country);

            Admin admin = new(employee._admin.AdminId, IsAdmin);

            Moderator mod = new(employee._moderator.ModeratorId, IsMod);

            Employee NewEmployee = new(employee.EmployeeId, employee.EmployeePassword, Title, WorkMail, WorkPhone, employee.AdminPassword, employee.PersonId
                , FirstName, LastName, employee.CPRNumber, info, admin, mod);

            EditEmployee = ReactiveCommand.Create(() => modCommands.EditAccount(NewEmployee, employee, database, mainWindowViewModel, modCommands));
        }
    }
}

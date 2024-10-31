using Avalonia.Controls.Documents;
using AvaloniaMVVMERPSystem.Classes;
using AvaloniaMVVMERPSystem.DataBase;
using AvaloniaMVVMERPSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMERPSystem.Models
{
    public partial class ModelCommands
    {
        public void AddEmployeeView(Database _Database, MainWindowViewModel _MainWindowViewModel, ModelCommands modelCommands, Employee employee)
        {
            _MainWindowViewModel.SwitchViewModel(new AddEmployeeViewModel(_MainWindowViewModel, _Database, modelCommands, employee));
        }

        public void AddEmployee(int employeeId, string emplyeePassword, string title, string workMail, string workTlf, string adminPassword, 
            int personId, string firstName, string lastName, string cprNumber, int personalInfoId, string mail, string tlf, string address, 
            string postalCode, string roadName, string houseNumber, string city, string country, int modId, bool isMod, int adminId, bool isAdmin,
            Database _Database, MainWindowViewModel _MainWindowViewModel, ModelCommands modelCommands, Employee employee)
        {
            PersonaLInfo info = new(personalInfoId,mail,tlf,address,postalCode,roadName,houseNumber,city,country);
            Admin admin = new(adminId,isAdmin);
            Moderator moderator = new(modId,isMod);

            adminPassword = PasswordHasher.HashPassword(adminPassword);
            emplyeePassword = PasswordHasher.HashPassword(emplyeePassword);
            
            Employee NewEmployee = new(employeeId, emplyeePassword, title, workMail, workTlf, adminPassword, personId, firstName, lastName, cprNumber, info, admin, moderator);
            _Database.CreateEmployee(NewEmployee);
        }
    }
}

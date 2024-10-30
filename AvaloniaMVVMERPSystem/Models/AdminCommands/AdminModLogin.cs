using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaMVVMERPSystem.Classes;
using AvaloniaMVVMERPSystem.DataBase;
using AvaloniaMVVMERPSystem.ViewModels;

namespace AvaloniaMVVMERPSystem.Models
{
    public partial class ModelCommands
    {
        public void LoginAuthentication (int Id ,string FirstName, string LastName, string password, string AdminPassWord, MainWindowViewModel mainWindowViewModel, Database database, ModelCommands modCommands)
        {

            Employee GetEmployee = database.GetEmployee(Id);

            GetEmployee.EmployeePassword = PasswordHasher.HashPassword(GetEmployee.EmployeePassword);
            if (GetEmployee.AdminPassword != null)
            {
                GetEmployee.AdminPassword = PasswordHasher.HashPassword(GetEmployee.AdminPassword);

                if (PasswordHasher.VerifyPassword(password, GetEmployee.EmployeePassword) && PasswordHasher.VerifyPassword(password, GetEmployee.EmployeePassword))
                {
                    LoginAsAdmin(database, mainWindowViewModel, modCommands, GetEmployee);
                }
            }
        }
    }
}

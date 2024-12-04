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

            if (GetEmployee.AdminPassword != null && GetEmployee.AdminPassword != "" && GetEmployee._admin.IsAdmin == true || GetEmployee._moderator.IsMod == true)
            {
                if (PasswordHasher.VerifyPassword(password, GetEmployee.EmployeePassword) && PasswordHasher.VerifyPassword(password, GetEmployee.EmployeePassword) &&  FirstName == GetEmployee.FirstName && LastName == GetEmployee.LastName)
                {
                    LoginAsAdmin(database, mainWindowViewModel, modCommands, GetEmployee);
                }
            }
        }
    }
}

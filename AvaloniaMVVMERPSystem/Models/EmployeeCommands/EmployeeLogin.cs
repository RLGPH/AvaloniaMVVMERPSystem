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
        public void EmployeeLogin(Database database, MainWindowViewModel mainWindowViewModel, ModelCommands modelCommands,string FirstName, string LastName, string Password,int EmployeeId)
        {
            Employee GetEmployee = database.GetEmployee(EmployeeId);
            if (GetEmployee.FirstName == FirstName && GetEmployee.LastName == LastName && PasswordHasher.VerifyPassword(Password,GetEmployee.EmployeePassword)) 
            {
                LoginAsEmployee(database, mainWindowViewModel, modelCommands, GetEmployee);
            }
        }
    }
}

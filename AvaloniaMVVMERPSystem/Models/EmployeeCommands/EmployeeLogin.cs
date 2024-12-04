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
        public void EmployeeLogin(Database database, MainWindowViewModel mainWindowViewModel ,string FirstName, string LastName, string Password,int EmployeeId)
        {
            Employee employee = database.GetEmployee(EmployeeId);
            if (employee.FirstName == FirstName && employee.LastName == LastName && PasswordHasher.VerifyPassword(Password,employee.EmployeePassword)) 
            {
                //LoginAsEmployee(database,mainWindowViewModel);
            }
        }
    }
}

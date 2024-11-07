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
        public void EditAccount(Employee changed , Employee employee, Database database, MainWindowViewModel mainWindowViewModel, ModelCommands modelCommands) 
        {
            employee = database.EditEmployee(changed, employee);

            SwitchToAdminMenu(database, mainWindowViewModel, modelCommands, employee);
        }
    }
}

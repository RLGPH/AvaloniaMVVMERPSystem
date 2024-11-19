using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
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

        public ObservableCollection<Employee> DeletEmployee(int id, Database dataBase)
        {
            ObservableCollection<Employee> employees = new();
            dataBase.DeleteEmployee(id);

            return employees = dataBase.GetAllEmployees();
        }
    }
}

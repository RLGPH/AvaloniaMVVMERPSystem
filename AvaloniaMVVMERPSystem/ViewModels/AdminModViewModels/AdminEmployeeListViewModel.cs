using AvaloniaMVVMERPSystem.Classes;
using AvaloniaMVVMERPSystem.DataBase;
using AvaloniaMVVMERPSystem.Models;
using CommunityToolkit.Mvvm.ComponentModel;
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
    public partial class AdminEmployeeListViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _MainWindowViewModel;
        private readonly Database _Database;
        private ModelCommands _ModelCommands;
        private Employee _employee;

        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get => _employees;
            set => this.RaiseAndSetIfChanged(ref _employees, value);
        }
        private Employee _selectedEmployee;

        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set => this.RaiseAndSetIfChanged(ref _selectedEmployee, value);
        }

        public ReactiveCommand<Unit, Unit> EditEmployee { get; }
        public ReactiveCommand<Unit, ObservableCollection<Employee>> RemoveEmployee { get; }
        public ReactiveCommand<Unit, Unit> BackToMain { get; }

        public AdminEmployeeListViewModel(MainWindowViewModel mainWindowViewModel, Database database, ModelCommands modCommands, Employee employee)
        {
            _MainWindowViewModel = mainWindowViewModel;
            _Database = database;
            _ModelCommands = modCommands;
            _employee = employee;

            _employees = database.GetAllEmployees();

            RemoveEmployee = ReactiveCommand.Create(() =>
            {
                
                if (SelectedEmployee != null)
                {
                    return modCommands.DeletEmployee(SelectedEmployee.EmployeeId, database);
                }
                return Employees;
            });

            RemoveEmployee.Subscribe(updatedEmployees => Employees = updatedEmployees);

            BackToMain = ReactiveCommand.Create(() => modCommands.SwitchToAdminMenu(database, mainWindowViewModel, modCommands, employee));

            EditEmployee = ReactiveCommand.Create(() => modCommands.SwitchToEditAccountA(database, employee, mainWindowViewModel, modCommands, SelectedEmployee));
        }       
    }
}

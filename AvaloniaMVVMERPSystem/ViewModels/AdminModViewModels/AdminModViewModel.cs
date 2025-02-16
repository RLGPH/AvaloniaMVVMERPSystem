﻿using AvaloniaMVVMERPSystem.DataBase;
using AvaloniaMVVMERPSystem.Models;
using ReactiveUI;
using System;
using AvaloniaMVVMERPSystem.Classes;
using System.Reactive;


namespace AvaloniaMVVMERPSystem.ViewModels
{
#pragma warning disable
    public class AdminModViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _MainWindowViewModel;
        private readonly Database _Database;
        private ModelCommands _ModelCommands;
        private Employee _employee;

        private string _firstName;
        private string _lastName;

        public string FirstName
        {
            get => _firstName;
            set => this.RaiseAndSetIfChanged(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => this.RaiseAndSetIfChanged(ref _lastName, value);
        }

        public ReactiveCommand<Unit, Unit> EditEmployee {  get; }
        public ReactiveCommand<Unit, Unit> AddEmployee { get; }
        public ReactiveCommand<Unit, Unit> BackToNormlogin { get; }
        public ReactiveCommand<Unit, Unit> EditAccount { get; }
        public ReactiveCommand<Unit,Unit> AddInventory {  get; }

        public AdminModViewModel(MainWindowViewModel mainWindowViewModel, Database database, ModelCommands modCommands, Employee employee)
        {
            _MainWindowViewModel = mainWindowViewModel;
            _Database = database;
            _ModelCommands = modCommands;
            _employee = employee;

            FirstName = employee.FirstName;
            LastName = employee.LastName;

            EditEmployee = ReactiveCommand.Create(() => modCommands.SwitchToAdminEmployeeList(database, employee, mainWindowViewModel, modCommands));

            EditAccount = ReactiveCommand.Create(() => modCommands.SwitchToEditAccountA(database, employee, mainWindowViewModel, modCommands, employee));

            AddEmployee = ReactiveCommand.Create(() => modCommands.AddEmployeeView(database,mainWindowViewModel,modCommands,employee));

            BackToNormlogin = ReactiveCommand.Create(() => modCommands.SwitchToNormLogin(database, mainWindowViewModel, modCommands));

            AddInventory = ReactiveCommand.Create(() => modCommands.SwitchToAddInventory(database, mainWindowViewModel, modCommands, employee));
        }
    }
}

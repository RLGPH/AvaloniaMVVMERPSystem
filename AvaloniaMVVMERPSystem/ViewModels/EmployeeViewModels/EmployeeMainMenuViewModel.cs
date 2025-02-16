﻿using AvaloniaMVVMERPSystem.Classes;
using AvaloniaMVVMERPSystem.DataBase;
using AvaloniaMVVMERPSystem.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMERPSystem.ViewModels
{
#pragma warning disable
    public class EmployeeMainMenuViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _MainWindowViewModel;
        private readonly Database _Database;
        private ModelCommands _ModelCommands;
        private Employee _Employee;

        public ReactiveCommand<Unit, Unit> EditAccount { get; }
        public ReactiveCommand<Unit, Unit> InventoryHistory { get; }
        public ReactiveCommand<Unit, Unit> EditInventory { get; }
        public ReactiveCommand<Unit, Unit> AddToInventory { get; }
        public ReactiveCommand<Unit, Unit> BackToNormlogin { get; }

        public EmployeeMainMenuViewModel(MainWindowViewModel mainWindowViewModel, Database database, ModelCommands modCommands, Employee employee)
        {
            _MainWindowViewModel = mainWindowViewModel;
            _Database = database;
            _ModelCommands = modCommands;
            _Employee = employee;

            AddToInventory = ReactiveCommand.Create(() => modCommands.SwitchToAddInventory(database, mainWindowViewModel, modCommands, employee));

            BackToNormlogin = ReactiveCommand.Create(() => modCommands.SwitchToNormLogin(database, mainWindowViewModel, modCommands));

            EditAccount = ReactiveCommand.Create(() => modCommands.SwitchToEditAccountA(_Database, _Employee, mainWindowViewModel,modCommands, employee));

            EditInventory = ReactiveCommand.Create(() => modCommands.SwitchToEditInventory(database, mainWindowViewModel, modCommands, employee));
        }
    }
}


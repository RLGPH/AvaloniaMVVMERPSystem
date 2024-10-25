using AvaloniaMVVMERPSystem.DataBase;
using AvaloniaMVVMERPSystem.Models;
using ReactiveUI;
using System;
using System.Reactive;

namespace AvaloniaMVVMERPSystem.ViewModels
{
    public class RegristorViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _MainWindowViewModel;
        private readonly Database _Database;
        private ModelCommands _ModelCommands;

        public ReactiveCommand<Unit, Unit> BackToNormlogin { get; }

        public RegristorViewModel(MainWindowViewModel mainWindowViewModel, Database database, ModelCommands modCommands)
        {
            _MainWindowViewModel = mainWindowViewModel;
            _Database = database;
            _ModelCommands = modCommands;

            BackToNormlogin = ReactiveCommand.Create(() => modCommands.SwitchToNormLogin(database, mainWindowViewModel, modCommands));
        }

    }
}

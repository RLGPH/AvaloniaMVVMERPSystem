using AvaloniaMVVMERPSystem.DataBase;
using ReactiveUI;
using System.Reactive;
using AvaloniaMVVMERPSystem.Models;

namespace AvaloniaMVVMERPSystem.ViewModels
{
    public class EmployeeLoginViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _MainWindowViewModel;
        private readonly Database _Database;
        private ModelCommands _ModelCommands;

        public ReactiveCommand<Unit, Unit> BackToNormlogin { get; }

        public EmployeeLoginViewModel(MainWindowViewModel mainWindowViewModel, Database database, ModelCommands modCommands)
        {
            _MainWindowViewModel = mainWindowViewModel;
            _Database = database;
            _ModelCommands = modCommands;

            BackToNormlogin = ReactiveCommand.Create(() => modCommands.SwitchToNormLogin(database, mainWindowViewModel, modCommands));
        }
    }
}

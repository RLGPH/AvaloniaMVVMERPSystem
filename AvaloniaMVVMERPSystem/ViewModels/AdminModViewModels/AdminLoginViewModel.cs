using AvaloniaMVVMERPSystem.DataBase;
using ReactiveUI;
using AvaloniaMVVMERPSystem.Models;
using System.Reactive;


namespace AvaloniaMVVMERPSystem.ViewModels
{
    public class AdminLoginViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _MainWindowViewModel;
        private readonly Database _Database;
        private ModelCommands _modelCommands;

        public ReactiveCommand<Unit, Unit> BackToNormlogin { get; }

        public AdminLoginViewModel(MainWindowViewModel mainWindowViewModel, Database database, ModelCommands modCommands)
        {
            _MainWindowViewModel = mainWindowViewModel;
            _Database = database;
            _modelCommands = modCommands;

            BackToNormlogin = ReactiveCommand.Create(() => modCommands.SwitchToNormLogin(database, mainWindowViewModel, modCommands));
        }
    }
}

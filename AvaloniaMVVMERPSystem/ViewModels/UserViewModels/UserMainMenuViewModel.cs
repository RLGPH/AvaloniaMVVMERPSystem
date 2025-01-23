using AvaloniaMVVMERPSystem.Classes;
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
    public class UserMainMenuViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _MainWindowViewModel;
        private readonly Database _Database;
        private ModelCommands _ModelCommands;
        private User _user;

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

        public ReactiveCommand<Unit, Unit> EditAccount { get; }
        public ReactiveCommand<Unit, Unit> OrderHistory { get; }
        public ReactiveCommand<Unit,Unit> OrderBTN { get; }
        public ReactiveCommand<Unit,Unit> LogoutBTN { get; }

        public UserMainMenuViewModel(MainWindowViewModel mainWindowViewModel, Database database, ModelCommands modCommands, User user)
        {
            _MainWindowViewModel = mainWindowViewModel;
            _Database = database;
            _ModelCommands = modCommands;
            _user = user;

            FirstName = user.FirstName;
            LastName = user.LastName;

            EditAccount = ReactiveCommand.Create(() => modCommands.SwitchToUserEdit(database,mainWindowViewModel,modCommands,user));
            LogoutBTN = ReactiveCommand.Create(() => modCommands.SwitchToNormLogin(database, mainWindowViewModel, modCommands));
        }
    }
}

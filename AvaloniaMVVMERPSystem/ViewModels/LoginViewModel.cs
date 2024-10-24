using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaMVVMERPSystem.DataBase;


namespace AvaloniaMVVMERPSystem.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private MainWindowViewModel _MainWindowViewModel;
        private Database _Database;

        public LoginViewModel(MainWindowViewModel mainWindowViewModel, Database database)
        {
            _MainWindowViewModel = mainWindowViewModel;
            _Database = database;
        }
    }
}

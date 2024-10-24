using AvaloniaMVVMERPSystem.DataBase;
using AvaloniaMVVMERPSystem.ViewModels;
using System;


namespace AvaloniaMVVMERPSystem.Models
{
    public partial class ModelCommands
    {
        public void SwitchToNormLogin(Database database, MainWindowViewModel _MainWindowViewModel,ModelCommands modelCommands)
        {
            _MainWindowViewModel.SwitchViewModel(new LoginViewModel(_MainWindowViewModel, database, modelCommands));
        }
    }
}

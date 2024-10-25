using AvaloniaMVVMERPSystem.DataBase;
using AvaloniaMVVMERPSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMERPSystem.Models
{
    public partial class ModelCommands
    {
        public void SwitchToRegristor(Database _Database, MainWindowViewModel _MainWindowViewModel, ModelCommands modelCommands)
        {
            _MainWindowViewModel.SwitchViewModel(new RegristorViewModel(_MainWindowViewModel, _Database, modelCommands));
        }
    }
}

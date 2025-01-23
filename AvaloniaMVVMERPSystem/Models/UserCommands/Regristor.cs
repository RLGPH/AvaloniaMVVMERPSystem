using AvaloniaMVVMERPSystem.Classes;
using AvaloniaMVVMERPSystem.DataBase;
using AvaloniaMVVMERPSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public void AddUser(Database database, MainWindowViewModel _MainWindowViewModel, ModelCommands modelCommands, User user, string ReenterPass)
        {
            if(user.UPassWord != null && user.UPassWord == ReenterPass)
            {
                user.UPassWord = Classes.PasswordHasher.HashPassword(user.UPassWord);

                database.AddUser(user);

                SwitchToNormLogin(database, _MainWindowViewModel, modelCommands);
            }
        }
    }
}

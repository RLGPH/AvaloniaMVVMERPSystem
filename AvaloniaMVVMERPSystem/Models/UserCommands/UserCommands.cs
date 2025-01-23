using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaMVVMERPSystem.DataBase;
using AvaloniaMVVMERPSystem.Classes;
using AvaloniaMVVMERPSystem.ViewModels;
using Tmds.DBus.Protocol;
using System.Reflection;
using System.Runtime.Intrinsics.Arm;

namespace AvaloniaMVVMERPSystem.Models
{
    public partial class ModelCommands
    {
        public void SwitchToUserMainPage(string First, string LastName, string password, Database database, MainWindowViewModel _MainWindowViewModel, ModelCommands modelCommands) 
        {
            User user = database.GetUserByName(First, LastName);
            if (user != null)
            {
                if(PasswordHasher.VerifyPassword(password, user.UPassWord))
                {
                    _MainWindowViewModel.SwitchViewModel(new UserMainMenuViewModel(_MainWindowViewModel, database, modelCommands, user));
                }
            }
        }

        public string SaveUserEdit(string FirstName, string LastName, string CPRNumber, string PostalCode, string Address, 
            string RoadName, string HouseNumber, string City, string Country, string PersonalPhone, string PersonalMail, 
            float Balance,string Password, string ReenterPassword, string OldPassword, User user, Database database)
        {
            if (OldPassword != null && PasswordHasher.VerifyPassword(OldPassword, user.UPassWord))
            {
                if (Password == ReenterPassword)
                {
                    user.UPassWord = PasswordHasher.HashPassword(Password);
                    user.FirstName = FirstName;
                    user.LastName = LastName;
                    user.Balance = Balance;
                    user.PInfo.Address = Address;
                    user.PInfo.RoadName = RoadName;
                    user.PInfo.HouseNumber = HouseNumber;
                    user.PInfo.City = City;
                    user.PInfo.Country = Country;
                    user.PInfo.Tlf = PersonalPhone;
                    user.PInfo.Mail = PersonalMail;
                    user.PInfo.CPRNumber = CPRNumber;
                    user.PInfo.PostalCode = PostalCode;

                    database.UpdateUser(user);
                    return "succesfully updated password";
                }
                return "unsuccesfully updated password";
            }
            else 
            {
                user.FirstName = FirstName;
                user.LastName = LastName;
                user.Balance = Balance;
                user.PInfo.Address = Address;
                user.PInfo.RoadName = RoadName;
                user.PInfo.HouseNumber = HouseNumber;
                user.PInfo.City = City;
                user.PInfo.Country = Country;
                user.PInfo.Tlf = PersonalPhone;
                user.PInfo.Mail = PersonalMail;
                user.PInfo.CPRNumber = CPRNumber;
                user.PInfo.PostalCode = PostalCode;

                database.UpdateUser(user);

                return "unsuccesfully updated password \n" +
                       "but succesfully updated anything else changed";
            }
        }
    }
}

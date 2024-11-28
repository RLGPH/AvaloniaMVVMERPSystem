using Avalonia.Controls;
using Avalonia.Markup.Xaml.Templates;
using AvaloniaMVVMERPSystem.Classes;
using AvaloniaMVVMERPSystem.DataBase;
using AvaloniaMVVMERPSystem.Models;
using DynamicData;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMERPSystem.ViewModels
{
    public class AddInventoryViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _MainWindowViewModel;
        private readonly Database _Database;
        private readonly ModelCommands _ModelCommands;
        private readonly Employee _employee;

        // Properties
        private string _itemName;
        private string _itemDescription;
        private float _storageSpaceNeeded;
        private ObservableCollection<Classes.Location> _locationList;
        private Classes.Location _selectedLocation;

        public string Status { get; set; }
        public string LocationName { get; set; }
        public string LCountry { get; set; }
        public string LCity { get; set; }
        public string LStreet { get; set; }
        public string LZipCode { get; set; }
        public float StorageSpaceLeft { get; set; }

        public string ItemName
        {
            get => _itemName;
            set => this.RaiseAndSetIfChanged(ref _itemName, value);
        }

        public string ItemDescription
        {
            get => _itemDescription;
            set => this.RaiseAndSetIfChanged(ref _itemDescription, value);
        }

        public float StorageSpaceNeeded
        {
            get => _storageSpaceNeeded;
            set => this.RaiseAndSetIfChanged(ref _storageSpaceNeeded, value);
        }

        public ObservableCollection<Classes.Location> LocationList
        {
            get => _locationList;
            set => this.RaiseAndSetIfChanged(ref _locationList, value);
        }

        public Classes.Location SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedLocation, value);
                if (value != null)
                {
                    // Update fields based on the selected location
                    LCountry = value.LCountry;
                    LCity = value.LCity;
                    LStreet = value.LStreet;
                    LZipCode = value.LZipCode;
                    StorageSpaceLeft = value.StorageSpaceLeft;
                    this.RaisePropertyChanged(nameof(LCountry));
                    this.RaisePropertyChanged(nameof(LCity));
                    this.RaisePropertyChanged(nameof(LStreet));
                    this.RaisePropertyChanged(nameof(LZipCode));
                    this.RaisePropertyChanged(nameof(StorageSpaceLeft));
                }
            }
        }
        public bool IsPopupOpen { get; set; }
        public string AdminPassword { get; set; }
        public string ReenteredPassword { get; set; }

        // Commands
        public ReactiveCommand<Unit, Unit> ContinueCommand { get; }
        public ReactiveCommand<Unit, Unit> CancelCommand { get; }
        public ReactiveCommand<Unit, Unit> AddLocation { get; set; }
        public ReactiveCommand<Unit, Unit> AddItem { get; set; }
        public ReactiveCommand<Unit, Unit> Back { get; set; }
        public ReactiveCommand<Unit, Unit> Check { get; set; }
        public ReactiveCommand<Unit, Unit> BackAdmin { get; set; }

        public AddInventoryViewModel(MainWindowViewModel mainWindowViewModel, Database database, ModelCommands modCommands, Employee employee)
        {
            _MainWindowViewModel = mainWindowViewModel;
            _Database = database;
            _ModelCommands = modCommands;
            _employee = employee;
            bool ContinueOption = false;

            // Load locations
            LocationList = new ObservableCollection<Classes.Location>();

            Classes.Location location = new(0, "New", "New", "New", "New", "New", 0);

            LocationList.Add(location);
            LocationList.Add(database.GetLocations());


            // Initialize commands
            AddItem = ReactiveCommand.Create(() =>
            {
                if (SelectedLocation == null)
                {
                    Status = "Please select a location.";
                    return;
                }

                if (string.IsNullOrWhiteSpace(ItemName) || string.IsNullOrWhiteSpace(ItemDescription))
                {
                    Status = "Item name and description cannot be empty.";
                    return;
                }

                Status = modCommands.AddInventory(SelectedLocation, ItemName, ItemDescription, database);
            });

            AddLocation = ReactiveCommand.Create(() => { IsPopupOpen = true; ContinueOption = false; });

            BackAdmin = ReactiveCommand.Create(() => { IsPopupOpen = true; ContinueOption = true; });

            ContinueCommand = ReactiveCommand.Create(() => 
            {
                if (AdminPassword == ReenteredPassword && PasswordHasher.VerifyPassword(AdminPassword, employee.AdminPassword))
                {
                    if (ContinueOption == true)
                    {
                        modCommands.SwitchToAdminMenu(database, mainWindowViewModel, modCommands, employee);
                    }
                    else if (ContinueOption == false && location != null)
                    {
                        modCommands.AddLocation(database);
                    }
                }
            });

            // Default Status
            Status = "N/A";
        }
    }
}

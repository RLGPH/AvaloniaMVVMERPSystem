using Avalonia.Controls;
using AvaloniaMVVMERPSystem.Classes;
using AvaloniaMVVMERPSystem.DataBase;
using AvaloniaMVVMERPSystem.Models;
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
    public class EditInventoryViewModel : ViewModelBase
    {
        private readonly Database _Database;
        private readonly ModelCommands _ModelCommands;
        private readonly MainWindowViewModel _MainWindowViewModel;
        private readonly Employee _employee;

        // Properties
        private string _itemName;
        private string _itemDescription;
        private float _storageSpaceNeeded;
        private ObservableCollection<Classes.Location> _locationList;
        private Classes.Location _selectedLocation;

        public ObservableCollection<CombinedItemLocation> CombinedItemLocations { get; set; }

        public string Status { get; set; }

        //used in showing information about location and in adding a new location 
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
        //back to AdminModView used to confirm they are admin
        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => this.RaiseAndSetIfChanged(ref _isPopupOpen, value);
        }

        public string AdminPassword { get; set; }
        public string ReenteredPassword { get; set; }

        private bool _isPopupOpenLocation;
        public bool IsPopupOpenLocation
        {
            get => _isPopupOpenLocation;
            set => this.RaiseAndSetIfChanged(ref _isPopupOpenLocation, value);
        }

        public ObservableCollection<CombinedItemLocation> CombinedItemL { get; set; }


        //button Commands
        public ReactiveCommand<Unit, Unit> ContinueCommand { get; }
        public ReactiveCommand<Unit, Unit> CancelCommand { get; }
        public ReactiveCommand<Unit, Unit> AddLocation { get; set; }
        public ReactiveCommand<Unit, Unit> AddItem { get; set; }
        public ReactiveCommand<Unit, Unit> Back { get; set; }
        public ReactiveCommand<Unit, Unit> Check { get; set; }
        public ReactiveCommand<Unit, Unit> BackAdmin { get; set; }

        // Property to hold the selected item
        public CombinedItemLocation SelectedItem { get; private set; }

        // Command to handle SelectionChanged
        public ReactiveCommand<SelectionChangedEventArgs, Unit> SelectedItemChanged { get; set; }

        public EditInventoryViewModel(MainWindowViewModel mainWindowViewModel, Database database, ModelCommands modCommands, Employee employee)
        {
            _Database = database;
            _MainWindowViewModel = mainWindowViewModel;
            _ModelCommands = modCommands;
            _employee = employee;
            bool ContinueOption = false;

            CombinedItemL = new(modCommands.GetCombinedLocations(database));

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

                Status = modCommands.AddInventory(SelectedLocation, ItemName, ItemDescription, database, StorageSpaceNeeded);
            });

            AddLocation = ReactiveCommand.Create(() => { IsPopupOpenLocation = true; ContinueOption = false; });

            BackAdmin = ReactiveCommand.Create(() => { IsPopupOpen = true; ContinueOption = true; });

            ContinueCommand = ReactiveCommand.Create(() =>
            {
                if (AdminPassword == ReenteredPassword && PasswordHasher.VerifyPassword(AdminPassword, employee.AdminPassword))
                {
                    if (ContinueOption == true)
                    {
                        modCommands.SwitchToAdminMenu(database, mainWindowViewModel, modCommands, employee);
                        IsPopupOpen = false;
                    }
                    else if (ContinueOption == false)
                    {
                        Status = modCommands.AddLocation(database, LocationName, LCountry, LCity, LStreet, LZipCode, StorageSpaceLeft);
                        IsPopupOpenLocation = false;
                    }
                }
            });

            CancelCommand = ReactiveCommand.Create(() => { IsPopupOpenLocation = false; IsPopupOpen = false; });

            SelectedItemChanged = ReactiveCommand.Create<SelectionChangedEventArgs>(OnSelectionChanged);
        }
        private void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            // Get the first selected item from the event args
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is CombinedItemLocation selectedItem)
            {
               ItemName = selectedItem.item.Name;
            }

        }
    }
}

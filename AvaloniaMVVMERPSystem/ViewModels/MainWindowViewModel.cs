﻿using AvaloniaMVVMERPSystem.DataBase;
using AvaloniaMVVMERPSystem.Models;
using ReactiveUI;
using System;

namespace AvaloniaMVVMERPSystem.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set => this.RaiseAndSetIfChanged(ref _currentViewModel, value);
        }

        public MainWindowViewModel()
        {
            CurrentViewModel = new LoginViewModel(this, new Database(),new ModelCommands());
        }

        public void SwitchViewModel(ViewModelBase viewModel)
        {
            CurrentViewModel = viewModel;
        }
    }
}
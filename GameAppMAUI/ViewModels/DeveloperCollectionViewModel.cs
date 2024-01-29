using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Globalization;
using GameApp.Core;
using Wojtalak_Szczerkowski.GameApp.Interfaces;
using Wojtalak_Szczerkowski.GameApp.BLC;
using IDeveloper = Wojtalak_Szczerkowski.GameApp.Interfaces.IDeveloper;

namespace GameAppMAUI.ViewModels
{
    public partial class DeveloperCollectionViewModel : ObservableObject
    {
        private BLC _blc;


        [ObservableProperty]
        private ObservableCollection<IDeveloper> developers;

        public DeveloperCollectionViewModel(BLC blc)
        {
            _blc = blc;
            developers = new ObservableCollection<IDeveloper>();
            foreach (IDeveloper developer in _blc.GetAllDevelopers())
            {
                developers.Add(new DeveloperViewModel(developer));
            }

            CancelCommand = new Command(
                execute: () =>
                {
                    DeveloperEdit.PropertyChanged -= OnDeveloperEditPropertyChanged;
                    DeveloperEdit = null;
                    IsEditing = false;
                    IsAdding = false;
                    RefreshCanExecute();
                    Shell.Current.GoToAsync("..");
                },
                canExecute: () =>
                {
                    return IsEditing || IsAdding;
                });
        }

        [ObservableProperty]
        private DeveloperViewModel developerEdit;

        [ObservableProperty]
        private bool isEditing;

        [ObservableProperty]
        private bool isAdding;

        [RelayCommand(CanExecute = nameof(CanCreateNewDeveloper))]
        private void CreateNewDeveloper()
        {
            DeveloperEdit = new DeveloperViewModel();
            DeveloperEdit.PropertyChanged += OnDeveloperEditPropertyChanged;
            IsAdding = true;
            RefreshCanExecute();
            Shell.Current.GoToAsync(nameof(DeveloperAddPage));
        }

        private bool CanCreateNewDeveloper()
        {
            return !IsAdding;
        }

        [RelayCommand(CanExecute = nameof(CanEditDeveloperBeSaved))]
        private void SaveDeveloper()
        {
            if (IsAdding)
            {
                _blc.AddDeveloper(DeveloperEdit);
                Developers.Add(DeveloperEdit);
                IsAdding = false;
            }
            else if (IsEditing)
            {
                _blc.UpdateDeveloper(DeveloperEdit);
                IsEditing = false;
            }
            DeveloperEdit.PropertyChanged -= OnDeveloperEditPropertyChanged;
            DeveloperEdit = null;
            RefreshCanExecute();
            Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private void DeleteDeveloper()
        {
            IDeveloper itemToRemove = Developers.FirstOrDefault(c => c.Id == DeveloperEdit.Id);
            if (itemToRemove != null)
            {
                _blc.DeleteDeveloper(DeveloperEdit.Id);
                Developers.Remove(itemToRemove);
            }
            DeveloperEdit.PropertyChanged -= OnDeveloperEditPropertyChanged;
            DeveloperEdit = null;
            IsEditing = false;
            RefreshCanExecute();
            Shell.Current.GoToAsync("..");
        }

        private bool CanEditDeveloperBeSaved()
        {
            return this.DeveloperEdit != null &&
                   DeveloperEdit.Name != null &&
                   DeveloperEdit.Name.Length > 1 &&
                   DeveloperEdit.Country != null &&
                   DeveloperEdit.Country.Length > 1;
        }

        public void OnSelectedDeveloper(ItemTappedEventArgs args)
        {
            DeveloperEdit = args.Item as DeveloperViewModel;
            DeveloperEdit.PropertyChanged += OnDeveloperEditPropertyChanged;
            IsEditing = true;

            RefreshCanExecute();
            Shell.Current.GoToAsync(nameof(DeveloperEditPage));
        }

        private void RefreshCanExecute()
        {
            //(CreateNewProducerCommand as Command)?.ChangeCanExecute();
            CreateNewDeveloperCommand.NotifyCanExecuteChanged();
            //(SaveProducerCommand as Command)?.ChangeCanExecute();
            SaveDeveloperCommand.NotifyCanExecuteChanged();
            (CancelCommand as Command)?.ChangeCanExecute();
        }

        void OnDeveloperEditPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            SaveDeveloperCommand.NotifyCanExecuteChanged();
        }

        public ICommand CancelCommand { get; set; }
    }
}

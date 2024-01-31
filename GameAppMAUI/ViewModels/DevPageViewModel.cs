
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Wojtalak_Szczerkowski.GameApp.Interfaces;
using Wojtalak_Szczerkowski.GameApp.BLC;


namespace GameAppMAUI.ViewModels
{
    public partial class DevPageViewModel : ObservableObject
    {
        private BLC BLCinstance;


        [ObservableProperty]
        private ObservableCollection<IDeveloper> developers;

        public DevPageViewModel(BLC blc)
        {
            BLCinstance = blc;
            developers = new ObservableCollection<IDeveloper>();
            foreach (IDeveloper developer in BLCinstance.GetDevs())
            {
                developers.Add(new DevViewModel(developer));
            }

            CancelCommand = new Command(
                execute: () =>
                {
                    DeveloperEdit.PropertyChanged -= DevPropertyChanged;
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
        private string filterText;
        public string FilterText
        {
            get => filterText;
            set
            {
                filterText = value;
                FilterDevs();
                OnPropertyChanged(nameof(FilterText));
            }
        }

        [ObservableProperty]
        private DevViewModel developerEdit;

        [ObservableProperty]
        private bool isEditing;

        [ObservableProperty]
        private bool isAdding;

        [RelayCommand(CanExecute = nameof(CanAddDev))]
        private void CreateDev()
        {
            DeveloperEdit = new DevViewModel();
            DeveloperEdit.PropertyChanged += DevPropertyChanged;
            IsAdding = true;
            RefreshCanExecute();
            Shell.Current.GoToAsync(nameof(DeveloperAddPage));
        }

        [RelayCommand]
        public void FilterDevs()
        {
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                developers.Clear();
                foreach (IDeveloper dev in BLCinstance.GetDevs())
                {
                    developers.Add(new DevViewModel(dev));
                }
            }
            else
            {
                for (int i = developers.Count - 1; i >= 0; i--)
                {
                    if (!developers[i].Name.ToLower().Contains(FilterText.ToLower()))
                    {
                        developers.RemoveAt(i);
                    }
                }
            }
        }

        private bool CanAddDev()
        {
            return !IsAdding;
        }

        [RelayCommand(CanExecute = nameof(CanSaveChanges))]
        private void SaveDev()
        {
            if (IsAdding)
            {
                IsAdding = false;
                Developers.Add(DeveloperEdit);
                BLCinstance.AddDev(DeveloperEdit);
                
            }
            else if (IsEditing)
            {
                IsEditing = false;
                BLCinstance.ChangeDev(DeveloperEdit);
                
            }
            DeveloperEdit.PropertyChanged -= DevPropertyChanged;
            DeveloperEdit = null;
            RefreshCanExecute();
            Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private void RemoveDev()
        {
            IDeveloper selecteditem = Developers.FirstOrDefault(d => d.Id == DeveloperEdit.Id);
            if (selecteditem != null)
            {
                BLCinstance.RemoveDev(DeveloperEdit.Id);
                Developers.Remove(selecteditem);
            }
            IsEditing = false;
            DeveloperEdit.PropertyChanged -= DevPropertyChanged;
            DeveloperEdit = null;
            RefreshCanExecute();
            Shell.Current.GoToAsync("..");
        }

        private bool CanSaveChanges()
        {
            return DeveloperEdit != null && DeveloperEdit.Name != null && DeveloperEdit.Name.Length > 0 && DeveloperEdit.Country != null && DeveloperEdit.Country.Length > 0;
        }
        public void EditRedirect(ItemTappedEventArgs args)
        {
            DeveloperEdit = args.Item as DevViewModel;
            DeveloperEdit.PropertyChanged += DevPropertyChanged;
            IsEditing = true;
            RefreshCanExecute();
            Shell.Current.GoToAsync(nameof(DeveloperEditPage));
        }

        private void RefreshCanExecute()
        {
            CreateDevCommand.NotifyCanExecuteChanged();
            SaveDevCommand.NotifyCanExecuteChanged();
            (CancelCommand as Command)?.ChangeCanExecute();
        }
        void DevPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            SaveDevCommand.NotifyCanExecuteChanged();
        }

        public ICommand CancelCommand { get; set; }
    }
}

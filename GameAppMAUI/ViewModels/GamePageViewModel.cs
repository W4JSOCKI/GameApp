using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Globalization;
using GameApp.Core;
using Wojtalak_Szczerkowski.GameApp.Interfaces;
using Wojtalak_Szczerkowski.GameApp.BLC;



namespace GameAppMAUI.ViewModels
{
    public partial class GamePageViewModel : ObservableObject, INotifyPropertyChanged
    {
        private readonly BLC BLCinstance;
        [ObservableProperty]
        private ObservableCollection<IGame> games;


        private string filterText;
        public string FilterText
        {
            get => filterText;
            set
            {
                filterText = value;
                FilterGames();
                OnPropertyChanged(nameof(FilterText));
            }
        }

        public GamePageViewModel(BLC blc)
        {
            BLCinstance = blc;
            games = new ObservableCollection<IGame>();

         

            foreach (IGame game in BLCinstance.GetGames())
            {
                games.Add(new GameViewModel(game));   
            }

            CancelCommand = new Command(
             execute: () =>
             {
                 GameEdit.PropertyChanged -= OnGameEditPropertyChanged;
                 GameEdit = null;
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
        private GameViewModel gameEdit;

        [ObservableProperty]
        private bool isEditing;

        [ObservableProperty]
        private bool isAdding;
        private bool CanCreateNewGame()
        {
            return !IsAdding;
        }

        [RelayCommand]
        public void FilterGames()
        {
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                games.Clear();
                foreach (IGame game in BLCinstance.GetGames())
                {
                    games.Add(new GameViewModel(game));
                }
            }
            else
            {
                for (int i = games.Count - 1; i >= 0; i--)
                {
                    if (!games[i].Title.ToLower().Contains(FilterText.ToLower()))
                    {
                        games.RemoveAt(i);
                    }
                }
            }
        }

        public List<IDeveloper> DevList
        {
            get
            {
                return BLCinstance.GetDevs().ToList();
            }
        }

        public List<string> GenreGames
        {
            get {return Enum.GetNames(typeof(Genre)).ToList();}
        }

        

        [RelayCommand(CanExecute = nameof(CanCreateNewGame))]
        private void CreateNewGame()
        {
            OnPropertyChanged(nameof(DevList));
            GameEdit = new GameViewModel();
            GameEdit.PropertyChanged += OnGameEditPropertyChanged;
            IsAdding = true;
            RefreshCanExecute();
            Shell.Current.GoToAsync(nameof(GameAddPage));
        }

        [RelayCommand(CanExecute = nameof(CanEditGameBeSaved))]
        private void SaveGame()
        {
            if (IsAdding)
            {
                BLCinstance.AddGame(GameEdit);
                Games.Add(GameEdit);
                IsAdding = false;
            }
            else if (IsEditing)
            {
                BLCinstance.ChangeGame(GameEdit);
                IsEditing = false;
            }
            GameEdit.PropertyChanged -= OnGameEditPropertyChanged;
            GameEdit = null;
            RefreshCanExecute();
            Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private void DeleteGame()
        {
            IGame itemToRemove = Games.FirstOrDefault(g => g.Id == GameEdit.Id);
            if (itemToRemove != null)
            {
                BLCinstance.RemoveGame(GameEdit.Id);
                Games.Remove(itemToRemove);
            }
            GameEdit.PropertyChanged -= OnGameEditPropertyChanged;
            GameEdit = null;
            IsEditing = false;
            RefreshCanExecute();
            Shell.Current.GoToAsync("..");
        }

        private bool CanEditGameBeSaved()
        {
            return GameEdit != null &&
                   GameEdit.Title != null &&
                   GameEdit.Title.Length > 3 &&
                   GameEdit.ReleaseYear > 1950;
        }


        public void EditRedirect(ItemTappedEventArgs args)
        {
            OnPropertyChanged(nameof(DevList));
            GameEdit = args.Item as GameViewModel;
            GameEdit.PropertyChanged += OnGameEditPropertyChanged;
            IsEditing = true;

            RefreshCanExecute();
            Shell.Current.GoToAsync(nameof(GameEditPage));
        }


        private void RefreshCanExecute()
        {
           
            CreateNewGameCommand.NotifyCanExecuteChanged();
            SaveGameCommand.NotifyCanExecuteChanged();
            (CancelCommand as Command)?.ChangeCanExecute();
        }

        void OnGameEditPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            SaveGameCommand.NotifyCanExecuteChanged();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand CancelCommand { get; set; }
    }

    public class GameGenreToIntConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            Genre gameType = (Genre)value;
            int result = (int)gameType;
            return result;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            int val = (int)value;
            if (val != -1)
                return (Genre)value;
            else
                return 0;
        }
    }

}
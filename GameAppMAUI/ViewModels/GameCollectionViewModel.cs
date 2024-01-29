using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using IGame = Wojtalak_Szczerkowski.GameApp.Interfaces.IGame;


namespace GameAppMAUI.ViewModels
{
    public partial class GameCollectionViewModel : ObservableObject, INotifyPropertyChanged
    {
        private readonly BLC _blc;

        [ObservableProperty]
        private ObservableCollection<IGame> games;


        public GameCollectionViewModel(BLC blc)
        {
            _blc = blc;
            games = new ObservableCollection<IGame>();
            foreach (IGame game in _blc.GetAllGames())
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
        public List<IDeveloper> DevelopersList
        {
            get
            {
                return _blc.GetAllDevelopers().ToList();
            }
        }

        public List<string> GenreGames
        {
            get
            {
                return Enum.GetNames(typeof(Genre)).ToList();
            }
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

        [RelayCommand(CanExecute = nameof(CanCreateNewGame))]
        private void CreateNewGame()
        {
            OnPropertyChanged(nameof(DevelopersList));
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
                _blc.AddGame(GameEdit);
                Games.Add(GameEdit);
                IsAdding = false;
            }
            else if (IsEditing)
            {
                _blc.UpdateGame(GameEdit);
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
            IGame itemToRemove = Games.FirstOrDefault(c => c.Id == GameEdit.Id);
            if (itemToRemove != null)
            {
                _blc.DeleteGame(GameEdit.Id);
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
                   GameEdit.Title.Length > 1 &&
                   GameEdit.ReleaseYear > 1900;
        }


        public void OnSelectedGame(ItemTappedEventArgs args)
        {
            OnPropertyChanged(nameof(DevelopersList));
            GameEdit = args.Item as GameViewModel;
            GameEdit.PropertyChanged += OnGameEditPropertyChanged;
            IsEditing = true;

            RefreshCanExecute();
            Shell.Current.GoToAsync(nameof(GameEditPage));
        }


        private void RefreshCanExecute()
        {
            //(CreateNewContainerCommand as Command)?.ChangeCanExecute();
            CreateNewGameCommand.NotifyCanExecuteChanged();
            //(SaveContainerCommand as Command)?.ChangeCanExecute();
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
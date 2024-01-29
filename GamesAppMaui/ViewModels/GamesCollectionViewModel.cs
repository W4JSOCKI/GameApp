using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Wojtalak_Szczerkowski.GameApp.BLC;
using Wojtalak_Szczerkowski.GameApp.Core;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using Wojtalak_Szczerkowski.GameApp.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel;
using System.Windows.Input;


namespace GamesAppMaui.ViewModels
{
    public partial class GamesCollectionViewModel : ObservableObject
    {
        static string AllTypesValue = "All";

        [ObservableProperty]
        private ObservableCollection<GameViewModel> games;

        BLC blc = BLC.GetInstance();

        [ObservableProperty]
        public ObservableCollection<string> gen;


        [ObservableProperty]
        string searchString = "";
        [ObservableProperty]
        int yearFrom = -1;
        [ObservableProperty]
        int yeatTo = -1;
        [ObservableProperty]
        string selectedType = AllTypesValue;

        public GamesCollectionViewModel()
        {
            games = new ObservableCollection<GameViewModel>();

            foreach (var game in blc.GetGames())
            {
                games.Add(new GameViewModel(game));
            }
            isEditing = false;
            gameEdit=null;

            CancelCommand = new Command(
                execute: () =>
                {
                    GameEdit.PropertyChanged -= OnGameEditPropertyChanged;
                    GameEdit = null;
                    IsEditing = false;
                    RefreshCanExecute();
                },
                               canExecute: () => IsEditing
            );
           
        }

        [RelayCommand]
        private void Search()
        {
            System.Diagnostics.Debug.WriteLine(SelectedType);
            games.Clear();
            IEnumerable<IGame> daoGames = blc.GetGames();
            if (SearchString.Length > 0)
            {
                daoGames = daoGames.Where(b => b.Title.Contains(SearchString) || b.Developer.Name.Contains(SearchString));
            }
            if (YearFrom != -1)
            {
                daoGames = daoGames.Where(b => b.ReleaseYear >= YearFrom);
            }
            if (yeatTo != -1)
            {
                daoGames = daoGames.Where(b => b.ReleaseYear <= YeatTo);
            }
            if (selectedType != AllTypesValue)
            {
                bool isConverted = Enum.TryParse(selectedType, out Genre SelectedTypeEnum);
                if (isConverted)
                {
                    daoGames = daoGames.Where(b => b.Gen == SelectedTypeEnum);
                }
            }
            foreach (var game in daoGames)
            {
                games.Add(new GameViewModel(game));
            }

        }

        [ObservableProperty]
        private GameViewModel gameEdit;
        [ObservableProperty]
        private bool isEditing;

        [RelayCommand (CanExecute= nameof(CanCreateNewGame))]
        private void CreateNewGame()
        {
            GameEdit = new GameViewModel();
            GameEdit.PropertyChanged += OnGameEditPropertyChanged;
            IsEditing = true;
            RefreshCanExecute();
        }
       
        private bool CanCreateNewGame() => !IsEditing;

        [RelayCommand(CanExecute =nameof(CanEditGameBeSaved))]
        private void SaveGame()
        {
            IGame dgame = blc.GetGame(game.Id);
            GameEdit.PropertyChanged -= OnGameEditPropertyChanged;
            GameEdit = null;
            IsEditing = false;
            
            RefreshCanExecute();
        }

        private bool CanEditGameBeSaved() => GameEdit != null && GameEdit.ReleaseYear > 1500;


        private void OnGameEditPropertyChanged(object sender, PropertyChangedEventArgs arg)
        {
            RefreshCanExecute();
        }
        private void RefreshCanExecute()
        {
            CreateNewGameCommand.NotifyCanExecuteChanged();
            SaveGameCommand.NotifyCanExecuteChanged();
            (CancelCommand as Command).ChangeCanExecute();
        }

        public ICommand CancelCommand { get; set; }

    }
}
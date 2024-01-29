using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameApp.Core;
using Wojtalak_Szczerkowski.GameApp.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace GamesAppMaui.ViewModels
{
    public partial class GameViewModel : ObservableObject, IGame, INotifyPropertyChanged
    {
        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        private int _rank;
        [ObservableProperty]
        private string? _title;
        [ObservableProperty]
        private string? _platform;
        [ObservableProperty]
        private int _releaseYear;
        [ObservableProperty]
        private Genre gen;
        [ObservableProperty]
        private IDeveloper? developer;

        private Genre selectedGenre;

        public Genre SelectedGenre
        {
            get { return selectedGenre; }
            set
            {
                if (selectedGenre != value)
                {
                    selectedGenre = value;
                    OnPropertyChanged(nameof(SelectedGenre));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public IReadOnlyList<string> AllGenres => Enum.GetNames(typeof(Genre)); 

        public GameViewModel(IGame game)
        {
            Id = game.Id;
            Rank = game.Rank;
            Title = game.Title;
            Platform = game.Platform;
            ReleaseYear = game.ReleaseYear;
            Gen = game.Gen;
            Developer = game.Developer;
        }

      

        public GameViewModel()
        {
          
        }
    }
}
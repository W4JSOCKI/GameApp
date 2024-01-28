using CommunityToolkit.Mvvm.ComponentModel;
using Wojtalak_Szczerkowski.GameApp.Core;
using Wojtalak_Szczerkowski.GameApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wojtalak_Szczerkowski.GameApp.Maui.ViewModels
{
    public partial class GameViewModel : ObservableObject, IGame
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
    }
}
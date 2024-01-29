using GameApp.Core;
using Wojtalak_Szczerkowski.GameApp.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using IGame = Wojtalak_Szczerkowski.GameApp.Interfaces.IGame;
using System.Windows.Input;

namespace GameAppMAUI.ViewModels
{
    public partial class GameViewModel : ObservableValidator, IGame
    {

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        private int id;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [MinLength(2)]
        private string? title;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        private int rank;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        private string? platform;



        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [Range(1600, 2024)]
        private int releaseYear;

        [ObservableProperty]
        private IDeveloper developer;

        [ObservableProperty]
        private Genre gen;


        public GameViewModel(IGame game)
        {
            id = game.Id;
            title = game.Title;
            releaseYear = game.ReleaseYear;
            developer = game.Developer;
            gen = game.Gen;
            rank = game.Rank;
            platform = game.Platform;

        }

        ~GameViewModel()
        {
        }

        public GameViewModel()
        {
            gen = Genre.RPG;
        }
    }
}
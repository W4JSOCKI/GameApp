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


namespace Wojtalak_Szczerkowski.GameApp.Maui.ViewModels
{
    public partial class GamesCollectionViewModel : ObservableObject
    {
        static string AllTypesValue = "All";

        [ObservableProperty]
        private ObservableCollection<GameViewModel> games;

        BLC.BLC blc = BLC.BLC.GetInstance();

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
            List<string> gen = Enum.GetNames(typeof(Genre)).ToList();
            gen.Insert(0, AllTypesValue);
            Gen = new ObservableCollection<string>(gen);
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


    }
}
using GameApp.Core;
using Wojtalak_Szczerkowski.GameApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Wojtalak_Szczerkowski.GameApp.DAOMock1
{
    public class DAOMock : IDAO
    {
        private List<IDeveloper> developers;
        private List<IGame> games;

        public DAOMock()
        {
            developers = new List<IDeveloper>()
        {
            new BO.Developer() { Id = 1, Name = "Sony", Country = "Polska" },
            new BO.Developer() { Id = 2, Name = "Nvidia", Country = "Rumunia" },
            new BO.Developer() { Id = 3, Name = "Wii", Country = "UK" }
        };

            games = new List<IGame>()
        {
            new BO.Game() { Id = 1, Developer = developers[0], Rank = 12, Title = "Mario", Platform = "PC", ReleaseYear = 2018, Gen = Genre.RPG },
            new BO.Game() { Id = 2, Developer = developers[1], Rank = 1, Title = "FIFA 2115", Platform = "PSP", ReleaseYear = 2017, Gen = Genre.SIMULATOR },
            new BO.Game() { Id = 3, Developer = developers[2], Rank = 22, Title = "LIGALEGEND", Platform = "XBOX", ReleaseYear = 2022, Gen = Genre.SURVIVAL }
        };
        }

        public void AddGame(IGame game)
        {
            games.Add(game);
        }

        public void AddDeveloper(IDeveloper developer)
        {
            developers.Add(developer);
        }

        public IEnumerable<IGame> GetAllGames()
        {
            return games;
        }

        public IEnumerable<IGame> GetGamesByTitle(string name)
        {
            return games.Where(g => g.Title == name);
        }
        public IEnumerable<IDeveloper> GetAllDevelopers()
        {
            return developers;
        }

        public IGame? GetGame(int ID)
        {
            return games.FirstOrDefault(b => b.Id == ID);
        }

        public IDeveloper? GetDeveloper(int ID)
        {
            return developers.FirstOrDefault(p => p.Id == ID);
        }

        public void DeleteGame(int ID)
        {
            IGame? gameToRemove = games.FirstOrDefault(b => b.Id == ID);
            if (gameToRemove != null)
                games.Remove(gameToRemove);
        }

        public void DeleteDeveloper(int ID)
        {
            IDeveloper? developerToRemove = developers.FirstOrDefault(p => p.Id == ID);
            if (developerToRemove != null)
                developers.Remove(developerToRemove);
        }

        public void UpdateGame(IGame game)
        {
            IGame? gameToUpdate = games.FirstOrDefault(b => b.Id == game.Id);
            
                
        }

        public void UpdateDeveloper(IDeveloper developer)
        {
            IDeveloper? developerToUpdate = developers.FirstOrDefault(p => p.Id == developer.Id);
            
                
        }
    }

}

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


        public IGame GetGame(int id)
        {
            return games.FirstOrDefault(g => g.Id == id);
        }
        public void AddGame(IGame game)
        {
            games.Add(game);
        }
        public void ChangeGame(IGame game)
        {
            IGame selectedgame = games.FirstOrDefault(g => g.Id == game.Id);
        }
        public void RemoveGame(int id)
        {
            IGame selectedgame = games.FirstOrDefault(b => b.Id == id);
            games.Remove(selectedgame);
        }
        public IEnumerable<IGame> GetGames()
        {
            return games;
        }
     
        public IDeveloper GetDev(int id)
        {
            return developers.FirstOrDefault(d => d.Id == id);
        }
        public void AddDev(IDeveloper developer)
        {
            developers.Add(developer);
        }
        public void ChangeDev(IDeveloper developer)
        {
            IDeveloper selecteddev = developers.FirstOrDefault(p => p.Id == developer.Id);

        }
        public void RemoveDev(int id)
        {   
            IDeveloper? selecteddev = developers.FirstOrDefault(p => p.Id == id);
            if (selecteddev != null)
                developers.Remove(selecteddev);
        }
        public IEnumerable<IDeveloper> GetDevs()
        {
            return developers;
        }

    }

}

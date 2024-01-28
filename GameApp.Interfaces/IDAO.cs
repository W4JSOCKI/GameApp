
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wojtalak_Szczerkowski.GameApp.Interfaces
{
    public interface IDAO
    {
        IEnumerable<IDeveloper> GetAllDevelopers();
        IDeveloper? GetDeveloper(int ID);
        IDeveloper CreateNewDeveloper();
        IDeveloper? UpdateDeveloper(IDeveloper producer);
        IDeveloper? RemoveDeveloper(int ID);
        IDeveloper? AddDeveloper(IDeveloper producer);

        IEnumerable<IGame> GetAllGames();
        IGame? GetGame(int ID);
        IGame CreateNewGame();
        IGame? UpdateGame(IGame bike);
        IGame? RemoveGame(int ID);
        IGame? AddGame(IGame bike);

        void SaveChanges();
    }
}

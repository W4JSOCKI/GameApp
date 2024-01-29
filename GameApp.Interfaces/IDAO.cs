
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wojtalak_Szczerkowski.GameApp.Interfaces
{
    public interface IDAO
    {


        IEnumerable<IGame> GetAllGames();

        IEnumerable<IGame> GetGamesByTitle(string name);

        void AddGame(IGame game);
        IGame? GetGame(int gameID);
        void UpdateGame(IGame game);
        void DeleteGame(int gameID);
        


        IEnumerable<IDeveloper> GetAllDevelopers();
        
        void AddDeveloper(IDeveloper developer);

        IDeveloper? GetDeveloper(int developerID);

        void UpdateDeveloper(IDeveloper developer);

        void DeleteDeveloper(int developerID);

      
    }
}

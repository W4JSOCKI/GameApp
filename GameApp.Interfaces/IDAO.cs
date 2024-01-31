
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wojtalak_Szczerkowski.GameApp.Interfaces
{
    public interface IDAO
    {

        IGame? GetGame(int gameid);

        void AddGame(IGame game);

        void ChangeGame(IGame game);
        void RemoveGame(int gameid);

        IEnumerable<IGame> GetGames();



        IDeveloper? GetDev(int developerid);

        IEnumerable<IDeveloper> GetDevs();
        
        void AddDev(IDeveloper developer);
        void ChangeDev(IDeveloper developer);

        void RemoveDev(int developerid);

      
    }
}

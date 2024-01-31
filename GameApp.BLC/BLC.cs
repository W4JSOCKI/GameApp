using Wojtalak_Szczerkowski.GameApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace Wojtalak_Szczerkowski.GameApp.BLC
{
    public class BLC
    {

        private IDAO daoinerface;
        public BLC(string libname)
        {
            Assembly assembly = Assembly.UnsafeLoadFrom(libname);
            Type ttc = null;
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsAssignableTo(typeof(IDAO)))
                {
                    ttc = type;
                    break;
                }
            }
            daoinerface = (IDAO)Activator.CreateInstance(ttc, null);
        }

        public IGame? GetGame(int gameid)
        {
            return daoinerface.GetGame(gameid);
        }

        public void AddGame(IGame game) => daoinerface.AddGame(game);
        public void ChangeGame(IGame game) => daoinerface.ChangeGame(game);
        public void RemoveGame(int gameid) => daoinerface.RemoveGame(gameid);
        public void AddDev(IDeveloper developer) => daoinerface.AddDev(developer);
        public void ChangeDev(IDeveloper developer) => daoinerface.ChangeDev(developer);
        public void RemoveDev(int developerid) => daoinerface.RemoveDev(developerid);

        public IEnumerable<IGame> GetGames()
        {
            return daoinerface.GetGames();
        }
      
        public IDeveloper? GetDev(int developerid)
        {
            return daoinerface.GetDev(developerid);
        }

        public IEnumerable<IDeveloper> GetDevs()
        {
            return daoinerface.GetDevs();
        }
    }
}

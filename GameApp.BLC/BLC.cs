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

        private IDAO dao;

        public BLC(string library)
        {
            Assembly assembly = Assembly.UnsafeLoadFrom(library);
            Type typeToCreate = null;

            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsAssignableTo(typeof(IDAO)))
                {
                    typeToCreate = type;
                    break;
                }
            }

            dao = (IDAO)Activator.CreateInstance(typeToCreate, null);
        }

        public IEnumerable<IGame> GetAllGames() => dao.GetAllGames();
        public IEnumerable<IGame> GetGamesByTitle(string name) => dao.GetGamesByTitle(name);
        public void AddGame(IGame game) => dao.AddGame(game);
        public IGame? GetGame(int gameID) => dao.GetGame(gameID);
        public void UpdateGame(IGame game) => dao.UpdateGame(game);
        public void DeleteGame(int gameID) => dao.DeleteGame(gameID);

        public IEnumerable<IDeveloper> GetAllDevelopers() => dao.GetAllDevelopers();
        public void AddDeveloper(IDeveloper developer) => dao.AddDeveloper(developer);
        public IDeveloper? GetDeveloper(int developerID) => dao.GetDeveloper(developerID);
        public void UpdateDeveloper(IDeveloper developer) => dao.UpdateDeveloper(developer);
        public void DeleteDeveloper(int developerID) => dao.DeleteDeveloper(developerID);
    }
}

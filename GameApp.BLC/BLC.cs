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
        private static readonly Lazy<BLC> instance = new Lazy<BLC>(() => new BLC());

        private readonly IDAO dao;

        private BLC()
        {
            string libraryName = ConfigurationManager.AppSettings["DAOLibraryName"];
            Assembly assembly = Assembly.UnsafeLoadFrom(libraryName);
            Type typeToCreate = assembly.GetTypes().FirstOrDefault(t => typeof(IDAO).IsAssignableFrom(t));
            dao = (IDAO)Activator.CreateInstance(typeToCreate ?? throw new InvalidOperationException("No suitable type found"));
        }

        public static BLC GetInstance() => instance.Value;

        public void SaveChanges() => dao.SaveChanges();

        public IEnumerable<IDeveloper> GetDevelopers() => dao.GetAllDevelopers();

        public IDeveloper? GetDeveloper(int ID) => dao.GetDeveloper(ID);

        public IDeveloper CreateDeveloper() => dao.CreateNewDeveloper();

        public IDeveloper? UpdateDeveloper(IDeveloper developer) => dao.UpdateDeveloper(developer);

        public IDeveloper? RemoveDeveloper(int ID) => dao.RemoveDeveloper(ID);

        public IDeveloper? AddDeveloper(IDeveloper developer) => dao.AddDeveloper(developer);

        public IEnumerable<IGame> GetGames() => dao.GetAllGames();

        public IGame? GetGame(int ID) => dao.GetGame(ID);

        public IGame? RemoveGame(int ID) => dao.RemoveGame(ID);

        public IGame CreateGame() => dao.CreateNewGame();

        public IGame? UpdateGame(IGame game) => dao.UpdateGame(game);

        public IGame? AddGame(IGame game) => dao.AddGame(game);
    }
}

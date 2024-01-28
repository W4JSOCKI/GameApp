using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wojtalak_Szczerkowski.GameApp.DAOSQLite;
using Wojtalak_Szczerkowski.GameApp.DAOSQLite.BO;
using Wojtalak_Szczerkowski.GameApp.Interfaces;


namespace Wojtalak_Szczerkowski.GameApp.DAOSQLite
{
    public class DAO_EF : IDAO
    {
        DataContext db;

        public DAO_EF()
        {
            db = new DataContext();
        }

        public IEnumerable<IDeveloper> GetAllDevelopers()
        {
            return db.Developers;
        }
        public IDeveloper? GetDeveloper(int ID)
        {
            return db.Developers.FirstOrDefault(p => p.Id == ID);
        }
        public IDeveloper CreateNewDeveloper()
        {
            return new BO.Developer();
        }
        public IDeveloper? UpdateDeveloper(IDeveloper developer)
        {
            BO.Developer? dbProducer = developer as BO.Developer;
            if (dbProducer == null)
            {
                System.Diagnostics.Debug.WriteLine("UpdateProducer: producer is not of type BO.Producer");
                return null;
            }
            return db.Developers.Update(dbProducer).Entity;
        }
        public IDeveloper? RemoveDeveloper(int ID)
        {
            return db.Developers.Remove(db.Developers.Find(ID)).Entity;
        }

        public IDeveloper? AddDeveloper(IDeveloper developer)
        {
            BO.Developer? dbDeveloper = developer as BO.Developer;
            if (dbDeveloper == null)
            {
                System.Diagnostics.Debug.WriteLine("AddDeveloper: developer is not of type BO.Developer");
                return null;
            }
            return db.Developers.Add(dbDeveloper).Entity;
        }

        public IEnumerable<IGame> GetAllGames()
        {
            return db.Games.Include(b => b._developer);
        }
        public IGame? GetGame(int ID)
        {
            return db.Games.Include(b => b._developer).FirstOrDefault(b => b.Id == ID);
        }
        public IGame CreateNewGame()
        {
            return new BO.Game();
        }
        public IGame? UpdateGame(IGame game)
        {
            BO.Game? dbGame = game as BO.Game;
            if (dbGame == null)
            {
                System.Diagnostics.Debug.WriteLine("UpdateGame: game is not of type BO.Game");
                return null;
            }
            return db.Games.Update(dbGame).Entity;
        }
        public IGame? RemoveGame(int ID)
        {
            return db.Games.Remove(db.Games.Find(ID)).Entity;
        }

        public IGame? AddGame(IGame game)
        {
            BO.Game? dbGame = game as BO.Game;
            if (dbGame == null)
            {
                System.Diagnostics.Debug.WriteLine("AddBike: bike is not of type BO.Bike");
                return null;
            }
            return db.Games.Add(dbGame).Entity;
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }




    }
}

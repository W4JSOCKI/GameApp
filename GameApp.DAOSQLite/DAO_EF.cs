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

        public IEnumerable<IGame> GetAllGames()
        {
            return db.Games.Include(g => g._developer).ToList();
        }

        public IEnumerable<IGame>GetGamesByTitle(string name)
        {
            return db.Games.Include(g => g._developer).Where(g => g.Title == name).ToList();
        }

        public void AddGame(IGame game)
        {
            var newId = (db.Games.Max(c => (int?)c.Id) ?? 0) + 1;
            game.Id = newId;
            var newGame = new BO.Game
            {
                Id = newId,
                Rank = game.Rank,
                Title = game.Title,
                Platform = game.Platform,
                ReleaseYear = game.ReleaseYear,
                Gen = game.Gen,
                _developer = (BO.Developer)GetDeveloper(game.Developer.Id), //NW CZY Z PODLOGA
            };
            db.Games.Add(newGame);
            db.SaveChanges();
        }

        public IGame? GetGame(int ID)
        {
            return db.Games.Include(b => b._developer).FirstOrDefault(g => g.Id == ID);
        }

       
        public void UpdateGame(IGame game)
        {
            var existingGame = db.Games.Find(game.Id);
            if (existingGame != null)
            {
                db.Entry(existingGame).CurrentValues.SetValues(game);
                db.SaveChanges();
            }
        }

        public void DeleteGame(int ID)
        {
            var game = db.Games.Find(ID);
            if (game != null)
            {
                db.Games.Remove(game);
                db.SaveChanges();
            }
        }

        public IEnumerable<IDeveloper> GetAllDevelopers()
        {
            return db.Developers.ToList();
        }

        public void AddDeveloper(IDeveloper developer)
        {
            var newId = (db.Developers.Max(d => (int?)d.Id) ?? 0) + 1;
            developer.Id = newId;
            var newDev = new BO.Developer
            {
                Id = newId,
                Name = developer.Name,
                Country = developer.Country
            };

            db.Developers.Add(newDev);
            db.SaveChanges();
        }

        public IDeveloper? GetDeveloper(int ID)
        {
            return db.Developers.FirstOrDefault(d => d.Id == ID);
        }

        public void UpdateDeveloper(IDeveloper developer)
        {
            var existingDeveloper = db.Developers.Find(developer.Id);
            if (existingDeveloper != null)
            {
                db.Entry(existingDeveloper).CurrentValues.SetValues(developer);
                db.SaveChanges();
            }
        }

  

        public void DeleteDeveloper(int ID)
        {
            var developer = db.Developers.Find(ID);
            if (developer != null)
            {
                db.Developers.Remove(developer);
                db.SaveChanges();
            }
        }

    }
}

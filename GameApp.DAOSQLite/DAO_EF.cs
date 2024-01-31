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
        public IGame GetGame(int id)
        {
            return db.Games.Include(g => g._developer).FirstOrDefault(g => g.Id == id);
        }

        public void AddGame(IGame game)
        {
            var nextid = (db.Games.Max(g => (int?)g.Id) ?? 0) + 1;
            game.Id = nextid;
            var newgame = new Game
            {
                Id = nextid,
                Rank = game.Rank,
                Title = game.Title,
                Platform = game.Platform,
                ReleaseYear = game.ReleaseYear,
                Gen = game.Gen,
                _developer = (Developer)GetDev(game.Developer.Id), 
            };
            db.Games.Add(newgame);
            db.SaveChanges();
        }
        public void ChangeGame(IGame game)
        {
            var selectedgame = db.Games.Find(game.Id);
            db.Entry(selectedgame).CurrentValues.SetValues(game);
            db.SaveChanges();
           
        }
        public void RemoveGame(int id)
        {
            var game = db.Games.Find(id);
            db.Games.Remove(game);
            db.SaveChanges();
      
        }

        public IEnumerable<IGame> GetGames()
        {
            return db.Games.Include(g => g._developer).ToList();
        }

     
        public IDeveloper? GetDev(int id)
        {
            return db.Developers.FirstOrDefault(d => d.Id == id);
        }
        public void AddDev(IDeveloper developer)
        {
            var nextid = (db.Developers.Max(d => (int?)d.Id) ?? 0) + 1;
            developer.Id = nextid;
            var newdev = new Developer
            {
                Id = nextid,
                Name = developer.Name,
                Country = developer.Country
            };

            db.Developers.Add(newdev);
            db.SaveChanges();
        }

        public void ChangeDev(IDeveloper developer)
        {
            var selecteddev = db.Developers.Find(developer.Id);  
            db.Entry(selecteddev).CurrentValues.SetValues(developer);
            db.SaveChanges();
            
        }
        public void RemoveDev(int id)
        {
            var developer = db.Developers.Find(id);
            db.Developers.Remove(developer);
            db.SaveChanges();
        }
        public IEnumerable<IDeveloper> GetDevs()
        {
            return db.Developers.ToList();
        }
    }
}

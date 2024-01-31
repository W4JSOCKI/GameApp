using Microsoft.EntityFrameworkCore;
using Wojtalak_Szczerkowski.GameApp.DAOSQLite.BO;
using System.Collections.Generic;

namespace Wojtalak_Szczerkowski.GameApp.DAOSQLite
{
    public class DataContext : DbContext
    {
        public string DbPath { get; }

        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Developer> Developers { get; set; }

        public DataContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "games.db");
            DbPath = "C:\\db\\games.db";

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
        }
    }
}

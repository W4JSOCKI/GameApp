using Microsoft.EntityFrameworkCore;
using Wojtalak_Szczerkowski.GameApp.DAOSQLite.BO;
using System.Collections.Generic;

namespace Wojtalak_Szczerkowski.GameApp.DAOSQLite
{
    public class DataContext : DbContext
    {
        public string DbPath { get; }

        public virtual DbSet<BO.Game> Games { get; set; }
        public virtual DbSet<BO.Developer> Developers { get; set; }

        public DataContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "games.db");
            DbPath = "C:\\db\\games.db";
            System.Diagnostics.Debug.WriteLine($"SQLite database path: {DbPath}");

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
        }
    }
}

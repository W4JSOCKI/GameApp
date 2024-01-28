using System.ComponentModel.DataAnnotations.Schema;
using Wojtalak_Szczerkowski.GameApp.Core;
using Wojtalak_Szczerkowski.GameApp.Interfaces;

namespace Wojtalak_Szczerkowski.GameApp.DAOSQLite.BO
{
    public class Game : IGame
    {
        public int Id { get; set; }
        public int Rank { get; set; }
        public string Title { get; set; }
        public string Platform { get; set; }
        public int ReleaseYear { get; set; }
        public Genre Gen { get; set; }

        public Developer _developer { get; set; }

        [NotMapped]
        public IDeveloper Developer
        {
            get
            {
                return _developer;
            }
            set
            {
                _developer = value as Developer;
            }
        }

    }
}

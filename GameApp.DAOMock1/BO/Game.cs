using Wojtalak_Szczerkowski.GameApp.Core;
using Wojtalak_Szczerkowski.GameApp.Interfaces;

namespace Wojtalak_Szczerkowski.GameApp.DAOMock1.BO
{
    public class Game : IGame
    {
        public int Id { get; set; }
        public int Rank { get; set; }
        public string Title { get; set; }
        public string Platform { get; set; }
        public int ReleaseYear { get; set; }
        public IDeveloper Developer { get; set; }

        public Genre Gen { get; set; }
    }
}

using Wojtalak_Szczerkowski.GameApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wojtalak_Szczerkowski.GameApp.Interfaces
{
    public interface IGame
    {
        public int Id { get; set; }
        public int Rank { get; set; }
        public string Title { get; set; }
        public string Platform { get; set; }
        public int ReleaseYear { get; set; }
        public IDeveloper Developer { get; set; }

        public Genre Gen { get; set; }

        IGame CopyFrom(IGame game)
        {
            Id = game.Id;
            Rank = game.Rank;
            Title = game.Title;
            Platform = game.Platform;
            ReleaseYear = game.ReleaseYear;
            Developer = game.Developer;
            ReleaseYear = game.ReleaseYear;
            Gen = game.Gen;
            return this;
        }
    }
}
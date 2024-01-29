using GameApp.Core;
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

    }
}
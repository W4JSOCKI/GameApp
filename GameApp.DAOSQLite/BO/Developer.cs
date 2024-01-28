using Wojtalak_Szczerkowski.GameApp.Interfaces;

namespace Wojtalak_Szczerkowski.GameApp.DAOSQLite.BO
{
    public class Developer : IDeveloper
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
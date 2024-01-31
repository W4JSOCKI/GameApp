using System.Configuration;
using Wojtalak_Szczerkowski.GameApp.BLC;
using Wojtalak_Szczerkowski.GameApp.Interfaces;

namespace Wojtalak_Szczerkowski.GameApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            BLC.BLC blc = new BLC.BLC(ConfigurationManager.AppSettings["DAOLibraryName"]);

           foreach (IDeveloper d in blc.GetDevs())
            {
                Console.WriteLine($"{d.Name} : {d.Country}");
            }

            Console.WriteLine("------------------------------");

            foreach (IGame g in blc.GetGames())
            {
                Console.WriteLine($"{g.Id}: {g.Rank} {g.Title} {g.Platform} {g.ReleaseYear} {g.Gen} {g.Developer.Name}");
            }
        }
    }
}
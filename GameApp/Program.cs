using Wojtalak_Szczerkowski.GameApp.BLC;
using Wojtalak_Szczerkowski.GameApp.Interfaces;

namespace Wojtalak_Szczerkowski.GameApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            BLC.BLC blc = BLC.BLC.GetInstance();

            foreach (IDeveloper d in blc.GetDevelopers())
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
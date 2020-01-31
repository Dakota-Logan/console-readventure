using System;
using ConsoleAdventure.Project;
using ConsoleAdventure.Project.Controllers;

namespace ConsoleAdventure
{
    public static class Program
    {
        public static GameController controller = new GameController();
        public static void Main()
        {
			controller.Run();
			Console.WriteLine("Thanks for playing!");
			// Console.WriteLine("Go fuck yourself!");
        }
    }
}

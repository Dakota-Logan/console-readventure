using System;
using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project.Controllers
{
	public class GameController : IGameController
	{
		public GameService _gameService = new GameService();
		public static bool running = true;

		//NOTE Makes sure everything is called to finish Setup and Starts the Game loop
		public void Run()
		{
			Console.WriteLine("What is your name?");
			Console.WriteLine();
			_gameService.Setup(Console.ReadLine());
			while (running)
			{
				Print();
				GetUserInput();
			}
		}

		//NOTE Gets the user input, calls the appropriate command, and passes on the option if needed.
		public void GetUserInput()
		{
			Console.WriteLine("What would you like to do?");
			Console.WriteLine();
			string input = Console.ReadLine().ToLower() + " ";
			string command = input.Substring(0, input.IndexOf(" "));
			string option = input.Substring(input.IndexOf(" ") + 1).Trim();
			//NOTE this will take the user input and parse it into a command and option.
			//IE: take silver key => command = "take" option = "silver key
			switch (command)
			{
				case "go":
					_gameService.Go(option);
					break;
				case "take":
					_gameService.TakeItem(option);
					break;
				case "use":
					_gameService.UseItem(option);
					break;
				case "look":
					_gameService.Look();
					break;
				case "inventory":
					_gameService.Inventory();
					break;
				case "help":
					_gameService.Help();
					break;
				case "restart":
					_gameService.Reset();
					break;
				case "quit":
					running = false;
					break;
				default:
					_gameService.Messages.Add("Bad input, please enter again.");
					break;
			}
		}

		//NOTE this should print your messages for the game.
		private void Print()
		{
			Console.Clear();
			foreach (string message in _gameService.Messages)
			{
				Console.WriteLine(message);
				Console.WriteLine();
			}
			_gameService.Messages.Clear();
		}

		public void Reset()
		{
			_gameService = new GameService();
		}
	}
}
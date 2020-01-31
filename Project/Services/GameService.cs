using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ConsoleAdventure.Project.Controllers;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project
{
	public class GameService : IGameService
	{
		private IGame _game { get; set; }

		public List<string> Messages { get; set; }

		public GameService()
		{
			_game = new Game();
			Messages = new List<string>();
		}

		public void Help()
		{
			Messages.Add("Type 'go' and the name of a room to enter it.");
			Messages.Add("Type 'help' for this dialog.");
			Messages.Add("Type 'inventory' to see inventory.");
			Messages.Add("Type 'use' to use item.");
			Messages.Add($"Current room: {_game.CurrentRoom.Name}.");
		}

		public void Go(string direction)
		{
			if (direction == null || direction == "")
			{
				Messages.Add("Bad input.");
				return;
			}

			Room newRoom = null;
			if (_game.CurrentRoom.Exits.ContainsKey(direction))
				newRoom = (Room) _game.CurrentRoom.Exits[direction];
			else
			{
				Messages.Add("Bad input.");
				return;
			}
			Messages.Add($"You have entered: {newRoom.Name} - {newRoom.Description}");
			newRoom.Enter((Game) _game);
			if (newRoom.Name == "primary_1")
			{
				if(_game.CurrentPlayer.Inventory.Find(cur=>cur.Name=="Switchblade")!=null)
					Console.WriteLine("You win!");
				else
					Console.WriteLine("You lose!");
				Thread.Sleep(1000);
				Program.controller.Reset();
			}
		}

		public void Inventory()
		{
			foreach (IItem item in _game.CurrentPlayer.Inventory)
			{
				Messages.Add($" --  {item.Name} | {item.Description} -- ");
			}
		}

		public void Look()
		{
			if (_game.CurrentPlayer.Inventory.Find(cur => cur.Name == "Torch") != null)
			{
				if (_game.CurrentRoom.Items.Count > 0)
				{
					foreach (Item item in _game.CurrentRoom.Items)
					{
						Messages.Add($"Item: {item.Name} - {item.Description}");
					}

					if (((Room) _game.CurrentRoom).StoredKey != null)
					{
						Messages.Add(
							$"Key: {((Room) _game.CurrentRoom).StoredKey.Name} - {((Room) _game.CurrentRoom).StoredKey.Description}");
					}
				}
			}
			else
			{
				Messages.Add("You can't see in here, maybe there is some sort of light source you could find.");
			}
		}

		///<summary>
		///Restarts the game
		///</summary>
		public void Reset()
		{
			_game = new Game();
			Console.Clear();
			Console.WriteLine("What is your name?");
			Console.WriteLine();
			Setup(Console.ReadLine());
		}

		public void Setup(string playerName)
		{
			_game.CurrentPlayer.Name = playerName;
			_game.Setup();
			Messages.Add($"{_game.CurrentRoom.Name} - {_game.CurrentRoom.Description}");
			Messages.Add("Exits:");
			foreach (KeyValuePair<string, IRoom> keyValuePair in _game.CurrentRoom.Exits)
			{
				Messages.Add($"{keyValuePair.Value.Name} - {keyValuePair.Value.Description}");
			}
		}

		///<summary>When taking an item be sure the item is in the current room before adding it to the player inventory, Also don't forget to remove the item from the room it was picked up in</summary>
		public void TakeItem(string itemName)
		{
			Item test = _game.CurrentRoom.Items.Find(cur => cur.Name.ToLower() == itemName.ToLower());
			Console.WriteLine((test == null).ToString());
			if (test != null)
				_game.CurrentPlayer.Inventory.Add(test);
			else
			{
				Messages.Add("No items with that name.");
				return;
			}
			Messages.Add("Item taken: " + test.Name);
			_game.CurrentRoom.Items.Remove(test);
		}

		///<summary>
		///No need to Pass a room since Items can only be used in the CurrentRoom
		///Make sure you validate the item is in the room or player inventory before
		///being able to use the item
		///</summary>
		public void UseItem(string itemName)
		{
			Item item = _game.CurrentPlayer.Inventory.Find(cur => cur.Name == itemName);
			if (item != null && item.Used != true)
				item.Used = true;
			else
			{
				Messages.Add("Item does not exist or has already been used.");
				return;
			}

			Messages.Add("Item used");
		}
	}
}
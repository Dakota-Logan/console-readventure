using System;
using System.Collections.Generic;
using ConsoleAdventure.Project.Controllers;
using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
	public class Room : IRoom
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public List<Item> Items { get; set; }
		public Dictionary<string, IRoom> Exits { get; set; }
		public bool Locked { get; set; }

		public Key StoredKey { get; set; }

		public void Unlock()
		{
			this.Locked = false;
		}

		public Room(string name, string description, List<Item> items, bool locked, Key key = null)
		{
			Name = name;
			Description = description;
			Items = items;
			Locked = locked;
			StoredKey = key;
		}

		public void Look(Player player)
		{
			if (player.Inventory.Find(cur => cur.Name == "Torch") != null)
				player.Inventory.AddRange(Items);
			else
				return;
			List<string> msgs = new List<string>();
			foreach (Item item in Items)
			{
				msgs.Add($"You have found {item.Name}. {item.Description}");
			}
		}

		public void Enter(Game _game)
		{
			if (Locked == true && ((Player)_game.CurrentPlayer).Keys.Find(cur=>cur.Id==Name)!=null)
			{
				_game.CurrentRoom = this;
				Program.controller._gameService.Messages.Add($"{Name} - {Description}");

			}
			else if (Locked == true)
				Program.controller._gameService.Messages.Add("Can't go in there without the key!");
			else
			{
				_game.CurrentRoom = this;
				Program.controller._gameService.Messages.Add($"{Name} - {Description}");
			}
		}
	}
}
using System;
using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
	public class Key
	{
		public Key(string name, string description, string id)
		{
			Name = name;
			Description = description;
			Id = id;
		}
		public string Name { get; set; }
		public string Description { get; set; }
		public string Id { get; set; }
	}
}
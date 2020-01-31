using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
    public class Player : IPlayer
    {
        public string Name { get; set; }
        public List<Item> Inventory { get; set; } = new List<Item>();

        public List<Key> Keys { get; set; } = new List<Key>();
    }
}
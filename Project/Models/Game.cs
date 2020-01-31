using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
    public class Game : IGame
    {
        public IRoom CurrentRoom { get; set; }
        public IPlayer CurrentPlayer { get; set; } = new Player();
        public Player RealPlayer = new Player();

        //NOTE Make yo rooms here...
        public void Setup()
        {
	        #region items
			Key ext_1_key = new Key("Golden Key",
				"A key that appears to be made of gold with silver trim and bronze counter-weights.", "ext_1");
			Key hall_2_key = new Key("Key", "A key that appears to be for a generic lock.", "hallway_2");
			Item Torch = new Item("Torch",
				"A glowing light source that provides a decent amount of light, more than it should.");
			Item Knife = new Item("Switchblade", "A switchblade knife, not much to say. You should know what that is...");
			#endregion

			#region enemies
			Enemy RadRoach = new Enemy("RadRoach",
				"A filthy, oversized, overpowered roach that has grown due to radiation.");
			#endregion

			#region rooms
			Room ext_1 = new Room("ext_1", "Extra room, most likely used for storage.",
				new List<Item>() { }, true);

			Room ext_2 = new Room("ext_2",
				"Looks like a few around the house things, cleaning equipment, bedframes, etc.",
				new List<Item>() {}, false);

			Room primary_1 = new Room("primary_1",
				"The primary room of the building, a table, some shelfs, and a small glowing light, but nothing seems to be in here.",
				new List<Item>() {}, false);

			Room primary_2 = new Room("primary_2",
				@"The secondary room, there is a torch on the wall that seems to be quite bright, brighter than it 
should be. There is also a table in the center of the room, it looks like it has something on it.", new List<Item>()
					{Torch, Knife}, false);

			Room primary_3 = new Room("primary_3", "A simple room with some cupboards around the edge of the s	",
			new List<Item>(){}, false, hall_2_key);

			Room end_of_hall = new Room("end_of_hall",
				"A dark room with what looks like something in the corner, maybe you should take a look?",
				new List<Item>() {}, false, ext_1_key);

			Room hallway_1 = new Room("hallway_1",
				"A hallway with 5 doors and a long walk from one end to the next",
				new List<Item>() { }, false);

			Room hallway_2 = new Room("hallway_2", "A hallway leading to extra rooms",
				new List<Item>(),
				true);
			#endregion

			#region roomExits
			primary_1.Exits = new Dictionary<string, IRoom>() {{"hallway_1", hallway_1}};
			primary_2.Exits = new Dictionary<string, IRoom>() {{"hallway_1", hallway_1}};
			primary_3.Exits = new Dictionary<string, IRoom>() {{"hallway_1", hallway_1}};
			end_of_hall.Exits = new Dictionary<string, IRoom>() {{"hallway_1", hallway_1}};
			ext_1.Exits = new Dictionary<string, IRoom>(){{"hallway_2", hallway_2}};
			ext_2.Exits = new Dictionary<string, IRoom>(){{"hallway_2", hallway_2}};
			hallway_1.Exits = new Dictionary<string, IRoom>()
			{
				{"primary_1", primary_1}, {"primary_2", primary_2}, {"primary_3", primary_3},
				{"end_of_hall", end_of_hall}, {"hallway_2", hallway_2}
			};
			hallway_2.Exits = new Dictionary<string, IRoom>()
				{{"ext_1", ext_1}, {"ext_2", ext_2}, {"hallway_1", hallway_1}};
			#endregion

			CurrentRoom = hallway_1;
        }
    }
}
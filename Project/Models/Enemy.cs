namespace ConsoleAdventure.Project.Models
{
	public class Enemy
	{
		public Enemy(string name, string description)
		{
			Name = name;
			Description = description;
		}
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
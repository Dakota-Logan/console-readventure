using System;
using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
    public class Item : IItem
    {
	    public Item(string name, string desc)
	    {
		    Name = name;
		    Description = desc;
		    Used = false;
	    }

	    protected Item()
	    {
		    throw new NotImplementedException();
	    }

	    public string Name { get; set; }
        public string Description { get; set; }

        public bool Used { get; set; }
    }

}
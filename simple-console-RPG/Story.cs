using System;

namespace simple_console_RPG
{
	public class Story
	{
		static public void Start()
		{
			Console.WriteLine("Welcome to the RPG adventure!");
			CharacterStats player = new CharacterStats();
			player.CreateCharacter();
        }

    
    }
}


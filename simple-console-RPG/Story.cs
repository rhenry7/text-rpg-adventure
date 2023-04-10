using System;

namespace simple_console_RPG
{
	public class Story
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Welcome to the RPG adventure!");
			CharacterStats player = new CharacterStats();
			player.CreateCharacter();
			AdventureGenerator newStory = new AdventureGenerator();
			string story = newStory.GenerateAdventure(2).Result;
			Console.WriteLine(story);
			Console.ReadLine();

        }

    
    }
}


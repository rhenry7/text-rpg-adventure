using System;
using OpenAI_API;

namespace simple_console_RPG
{
	public class Story
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Welcome to the RPG adventure! \n");
			Console.WriteLine("one moment while we create your story...");
			AdventureGenerator story = new AdventureGenerator();
			string userStory = story.GenerateAdventure().Result;
			Console.WriteLine(userStory);
			CharacterStats player = new CharacterStats();
			player.CreateCharacter();
			Console.ReadLine();
        }

    
    }
}


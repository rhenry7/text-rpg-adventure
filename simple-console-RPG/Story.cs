
using System;

namespace simple_console_RPG
{
	public class StoryAng
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("Welcome to the RPG adventure! \n");
			CharacterStats player = new CharacterStats();
            StoryObjects story = new StoryObjects();

            await story.Intro(player.CreateCharacter());
			Console.WriteLine("did it finish chapter one bro?");
			Console.ReadKey();

        }


    }
}


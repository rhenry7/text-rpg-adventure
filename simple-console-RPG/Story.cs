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
			Console.WriteLine("As the sun rises over the medieval kingdom of Ardania, \n" +
				"the people tremble in fear. For a great dragon has awoken from its slumber," + "and it has been terrorizing the land for weeks." +
				"The king has put out a call to arms, seeking brave warriors to slay the beast and bring peace back to the kingdom." +
				"\n\nYou are a young adventurer, eager to prove your worth and earn your place among the heroes of Ardania. Hearing the call, you make your way to the castle, " +
				"\nwhere the king himself greets you. He tells you of the dragon's lair, deep in the mountains to the east, and offers you a handsome reward for slaying the beast.");
			Console.ReadLine();

        }

    
    }
}


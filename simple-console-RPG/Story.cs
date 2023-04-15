
namespace simple_console_RPG
{
	public class StoryAng
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Welcome to the RPG adventure! \n");
			CharacterStats player = new CharacterStats();
			player.CreateCharacter();
            Console.ReadLine();
        }

    
    }
}


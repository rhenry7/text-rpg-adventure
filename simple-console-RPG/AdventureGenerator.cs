using System.Net.Http.Headers;
using System.Text.Json;
using OpenAI_API;
using System.IO;
using simple_console_RPG;


public class AdventureGenerator
{

    public enum Location
    {
        Castle,
        Forrest,
        Desert,
        Swamp,
        Dungeon,
        Village,
        Mountain,
        Volcano,
        IceKingdom,
        RockyVillage,
    }


    public enum Enemy
    {
        GoblinArmy,
        EvilElves,
        MagicMushroomPeople,
        SwampMonsters,
        Dragons,
        GiantTrolls,
        MagicBirds,
        LavaLizardsWithSwords,
        Orc,
        Knights,
    }

    public enum Objective
    {
        SaveThePrincess,
        AvengeYourFather,
        Destroy_The_Magic_Orb,
        Find_The_Ancient_Treasure,
        The_Sacred_Sword,
        Heal_The_AncientTree,
        DefeatTheEmperor,
        DeliverTheChosenOneToSafety,
        FindTheKeysToTheHiddenKingdom,
        SlayTheDragon,
    }

    private string _setting { get; set; }
    private string _enemyAI { get; set; }
    private string _goal { get; set; }
    private bool _chapterOneComplete { get; set; }
    private bool _chapterTwoComplete { get; set; }

    private bool ChapterOneComplete
    {
        get => _chapterOneComplete;
        set => _chapterOneComplete = value;
    }

    private bool ChapterTwoComplete
    {
        get => _chapterTwoComplete;
        set => _chapterTwoComplete = value;
    }

    private string Setting
    {
        get => _setting;
        set => _setting = value;

    }

    private string EnemyAi
    {
        get => _enemyAI;
        set => _enemyAI = value;

    }

    private string Goal
    {
        get => _goal;
        set => _goal = value;

    }

    public async Task<string> GenerateAdventure(int? DiceRollOne, int? DiceRollTwo, int? DiceRollThree, int? Choice, PlayerStats player) // int Location, int Enemy, int Objective
    {
    

        string apikeyFilePath = "apikey.txt";
        string text = File.ReadAllText(apikeyFilePath);
        OpenAIAPI api = new OpenAIAPI(text);
        var chat = api.Chat.CreateConversation();
        PlayerStats newPlayer = new PlayerStats();

        if (DiceRollOne.HasValue && DiceRollTwo.HasValue && DiceRollThree.HasValue)
        {
            Setting = Enum.GetName(typeof(Location), DiceRollOne.Value);
            EnemyAi = Enum.GetName(typeof(Enemy), DiceRollTwo.Value);
            Goal = Enum.GetName(typeof(Objective), DiceRollThree.Value);
        }

        if (ChapterOneComplete && Choice == 2)
        {
            Console.WriteLine("Chapter One complete...");
            string chapterTwo = await ChapterTwoAsync();
            Console.WriteLine(chapterTwo);
            Console.ReadLine();
            return "";
        } else if(ChapterTwoComplete && Choice == 3)
        {
            Console.WriteLine("Chapter Two complete...");
            string chapterThree = await ChapterThreeAsync();
            Console.WriteLine(chapterThree);
            Console.ReadLine();
            return "on to chapter three...";
        }
            else
        {
            chat.AppendSystemMessage("Write the intro story for a dungeons and dragons RPG board game."
            + "Create a story based on user input such as location, enemy, and objective."
            + "If the user tells you a location, enemy and objective, you create the setting for the story based on input. limit 1 paragraph");
            chat.AppendUserInput($"Location: {Enum.GetName(typeof(Location), DiceRollOne)}");
            chat.AppendUserInput($"Enemy: {Enum.GetName(typeof(Enemy), DiceRollTwo)}");
            chat.AppendUserInput($"Objective: {Enum.GetName(typeof(Objective), DiceRollThree)}");
            ChapterOneComplete = true;
        }

         async Task<string> ChapterTwoAsync()
        {
            int enemyLevel = new Random().Next(0, 4);
        
            if( player.Strength > enemyLevel)
            {
                chat.AppendUserInput($"characters struggle to fight {EnemyAi},"
                + $"they use their skills but the {EnemyAi} proves to be a real challenge.. but wait!"
                + $"they see something in the {Setting} that may help them.. three sentences");
                chat.AppendUserInput($"{player.Name} was able to defeat {EnemyAi} using his strength in a great battle! \n");
                Console.WriteLine($"Due to your strength... you may continue on your quest... \n");
            }

            if (player.Strength < 4)
            {
                Console.WriteLine($"because character has level strength: {player.Strength}, they were NOT able to defeat {EnemyAi}");
                chat.AppendUserInput($"the character has taken heavy damage in their battle against {EnemyAi}");
                newPlayer.Health = player.Health;
                newPlayer.Health = newPlayer.Health - 4;
                Console.WriteLine($"You have HP: {newPlayer.Health} remaining");
                Console.WriteLine("You have almost died! but you march on...");
            }

            if (player.Speed > enemyLevel)
            {
                chat.AppendUserInput($"characters struggle to fight {EnemyAi},"
                + $"they use their skills but the {EnemyAi} proves to be a real challenge.. but wait!"
                + $"luckily, they use their speed to evade the enemy.. they navigate the {Setting} and get away!");
                chat.AppendUserInput($"because character has level speed: {player.Speed}, they were able to evade {EnemyAi}");
            }

            var result = await chat.GetResponseFromChatbotAsync();
            ChapterTwoComplete = true;
            return result;
  
        }

        async Task<string> ChapterThreeAsync()
        {
            if(player.Luck >= 7)
            {
                chat.AppendUserInput($"They survive an attack agaisnt {EnemyAi},"
         + $"in the {Setting}, the player finds a lucky: weapon. "
         + $" {player.Name} miracously achieves the {Goal}, with the help of a legendary warrior who happened to be in {Setting}"
         + $"  ...one paragraph");
                var result = await chat.GetResponseFromChatbotAsync();
                return result;
            } else
            {
                chat.AppendUserInput($"They battle more {EnemyAi},"
          + $"they navigate the {Setting} and found someone: kind; wise; age; "
          + $" {player.Name} has almost reached; nearly accomplish; the {Goal}"
          + $" {player.Name} must defeat the strongest boss {EnemyAi} to accomplish {Goal}.. one paragraph");
                var result = await chat.GetResponseFromChatbotAsync();
                return result;
            }
          
        }


        string response = await chat.GetResponseFromChatbotAsync();
        return response;
    }
}

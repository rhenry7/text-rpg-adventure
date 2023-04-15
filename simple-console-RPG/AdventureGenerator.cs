using System.Net.Http.Headers;
using System.Text.Json;
using OpenAI_API;
using System.IO;
using simple_console_RPG;


public class AdventureGenerator
{
    // main story points
    public string[] enemyArray = new string[] { "Goblin Army", "Evil Elves", "Savage Wizards", "Barbarians", "Undead Knights", "Ghost Mercenaries", "Lava Lizards with swords", "Fire Dragon", "Corrupt Politicians"};
    public string[] enemyLeaderArray = new string[] { "Council of Elves", "Immortal Wizard", "Tribal Cheiftan", "The forgotten King", "The Dragon shapeshifter", "Prideful General", "The Mad King", "High Priests", "Ancient Pirate King" };
    public string[] locationArray = new string[] { "Castle", "Forest", "Desert", "Swamp", "Rainy Dungeon", "Grassy Village", "Mountain", "Volcano", "Ice Kingdom", "Jungle" };
    public string[] objectiveArray = new string[] { "Save the princess", "Find the keys to the hidden kingdom", "Defeat the emperor", "find who killed your father", "solve the puzzle of the time treasure", "Heal the ancient tree", "uncover the corruption in the city", "help the rebels free their people", "Vanquish the wizard", "Destroy the magic orb" };
    // specifics for enemy, locations or objective
    public string[] enemyMotivationArray = new string[] {
        "Environmentalism: The enemy is an environmental extremist who believes that humans are destroying the planet and seeks to stop them at any cost.",
        "Religious fundamentalism: The enemy is Mysteriously motivated by religious fundamentalism and seeks to establish a theocratic state based on their particular religion.",
        "Capitalism: The enemy is a greedy capitalist who believes that profit and economic growth are the most important goals, even if it means exploiting others and damaging the environment.",
        "Neo-colonialism: The enemy is a representative of a powerful foreign kindgom that seeks to exploit or dominate the world for their own benefit.",
        "Madness: The enemy is driven insane by dark magic, a curse, or a traumatic event, causing them to lash out against others.",
        "Redemption: The enemy seeks redemption for past misdeeds, but believes that the only way to achieve it is by committing a great act of evil.",
        "Survival: The enemy believs they are justified and is simply trying to survive in a dangerous world and views the players as a threat to their own existence.",
        "Honor: The enemy is Fiercely motivated by a sense of honor or duty, and believes that their actions are justified by a higher moral code.",
        "Independence: The enemy is fighting for independence and autonomy from a larger, more powerful state or kingdom, motivated by a desire for self-determination and sovereignty.",
        "Love: The enemy believs they are the hero is motivated by love for another person, whether it be a romantic partner, family member, or friend, and will do anything to protect them."
    };
    // verbs
    string[] fightVerbs = new string[] { "Attack", "Strike", "Assault", "Charge", "Pummel", "Smash", "Thrash", "Beat", "Conquer", "Vanquish" };
    string[] dodgeVerbs = new string[] { "Duck", "Dodge", "Evade", "Escape", "Flee", "Jump", "Roll", "Sprint", "Tumble", "Weave" };
    // adverbs
    string[] combatAdverbs = new string[] { "Ferociously", "Savagely", "Brutally", "Viciously", "Relentlessly", "Mercilessly", "Fiercely", "Wildly", "Intensely", "Violently" };
    string[] dodgeAdverbs = new string[] { "Swiftly", "Gracefully", "Effortlessly", "Nimbly", "Quickly", "Evasively", "Skillfully", "Agilely", "Dexterously", "Acrobatically" };
    // adjectives
    string[] enemyAdjectives = new string[] { "Fierce", "Savage", "Mysterious", "Brutal", "Hateful", "Cruel", "Malignant", "Wicked", "Cunning", "Diabolical" };
    string[] magicAdverbs = new string[] { "Mystically", "Enchantingly", "Eerily", "Magically", "Spellbindingly", "Charmingly", "Spiritedly", "Ethereally", "Supernaturally", "Enigmatically" };
    // magic attacks
    string[] magicSpells = new string[] { "Fireball", "Ice Lance", "Thunderbolt", "Frost Nova", "Arcane Missile", "Shadow Bolt", "Divine Light", "Nature's Wrath", "Gravity Well", "Time Warp" };

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
    private bool _chapterThreeComplete { get; set; }
    private string enemyGoal { get; set; }
    private string _combatDescription { get; set; }

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

    private bool ChapterThreeComplete
    {
        get => _chapterThreeComplete;
        set => _chapterThreeComplete = value;
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
    private string EnemyGoal
    {
        get => enemyGoal;
        set => enemyGoal = value;
    }
    private string CombatDescription
    {
        get => _combatDescription;
        set => _combatDescription = value;
    }

    // dont need three dice rolls, one can do the job..
    public async Task<string> GenerateAdventure(int? DiceRollOne, int? DiceRollTwo, int? DiceRollThree, int? Choice, PlayerStats player) // int Location, int Enemy, int Objective
    {
        string apikeyFilePath = "apikey.txt";
        string text = File.ReadAllText(apikeyFilePath);
        OpenAIAPI api = new OpenAIAPI(text);
        var chat = api.Chat.CreateConversation();
        PlayerStats newPlayer = new PlayerStats();

        if (DiceRollOne.HasValue && DiceRollTwo.HasValue && DiceRollThree.HasValue)
        {
            Setting = locationArray[(int)DiceRollOne];
            EnemyAi = enemyArray[(int)DiceRollTwo];
            Goal = objectiveArray[(int)DiceRollThree];
            EnemyGoal = enemyMotivationArray[new Random().Next(0, 10)];
            CombatDescription = combatAdverbs[new Random().Next(0, 10)];

        }

        if (ChapterOneComplete && Choice == 2)
        {
            Console.WriteLine("Chapter One complete...");
            string chapterTwo = await ChapterTwoAsync();
            Console.WriteLine(chapterTwo);
            return "";
        } else if(ChapterTwoComplete && Choice == 3)
        {
            Console.WriteLine("Chapter Two complete...");
            string chapterThree = await ChapterThreeAsync();
            Console.WriteLine(chapterThree);
            Console.ReadLine();
            return "you bravely continue..."; // create a chapter four
        } else if(ChapterThreeComplete && Choice ==4)
        {
            Console.WriteLine("Chapter Three complete...");
            string chapterThree = await ChapterFourAsync();
            Console.WriteLine(chapterThree);
            Console.ReadLine();
        }
            else 
        {
            chat.AppendSystemMessage(" generate adventure storyline for an RPG game! "
            + " Create a story based on user input such as location, enemy, and objective."
            + $" If the user tells you a location, enemy and {EnemyGoal}, you create the setting for the story based on input. limit 1 paragraph");
            chat.AppendUserInput($"location: {Setting}");
            chat.AppendUserInput($"Enemy: {EnemyAi}");
            chat.AppendUserInput($"Objective: {Goal}");
            ChapterOneComplete = true;
        }

         async Task<string> ChapterTwoAsync() // handles combat 
        {
            int enemyStrengthLevel = new Random().Next(0, 4);
            int enemySpeedLevel = new Random().Next(0, 4);
            chat.AppendUserInput($"The enemy objective is described more: {EnemyGoal} ...two sentences");
            Console.WriteLine($"An enemy approaches... Will you run or will you fight? ");
            Console.WriteLine("Enter 1 to run, or 2 to fignt");
            // choose to run? or choose to fight!
            // 1 to fight, 2 to run
            int combatChoice = int.Parse(Console.ReadLine());

            if(combatChoice == 2)
            {

            if ( player.Strength > 4)
            {
                chat.AppendUserInput($"characters encounter and must fight the enemy,"
                +  $"they battle in the {Setting} but the battle is {CombatDescription}" 
                + $"player has decided becuase of motive to fight typeof:  [single, singular] {EnemyAi} using their typeof: {Goal}"
                + $"continue the story, player takes some damage in battle, they navigate {Setting} to defeat {EnemyAi} ...four sentences");
      
                newPlayer.Health = player.Health - enemyStrengthLevel ;
         
                Console.WriteLine($"You have HP: {newPlayer.Health} remaining");
                Console.WriteLine($"You must use your strength to fight... \n");
                var endOfChapter = await chat.GetResponseFromChatbotAsync();
                ChapterTwoComplete = true;
                return endOfChapter;
            }

            if (player.Strength < 5)
            {
                Console.WriteLine($"because character has level strength: {player.Strength}, they were NOT able to defeat {EnemyAi}");
                newPlayer.Health = player.Health;
                newPlayer.Health = newPlayer.Health - 5;
                Console.WriteLine($"You lost 5 HP");
                    chat.AppendUserInput($"the character has taken heavy damage in their battle against {EnemyAi}"
                        + $"now, the character must use their skills to defeat the [single, singular] {EnemyAi}"
                        + $"the {EnemyAi} has been strong, and the character has been injured .. three short sentences");
                Console.WriteLine($"You have {newPlayer.Health} HP remaining");
                Console.WriteLine("You have been wounded! but you survive and must march on...");
                var endOfChapter = await chat.GetResponseFromChatbotAsync();
                ChapterTwoComplete = true;
                return endOfChapter;
            }


            }
            else
            {
                if (player.Speed > enemySpeedLevel)
                {
                    chat.AppendUserInput($"continue the story.. characters struggle to fight {EnemyAi},"
                    + $"they use their skills but the {EnemyAi} proves to be a real challenge.. but wait!"
                    + "due to speed character takes minor damage, and deals with an injury"
                    + $"luckily, they use their speed to evade the enemy.. they navigate the {Setting} and get away!"
                    + $"speed has helped the character escape the evil {EnemyAi}.. the journey continues ..five sentences");
                    newPlayer.Health = player.Health;
                    newPlayer.Health = newPlayer.Health - 4;
                    var endOfChapter = await chat.GetResponseFromChatbotAsync();
                    ChapterTwoComplete = true;
                    return endOfChapter;
                }
                else if (player.Magic > enemyStrengthLevel)
                {
                    newPlayer.Health = player.Health;
                    newPlayer.Health = newPlayer.Health - new Random().Next(0, 10);
                    chat.AppendUserInput($"continue the story.. characters encounter and must fight the {EnemyAi},"
                     + "they take damage and is injured!.."
                     + $"they battle in the {Setting} but the battle is {CombatDescription}, they character uses a magic spell blast to fight and win.. four sentences");
                    var endOfChapter = await chat.GetResponseFromChatbotAsync();
                    ChapterTwoComplete = true;
                    return endOfChapter;

                }
                else
                {

                    return "should not be here, fix me right now!! (inside of combat loop)";
                }
            }

            return "should not be here, fix me right now!! (outside of combat loop)";
        }

        async Task<string> ChapterThreeAsync()
        {
            if(player.Luck >= 7)
            {
           chat.AppendUserInput($"They survive an attack against {EnemyAi},"
         + $"in the {Setting}, the player finds a lucky: weapon. "
         + $" {player.Name} miracously achieves the {Goal}, with the help of a legendary warrior who happened to be in {Setting}"
         + $"  ...one paragraph");
                var result = await chat.GetResponseFromChatbotAsync();
                return result;
            }
            else if (player.Magic > 6)
            {
                chat.AppendUserInput($"continue the story.. characters feel emotion about {EnemyGoal},"
                 + $"they navigate the {Setting} and use their magic {CombatDescription}, they character uses their magic to fight and win.. four sentences");
                var endOfChapter = await chat.GetResponseFromChatbotAsync();
                ChapterTwoComplete = true;
                return endOfChapter;

            } else
            {
                newPlayer.Health = player.Health;
                newPlayer.Health = newPlayer.Health - new Random().Next(0, 10) * 2;
                Console.WriteLine($"A fierce battle begins! You have HP: {newPlayer.Health} remaining");
                if(newPlayer.Health == 0 || newPlayer.Health < 0)
                {
                    chat.AppendUserInput("the character has died due to their injuries");
                    return "You have died! Game Over.";
                } 

                Console.WriteLine("You have taken more damage!.. yet you still continue...");
                chat.AppendUserInput($" character continues the story from where it left off, the player continues to battle more of the enemy,"
                +" they are suprised an attacked by random enemy"
                + $" character now only has {newPlayer.Health}  health points remaining"
                + $" player has strong emotional reaction to the {EnemyGoal}"
                + $" player encounters the leader {enemyLeaderArray[new Random().Next(0, 9)]} + {EnemyAi}.. one paragraph");
                if (newPlayer.Health > 0)
                {
                    Console.WriteLine("Would you like to continue ? (5)");
                    int response = int.Parse(Console.ReadLine());
                    if (response == 5) ChapterFourAsync();
                    var result = await chat.GetResponseFromChatbotAsync();
                    return result;
                } else
                {
                    return "u dead";
                }
                 // character choice..
                 // interact with NPC (random) to do (random side quest)
                 // if side quest complete + random attributes + health * 3
                 // if side quest fail - health, if health < 0, player dies - game over
           
            }


        }


        async Task<string> ChapterFourAsync() // side quest
        {
            Console.WriteLine("Suddenly... a strange figure approaches..");
            Console.WriteLine("it is a random character! they offer you a quest do you accept?");
            int questResponse = int.Parse(Console.ReadLine());
            if(questResponse == 6)
             {
                chat.AppendUserInput("the player has chosen to accept a side quest to help an npc");
                //string response = "you suck";
                return "";
            }
            return "im not sure...";
        }



        string response = await chat.GetResponseFromChatbotAsync();
        return response;
    }
}

using OpenAI_API;
using simple_console_RPG;


public class AdventureGenerator
{
    // main story points
    public string[] enemyArray = new string[] { "Goblin Army", "Evil Elves", "Savage Wizards", "Barbarians", "Undead Knights", "Ghost Mercenaries", "Lava Lizards with swords", "Fire Dragon", "Corrupt Politicians"};
    public string[] enemyLeaderArray = new string[] { "Council of Elves", "Immortal Wizard", "Tribal Cheiftan", "The forgotten King", "The Dragon shapeshifter", "Prideful General", "The Mad King", "High Priests", "Ancient Pirate King" };
    public string[] locationArray = new string[] { "Castle", "Forest", "Desert", "Swamp", "Rainy Dungeon", "Grassy Village", "Mountain", "Volcano", "Ice Kingdom", "Jungle" };
    public string[] objectiveArray = new string[] { "Save the princess", "Find the keys to the hidden kingdom", "Defeat the emperor", "find who killed your father", "solve the puzzle of the time treasure", "Heal the ancient tree", "uncover the corruption in the city", "help the rebels free their people", "Vanquish the wizard", "Destroy the magic orb" };
    // NPC's
    public string[] npcArray = new string[] { "Wise old person", "young child", "lost banker" };
    // specifics for enemy, locations or objective
    public string[] enemyMotivationArray = new string[] {
        "Environmentalism: The enemy is an environmental extremist who believes that humans are destroying the planet and seeks to stop them at any cost by destroying all of humanity",
        "Religious fundamentalism: The enemy is Mysteriously motivated by religious fundamentalism and seeks to establish a theocratic state based on their particular belief system.",
        "Greed: The enemy is a greedy villian who believes that wealth is the most important goal, even if it means exploiting others and destroying any thing in their path.",
        "Pride: The enemy is a representative of a powerful foreign kindgom that seeks to exploit or dominate the world for their own benefit.",
        "Madness: The enemy is driven insane by dark magic, a curse, or a traumatic event, causing them to lash out against others.",
        "Redemption: The enemy seeks redemption for past misdeeds, but believes that the only way to achieve it is by committing a great act of evil.",
        "Survival: The enemy believs they are justified and is simply trying to survive in a dangerous world and views the players as a threat to their own existence.",
        "Honor: The enemy is Fiercely motivated by a sense of honor or duty, and believes that their actions are justified by a higher moral code.",
        "Independence: The enemy is fighting for independence and autonomy from a larger, more powerful state or kingdom, motivated by a desire for self-determination and sovereignty... they will destroy everything to achieve this",
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
    private readonly int exitCode;

    enum Scenario
    {
        // need to figure out how class benefits character type
        // how does characterClass add to stats
        StrengthBased,
        SpeedBased, 
        LuckBased, 
        MagicBased, 
        HealthBased, 
        Default
    }

    private string _setting { get; set; }
    private string _enemyAI { get; set; }
    private string _goal { get; set; }
    private bool _chapterOneComplete { get; set; }
    private bool _chapterTwoComplete { get; set; }
    private bool _chapterThreeComplete { get; set; }
    private bool _chapterFourComplete { get; set; }
    private string enemyGoal { get; set; }
    private string _combatDescription { get; set; }

    public int Health { get; set; }

    public int HP { get; set; }

    public int Speed { get; set; }

    public int Strength { get; set; }

    public int Range { get; set; }

    public int Magic { get; set; }

    public int Luck { get; set; }

    public int Level { get; set; }

    public int EXP { get; set; }

    public int Dice { get; set; }

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

    private bool ChapterFourComplete
    {
        get => _chapterFourComplete;
        set => _chapterFourComplete = value;             
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


    public string GenerateChapterOutline()
    {
        return "";
    }


    // dont need three dice rolls, one can do the job..
    public async Task<string> GenerateAdventure(int? DiceRollOne, int? DiceRollTwo, int? DiceRollThree, string? Choice, PlayerStats player) // int Location, int Enemy, int Objective
    {
        string apikeyFilePath = "apikey.txt";
        string text = File.ReadAllText(apikeyFilePath);
        OpenAIAPI api = new OpenAIAPI(text);
        var chat = api.Chat.CreateConversation();
        PlayerStats newPlayer = new PlayerStats();
        newPlayer = player;
        Health = player.Health;
        Strength = player.Strength;
        Speed = player.Speed;
        Magic = player.Magic;
        Luck = player.Luck;


        chat.AppendSystemMessage("add to a dungeons and dragons storyline, adventure game, based on [AppendUserInput]"
            + "write in the style of [George RR Martin, The Bible, Tolkein, C.S. Lewis, ]"
            + "use stats of character to update the story.");


        if (DiceRollOne.HasValue && DiceRollTwo.HasValue && DiceRollThree.HasValue)
        {
            Setting = locationArray[(int)DiceRollOne];
            EnemyAi = enemyArray[(int)DiceRollTwo];
            Goal = objectiveArray[(int)DiceRollThree];
            EnemyGoal = enemyMotivationArray[new Random().Next(0, 10)];
            CombatDescription = combatAdverbs[new Random().Next(0, 10)];
        }

        string GenerateChapterOutline()
        {
            string chapterOneOutLine = $"in this phase of the story takes place in {Setting} and the player has just encountered and survived a battle with an {EnemyAi}"
                + $"the battle was {CombatDescription} the player has strong feelings about stopping {EnemyGoal}";
            return chapterOneOutLine;
        }

        //await chat.StreamResponseFromChatbotAsync(res =>
        //{
        //    Console.Write(res);
        //});

        switch (Choice)
        {
            case "chapter two":
                Console.WriteLine("Chapter One complete...");
                string chapterTwo = await ChapterTwoAsync();
                Console.WriteLine(chapterTwo);
                return "";
            case "chapter three":
                Console.WriteLine("Chapter Two complete...");
                chat.AppendExampleChatbotOutput("the first two chapters of the adventure are complete");
                string chapterThree = await ChapterThreeAsync();
                Console.WriteLine(chapterThree);
                Console.ReadLine();
                return "you bravely continue..."; // create a chapter four
            case "chapter four":
                Console.WriteLine("Chapter Three complete...");
                string chapterFour = await ChapterFourAsync();
                Console.WriteLine(chapterFour);
                Console.ReadLine();
                break;
            default:
                chat.AppendSystemMessage(" generate adventure storyline for an RPG game! "
                + " Create a story based on user input such as location, enemy, and objective."
                + $" If the user tells you a location, enemy and {EnemyGoal}, you create the setting for the story based on input. limit 1 paragraph");
                chat.AppendUserInput($"location: {Setting}");
                chat.AppendUserInput($"Enemy: {EnemyAi}");
                chat.AppendUserInput($"Objective: {Goal}");
                ChapterOneComplete = true;
                break;
        }


        async Task<string> ChapterTwoAsync() // handles combat 
        {
            int enemyStrengthLevel = new Random().Next(0, 4);
            int enemySpeedLevel = new Random().Next(0, 4);
            //chat.AppendUserInput($"The enemy objective is described more: {EnemyGoal} ...two sentences");
            Console.WriteLine($"An enemy approaches... Will you run or will you fight? ");
            Console.WriteLine("Enter `run` to run, or `fight` to fignt");
            // choose to run? or choose to fight!
            // "fight" to fight, "run" to run
            string combatChoice = Console.ReadLine();
            if(combatChoice == "fight")
            {
                Console.WriteLine("You have chosen to fight!!... prepare for the upcoming battle...");
                Console.WriteLine($"player has {player.Strength} strength and {player.Health} HP, the other way has {Strength} strength, and {Health} HP");
                Scenario scenario = player.Strength > 3 ? Scenario.StrengthBased :
                             player.Speed > 3 ? Scenario.SpeedBased :
                             player.Magic > 3 ? Scenario.MagicBased :
                             player.Luck > 4 ? Scenario.LuckBased :
                             player.Health > 5 ? Scenario.HealthBased :
                             Scenario.Default;
                switch (scenario)
                {
                    case Scenario.StrengthBased:
                        string res = GenerateChapterOutline();
                        chat.AppendSystemMessage(res);
                        chat.AppendUserInput($"characters encounter and must fight the enemy,"
                            + $"they battle in the {Setting} but the battle is {CombatDescription}"
                            + "player uses strength skil to fight"
                            + $"player has decided becuase of motive to fight typeof:  [single, singular] {EnemyAi} using their typeof: {Goal}"
                            + $"continue the story, player takes some damage in the {CombatDescription} battle, they navigate {Setting} to defeat {EnemyAi} ...four sentences"); ;

                        Health = Health - new Random().Next(0, 10); ;

                        Console.WriteLine($"You have HP: {Health} remaining");
                        Console.WriteLine($"You must use your strength to fight... \n");
                        var endStrengthScenario = await chat.GetResponseFromChatbotAsync();
                        ChapterTwoComplete = true;
                        return endStrengthScenario;

                    case Scenario.SpeedBased:
                        Console.WriteLine("Speed based character...");
                        chat.AppendUserInput($"continue the story.. characters struggle to fight {EnemyAi},"
                       + $"they use their skills but the {EnemyAi} proves to be a real challenge.. but wait!"
                       + "due to speed character takes minor damage, and deals with an injury"
                       + $"luckily, they use their speed to {dodgeVerbs[new Random().Next(0, 10)]} the enemy.. they navigate the {Setting} and get away!"
                       + $"speed has helped the character escape the evil {EnemyAi}.. the journey continues ..four sentences");
                        Health = Health - new Random().Next(0, 10);
                        var endSpeedScenario = await chat.GetResponseFromChatbotAsync();
                        ChapterTwoComplete = true;
                        return endSpeedScenario;


                    case Scenario.MagicBased:
                        Console.WriteLine("You prepare your magic to defeat the upcoming enemy.");
                        Health = Health - new Random().Next(0, 10);

                        Console.WriteLine($"You have HP: {Health} remaining..");
                        chat.AppendUserInput($"continue the story.. characters encounter and must fight one of the {EnemyAi},"
                         + "the player take damage and is injured!.."
                         + $"the battle is {CombatDescription} but the player use their {magicSpells[new Random().Next(0, 8)]}"
                         + $"player is battling in this {Setting} now uses more magic abilities and {magicSpells[new Random().Next(0, 8)]}!.."
                         + $"the battle is magical,chaotic, they try relentlessly to defeat this individual {EnemyAi} they survive and continue their {Goal}.. four sentences");
                        var endMagicScenario = await chat.GetResponseFromChatbotAsync();
                        ChapterTwoComplete = true;
                        return endMagicScenario;

                    case Scenario.Default:
                        Console.WriteLine("you were quite unexceptional and have died. Game Over.");
                        Console.ReadLine();
                        Environment.Exit(exitCode);
                        return "";

                }

            }
            else
            {
                    return "should not be here, fix me right now!! (inside of combat loop)";
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
                 + $"they navigate the {Setting} and use their magic {CombatDescription}, they character uses their magic to solve a mystery in the {Setting} in order to stop main {EnemyAi}.. four sentences");
                var endOfChapter = await chat.GetResponseFromChatbotAsync();
                ChapterThreeComplete = true;
                return endOfChapter;

            } else
            {
                ChapterThreeComplete = true;
                Health = Health - new Random().Next(0, 10);
                Console.WriteLine($"A fierce battle begins! You have HP: {Health} remaining");
                if(Health == 0 || Health < 0)
                {
                    Console.WriteLine("You were neither strong or lucky enough to survive this quest young warrior... you have fallen in battle.");
                    Console.WriteLine("You have lost! Game Over.");
                    chat.AppendUserInput($"Despite their best effort, the player has now died, they have 0 health... their strength, speed, magic was not enough and {EnemyAi} has tapes into their motivation: {enemyMotivationArray[new Random().Next(0, 10)]} and achieves their goal {enemyGoal[new Random().Next(0, 10)]}!  .. one paragraph");
                    chat.AppendUserInput("the character has died due to their injuries");
                    Console.ReadLine();
                    Environment.Exit(exitCode);
                    return "You have died! Game Over.";
                } else
                {
                    Console.WriteLine($"You have 0 HP remaining...");
                    Console.WriteLine("You were neither strong or lucky enough to survive this quest young warrior... you have fallen in battle.");
                    Console.WriteLine("You have lost! Game Over.");
                    chat.AppendUserInput($"Despite their best effort, the player has now died, they have 0 health... their strength, speed, magic was not enough and {EnemyAi} has tapes into their motivation: {enemyMotivationArray[new Random().Next(0, 10)]} and achieves their goal {enemyGoal[new Random().Next(0, 10)]}!  .. one paragraph");
                    Console.ReadLine();
                    Environment.Exit(exitCode);
                    return " ";
                }

                Console.WriteLine("You have taken more damage!.. yet you still continue...");
                chat.AppendUserInput($"continues the story.. from where it left off, the player continues to endure more of the battles,"
                + $" they are suprised an attacked by random {EnemyAi}"
                + $" the player now only has {Health} health points remaining!"
                + $" player has strong emotional reaction to the {EnemyGoal}"
                // {enemyLeaderArray[new Random().Next(0, 9)]}
                + $" player encounters the leader {EnemyAi} motivated by deep  {EnemyGoal} who wants to use the {Setting} for their goal.. two sentences");
                var chapterThreeConclusion = await chat.GetResponseFromChatbotAsync();
                Console.WriteLine(chapterThreeConclusion);
                if (newPlayer.Health > 0)
                {
                    Console.WriteLine("Would you like to continue ? (chapter four)");
                    string response = Console.ReadLine();
                    if (response == "chapter four") {
                        ChapterFourComplete = true;
                        ChapterFourAsync();
                        return "";
                    }  else
                    {
                    Console.WriteLine("the adventure continues... one moment...");
                    var result = await chat.GetResponseFromChatbotAsync();
                    return result;
                    }
                } else
                {
                    Console.WriteLine($"You have 0 HP remaining...");
                    Console.WriteLine("You were neither strong or lucky enough to survive this quest young warrior... you have fallen in battle.");
                    Console.WriteLine("You have lost! Game Over.");
                    chat.AppendUserInput($"Despite their best effort, the player has now died, they have 0 health... their strength, speed, magic was not enough and {EnemyAi} has tapes into their motivation: {enemyMotivationArray[new Random().Next(0, 10)]} and achieves their goal {enemyGoal[new Random().Next(0, 10)]}!  .. one paragraph");
                    Console.ReadLine();
                    Environment.Exit(exitCode);
                    return " ";
                    return "u dead";
                }
           
            }

        }

        async Task<string> ChapterFourAsync() // side quest
        {
            Console.WriteLine("Suddenly... a strange figure approaches..");
            Console.WriteLine("it is a random character! they offer you a quest do you accept? (6)");
            Console.WriteLine("Enter `deny` to deny, or `accept` to accept");

            string questResponse = Console.ReadLine();
            if(questResponse == "accept")
             {
                Console.WriteLine("a new side mission begins... one moment...");
                chat.AppendUserInput("the player has chosen to accept a side quest to help an npc with a unique motivation .. two sentences");
                Console.WriteLine("Press enter to continue to the side quest... ");
                Console.ReadLine();
                chat.AppendUserInput("the player has chosen to accept a side quest to help an npc, using skill to solve a mystery .. four sentences");
                Console.WriteLine($"Speed: {player.Speed}, Magic: {player.Magic}, Strength: {player.Strength}");
                if (player.Speed + player.Strength + player.Magic > new Random().Next(0, 6))
                {
                    // decide on how much to heal player
                    Console.WriteLine($"You have {player.Health} remaining, second variable HP attempt: {Health}");
                    Console.WriteLine("You have found a magic berry that heals your health! + 5 health");
                    Console.WriteLine("Your bravery in adventure has been rewarded, you now have have plus +2 Strengh +1 Speed");

                    Health =+5;
                    Strength =+ 2;
                    Speed =+ 2;

                    Console.WriteLine($"Health: {Health}, Strength: {Strength}, Speed: {Speed}");
                    chat.AppendUserInput("after solving the shocking mystery, player is rewarded! .. two short sentences");
                    return " ";
                }
                else if (newPlayer.Luck > 4)
                {
                    Console.WriteLine($"You have {Health} remaining");
                    Console.WriteLine("You were too weak to fight, yet luckily, magically have found a magic potion that heals your health! + 15 health");
                    Console.WriteLine("Your bravery in adventure has been rewarded, you now have have plus +5 magic +1 strength");

                    Health =+ 15;
                    Magic =+ 5;
                    Strength =+ 1;

                    Console.WriteLine($"Health: {Health}, Magic: {Magic}, Strength: {Strength}");
                    chat.AppendUserInput("player did not use magic, or speed or strength to solve the mystery, they used luck! some how they player helped and recieve a reward! .. three sentences");
                    return " ";
                }
                else
                {
                    Console.WriteLine($"You have 0 HP remaining...");
                    Console.WriteLine("You were neither strong or lucky enough to survive this quest young warrior... you have fallen in battle.");
                    Console.WriteLine("You have lost! Game Over.");
                    chat.AppendUserInput($"Despite their best effort, the player has now died, they have 0 health... their strength, speed, magic was not enough and {EnemyAi} has tapes into their motivation: {enemyMotivationArray[new Random().Next(0, 10)]} and achieves their goal {enemyGoal[new Random().Next(0, 10)]}!  .. one paragraph");
                    Console.ReadLine();
                    Environment.Exit(exitCode);
                    return " ";
                }
            } 
            return "im not sure...";
        }

      

        string response = await chat.GetResponseFromChatbotAsync();
        return response;
    }
}

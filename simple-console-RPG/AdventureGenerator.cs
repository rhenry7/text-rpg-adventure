using OpenAI_API;
using simple_console_RPG;


public class AdventureGenerator
{
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




    // dont need three dice rolls, one can do the job..
    public async  Task <string> GenerateAdventure(int? DiceRollOne, int? DiceRollTwo, int? DiceRollThree, string? Choice, PlayerStats? playerStats) // int Location, int Enemy, int Objective
    {

        // randomizer
        int random = new Random().Next(0, 10);

        // api junk
        string apikeyFilePath = "apikey.txt";
        string text = File.ReadAllText(apikeyFilePath);

        OpenAIAPI api = new OpenAIAPI(text);
        var chat = api.Chat.CreateConversation();

        // story helper classes
        Grammar grammar = new Grammar();
        StorySetup story = new StorySetup();
        StoryParameters storyparams = new StoryParameters();

        // set up
        string[] location = story.Location();
        string[] enemy = story.EnemyType();
        string[] enemyAdjective = story.EnemyAdjective();
        string[] enemyElemental = story.Elemental();
        string[] playerObjective = story.PlayerObjective();
        string[] enemyObjective = story.EnemyObjective();

        // grammar / words
        string[] fight = grammar.GetFightVerbs();
        string[] magicSpell = grammar.MagicSpells();
        string[] combatDescription = grammar.GetCombatAdverbs();
        string[] dodge = grammar.GetDodgeVerbs();

        PlayerStats player = new PlayerStats();

        // player stats
        Health = player.Health;
        Strength = player.Strength;
        Speed = player.Speed;
        Magic = player.Magic;
        Luck = player.Luck;


        if (DiceRollOne.HasValue && DiceRollTwo.HasValue && DiceRollThree.HasValue)
        {
            Setting = location[(int)DiceRollOne];
            EnemyAi = enemy[new Random().Next(enemy.Length)];
            Goal = playerObjective[(int)DiceRollThree];
            EnemyGoal = enemyObjective[new Random().Next(enemyObjective.Length)];
            CombatDescription = combatDescription[new Random().Next(combatDescription.Length)];
        }

        // fix elemental prop

        var storyparam = storyparams.TemplateForStory(Setting, EnemyAi, enemyElemental[random],  EnemyGoal, Goal);
        string secondPhase = storyparams.SecondPhase(Setting, EnemyAi, CombatDescription, random);

        Console.WriteLine("are you ready? (chapter one)");
        string chapterOneStart = Console.ReadLine();
        if(chapterOneStart == "chapter one")
        {
            chat.AppendSystemMessage("roleplay as a dungeon master, story teller to a dungeons and dragons storyline, adventure game"
            + "tell a dramatic and poetic heartful adventure"
            + $"I would like you to use the  {storyparam} as an outline for the story ...one paragraph");
            chat.AppendUserInput(" generate adventure storyline for an RPG game! "
              + " Create a story based on user input such as location, enemy, and objective."
              + $" If the user tells you a location, enemy and {EnemyGoal}, you create the setting for the story based on input. limit 1 paragraph");
            chat.AppendUserInput($"location: {Setting}");
            chat.AppendUserInput($"Enemy: {EnemyAi}");
            chat.AppendUserInput($"Objective: {Goal}");

            string response = await chat.GetResponseFromChatbotAsync();
            Console.WriteLine(response);

            Console.WriteLine("are you ready? (chapter two)");
            string chapterTwoRes = Console.ReadLine();
            if (chapterTwoRes == "chapter two")
            {
            string chapterTwo = await ChapterTwoAsync();
            return chapterTwo;
            }
        }

       


        //switch (Choice)
        //{
        //    case "chapter two":
        //        Console.WriteLine("Chapter One complete...");
        //        string chapterTwo = await ChapterTwoAsync();
        //        Console.WriteLine(chapterTwo);
        //        return "";
        //    case "chapter three":
        //        Console.WriteLine("Chapter Two complete...");
        //        //chat.AppendExampleChatbotOutput("the first two chapters of the adventure are complete");
        //        string chapterThree = await ChapterThreeAsync();
        //        Console.WriteLine(chapterThree);
        //        return ""; // create a chapter four
        //    case "chapter four":
        //        Console.WriteLine("Chapter Three complete...");
        //        string chapterFour = await ChapterFourAsync();
        //        Console.WriteLine(chapterFour);
        //        break;
        //    default:
        //        chat.AppendSystemMessage(" generate adventure storyline for an RPG game! "
        //        + " Create a story based on user input such as location, enemy, and objective."
        //        + $" If the user tells you a location, enemy and {EnemyGoal}, you create the setting for the story based on input. limit 1 paragraph");
        //        chat.AppendUserInput($"location: {Setting}");
        //        chat.AppendUserInput($"Enemy: {EnemyAi}");
        //        chat.AppendUserInput($"Objective: {Goal}");
        //        ChapterOneComplete = true;
        //        break;
        //}


        async Task<string> ChapterTwoAsync() // handles combat 
        {
            int enemyStrengthLevel = new Random().Next(0, 4);
            int enemySpeedLevel = new Random().Next(0, 4);

            await Task.Delay(1000);
            Console.WriteLine($"An enemy approaches... Will you run or will you fight? ");

            await Task.Delay(2000);
            Console.WriteLine("Enter `run` to run, or `fight` to fignt");
            string combatChoice = Console.ReadLine();

            if(combatChoice == "fight")
            {
                await Task.Delay(1000);
                chat.AppendSystemMessage(secondPhase);

                Console.WriteLine("You have chosen to fight!!... prepare for the upcoming battle...");
                Scenario scenario = player.Strength > 3 ? Scenario.StrengthBased :
                             player.Speed > 3 ? Scenario.SpeedBased :
                             player.Magic > 3 ? Scenario.MagicBased :
                             player.Luck > 4 ? Scenario.LuckBased :
                             player.Health > 5 ? Scenario.HealthBased :
                             Scenario.Default;

                switch (scenario)
                {
                    case Scenario.StrengthBased:
                        chat.AppendUserInput($"characters encounter and must fight the enemy,"
                            + $"they battle in the {Setting} but the battle is {CombatDescription}"
                            + $"player uses strength skil in the fight where the {EnemyAi} tried to {fight[random]} them"
                            + $"player has decided becuase of motive to fight typeof:  [single, singular] {EnemyAi} using their typeof: {Goal}"
                            + $"continue the story, player takes some damage in the {CombatDescription} battle, they navigate {Setting} to defeat {EnemyAi}"); ;

                        Health = Health - new Random().Next(3, 10); ;
                        await Task.Delay(1000);
                        Console.WriteLine($"You have HP: {Health} remaining");
                        await Task.Delay(2000);
                        Console.WriteLine($"You must use your strength to fight... \n");
                        var endStrengthScenario = await chat.GetResponseFromChatbotAsync();
                        Console.WriteLine(endStrengthScenario);
                        ChapterTwoComplete = true;
                        return endStrengthScenario;
                        ChapterThreeAsync();


                    case Scenario.SpeedBased:
                        await Task.Delay(1000);
                        Console.WriteLine("you have encountered a " + EnemyAi);
                        Health = Health - new Random().Next(3, 10);
                        await Task.Delay(1000);
                        Console.WriteLine($"You have HP: {Health} remaining...");

                        chat.AppendUserInput($"continue the story.. characters struggle to fight {EnemyAi},"
                       + $"they use their skills but the {EnemyAi} proves to be a real challenge.. but wait!"
                       + "due to speed character takes minor damage, and deals with an injury"
                       + $"luckily, they use their speed to {dodge[new Random().Next(0, 10)]} the enemy.. they navigate the {Setting} and get away!"
                       + $"speed has helped the character escape the evil {EnemyAi}.. the journey continues ..four sentences");
                        Health = Health - new Random().Next(0, 10);
                        var endSpeedScenario = await chat.GetResponseFromChatbotAsync();
                        Console.WriteLine(endSpeedScenario);
                        ChapterTwoComplete = true;
                        return endSpeedScenario;
                        ChapterThreeAsync();


                    case Scenario.MagicBased:
                        await Task.Delay(1000);
                        Console.WriteLine("You prepare your magic to defeat the upcoming enemy...");
                        Health = Health - new Random().Next(3, 10);
                        await Task.Delay(2000);
                        Console.WriteLine($"You have HP: {Health} remaining...");
                        chat.AppendUserInput($"continue the story.. characters encounter and must fight one of the {EnemyAi},"
                         + "the player take damage and is injured!.."
                         + $"the battle is {CombatDescription} but the player use their {magicSpell[new Random().Next(0, 8)]}"
                         + $"player is battling in this {Setting} now uses more magic abilities and {magicSpell[new Random().Next(0, 8)]}!.."
                         + $"the battle is magical,chaotic, they try relentlessly to defeat this individual {EnemyAi} they survive and continue their {Goal}.. four sentences");
                        var endMagicScenario = await chat.GetResponseFromChatbotAsync();
                        ChapterTwoComplete = true;
                        return endMagicScenario;
                        ChapterThreeAsync();

                    case Scenario.Default:
                        await Task.Delay(1000);
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
           
            
                Health = Health - new Random().Next(0, 10);
                Task.Delay(1000);
                Console.WriteLine("You encounter an enemy!");
                Task.Delay(2000);
                Console.WriteLine("A fierce battle begins.. you have taken damage!");
                Task.Delay(3000);
                Console.WriteLine($" You have HP: {Health} remaining");

                if(Health == 0 || Health < 0)
                {
                    Console.WriteLine("You were neither strong or lucky enough to survive this quest young warrior... you have fallen in battle.");
                    Console.WriteLine("You have lost! Game Over.");
                    chat.AppendSystemMessage($"Despite their best effort, the player has now died, they have 0 health... their strength, speed, magic was not enough and {EnemyAi} has tapes into their motivation: {EnemyGoal[new Random().Next(0, 10)]} and achieves their goal {enemyGoal[new Random().Next(0, 10)]}!  .. one paragraph");
                    chat.AppendUserInput("the character has died due to their injuries");
                    Console.ReadLine();
                    Environment.Exit(exitCode);

                    return "You have died! Game Over.";
                }

                await Task.Delay(1000);
                Console.WriteLine("You have taken more damage!.. yet you still continue...");
                chat.AppendUserInput($"continues the story.. from where it left off, the player continues to endure more of the battles,"
                + $" they are suprised an attacked by random {EnemyAi}"
                + $" the player now only has {Health} health points remaining!"
                + $" player has strong emotional reaction to the {EnemyGoal}"
                + $" player encounters the leader {EnemyAi} motivated by deep  {EnemyGoal} who wants to use the {Setting} for their goal.. two sentences");
                return "the adventure continues...";

                ChapterFourAsync();

                // TODO: check if this is where the problem is?
                //string chapterThreeConclusion = await chat.GetResponseFromChatbotAsync();
                //ChapterThreeComplete = true;


            if (Health == 0 || Health < 0)
                {
                    await Task.Delay(1000);
                    Console.WriteLine($"You have 0 HP remaining...");
                    await Task.Delay(1500);
                    Console.WriteLine("You were neither strong or lucky enough to survive this quest young warrior... you have fallen in battle.");
                    await Task.Delay(2000);
                    Console.WriteLine("You have lost! Game Over.");
                    chat.AppendSystemMessage($"Despite their best effort, the player has now died, they have 0 health... their strength, speed, magic was not enough and {EnemyAi} has tapes into their motivation: {EnemyGoal[new Random().Next(0, 10)]} and achieves their goal {enemyGoal[new Random().Next(0, 10)]}!  .. one paragraph");
                    Console.ReadLine();
                    Environment.Exit(exitCode);

                    return "";
                } 
        }

        async Task<string> ChapterFourAsync() // side quest
        {
            Task.Delay(1000);
            Console.WriteLine("Suddenly... a strange figure approaches..");
            Task.Delay(2000);
            Console.WriteLine("it is a random character! they offer you a quest do you accept?");
            Task.Delay(2500);
            Console.WriteLine("Enter `deny` to deny, or `accept` to accept");
            string questResponse = Console.ReadLine();
            if(questResponse == "accept")
             {
                Task.Delay(1000);
                Console.WriteLine("a new side mission begins... one moment...");

                chat.AppendUserInput("the player has chosen to accept a side quest to help an npc with a unique motivation .. two sentences");
                Console.WriteLine("Press enter to continue to the side quest... ");
                Console.ReadLine();

                chat.AppendUserInput("the player has chosen to accept a side quest to help an npc, using skill to solve a mystery .. four sentences");
                Console.WriteLine($"Speed: {player.Speed}, Magic: {player.Magic}, Strength: {player.Strength}");
                if (player.Speed + player.Strength + player.Magic > new Random().Next(3, 6))
                {
                    // decide on how much to heal player
                    string apikeyFilePath = "apikey.txt";
                    string text = File.ReadAllText(apikeyFilePath);
                    OpenAIAPI api = new OpenAIAPI(text);
                    var chat = api.Chat.CreateConversation();
                    PlayerStats newPlayer = new PlayerStats();

                    chat.AppendUserInput($"the character has been rewarded for their kindness and bravery in helping a stranger in the {Setting}"
                        + $"the stranger has rewarded the character with a special kind of magical berry to heal give renewed strength to defeat the {EnemyGoal}"
                        + $"the stranger points the player to the direction of where to defeat the leader of the {EnemyAi} , continuing the story and for the final battle");
                    Console.WriteLine($"You have entered the encounter with {Health} HP.");
                    await Task.Delay(2000);
                    Console.WriteLine("You have found a magic berry that heals your health! + 5 health");
                    await Task.Delay(3000);

                    Console.WriteLine("Your bravery in adventure has been rewarded, you now have have plus +2 Strengh +2 Speed");

                    Health += 5;
                    Strength += 2;
                    Speed += 2;

                    await Task.Delay(4000);
                    Console.WriteLine($"Health: {Health}, Strength: {Strength}, Speed: {Speed}");
                    chat.AppendSystemMessage($"the characters journey in this {Setting} to defeat the {EnemyAi}!... four sentences");

                    string response = await chat.GetResponseFromChatbotAsync();
                    return response;
                }
              
                else
                {
                    await Task.Delay(1000);
                    Console.WriteLine($"You have 0 HP remaining...");

                    await Task.Delay(1000);
                    Console.WriteLine("You were neither strong or lucky enough to survive this quest young warrior... you have fallen in battle.");
                    Console.WriteLine("You have lost! Game Over.");

                    chat.AppendUserInput($"Despite their best effort, the player has now died and failed their {Goal}, they have 0 health... their strength, speed, magic was not enough and {EnemyAi} has tapes into their motivation: {EnemyGoal} and achieves their goal!  .. one paragraph");
                    Console.ReadLine();

                    Environment.Exit(exitCode);

                    return "you died";
                }
            } 
            return "im not sure...";
        }

        return null; 
    }
}

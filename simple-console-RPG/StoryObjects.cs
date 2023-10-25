using System;
using System.Numerics;
using Microsoft.Extensions.Options;
using OpenAI_API;
using simple_console_RPG;

namespace simple_console_RPG


{

	public class StoryObjects
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
        private string _enemyLeader { get; set; }
        private string _goal { get; set; }
        private string _chapterOneComplete { get; set; }
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

        public int _fightChoice { get; set; }

        public string _storyInto { get; set; }
        public string _battleDetail { get; set; }
        public string _bossEncounter1 { get; set; }
        public string _bossEncounter2 { get; set; }
        public string _npcEncounter1 { get; set; }



        public string StoryInfo
        {
            get => _storyInto;
            set => _storyInto = value;
        }

        public string EnemyLeader
        {
            get => _enemyLeader;
            set => _enemyLeader = value;
        }

        public string BattleDetail
        {
            get => _battleDetail;
            set => _battleDetail = value;
        }

        public string BossEncounter1
        {
            get => _bossEncounter1;
            set => _bossEncounter1 = value;

        }

        public string BossEncounter2
        {
            get => _bossEncounter2;
            set => _bossEncounter2 = value;

        }

        public string NPCEncounter1
        {
            get => _npcEncounter1;
            set => _npcEncounter1 = value;

        }

        private string ChapterOneComplete
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

        private int FightChoice
        {
            get => _fightChoice;
            set => _fightChoice = value;
        }



        private PlayerStats newPlayer;

        public void PlayerChoice()
        {

            Console.WriteLine("3 - for yes");
            Console.WriteLine("4 - for no");
            FightChoice = int.Parse(Console.ReadLine());
            if (FightChoice == 3)
            {
                Console.WriteLine("\n");
            }
            else if (FightChoice == 4)
            {
                Console.WriteLine("you suck");
                Console.ReadLine();
                Environment.Exit(exitCode);
                Console.WriteLine("\n");
            }
            else
            {
                Console.WriteLine("you suck");

            }

        }



public async Task<ChatResult> CreateChatCompletionAsync(params ChatRequest[] messages) 
{
var result = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
{
    Model = Model.ChatGPTTurbo,
    Temperature = 0.1,
    MaxTokens = 50,
    Messages = new ChatMessage[] {
            new ChatMessage(ChatMessageRole.User, "Hello!")
        }
});
			return CreateChatCompletionAsync(request);       
             }

        public async Task<StoryObjects> Intro(Task<PlayerStats> player)
        {

            this.newPlayer = await player;
   

            Console.WriteLine($"stats ->  strength: {newPlayer.Strength}, speed: {newPlayer.Speed}, health: {newPlayer.Health} ");

            // randomizer
            int random = new Random().Next(0, 10);

            // api junk
            string apikeyFilePath = "apikey.txt";
            string text = File.ReadAllText(apikeyFilePath);

            OpenAIAPI api = new OpenAIAPI(text);
            //var chat = api.Chat.CreateConversation();
            
            

            // story helper classes
            Grammar grammar = new Grammar();
            StorySetup story = new StorySetup();
            StoryParameters storyparams = new StoryParameters();

            // set up
            string[] location = story.Location();
            string[] enemy = story.EnemyType();
            string[] enemyAdjective = story.EnemyAdjective();
            string[] enemyLeader = story.EnemyLeader();
            string[] enemyElemental = story.Elemental();
            string[] playerObjective = story.PlayerObjective();
            string[] enemyObjective = story.EnemyObjective();

            // grammar / words
            string[] fight = grammar.GetFightVerbs();
            string[] magicSpell = grammar.MagicSpells();
            string[] combatDescription = grammar.GetCombatAdverbs();
            string[] dodge = grammar.GetDodgeVerbs();



            Setting = location[new Random().Next(location.Length)];

            Console.WriteLine($"For your roll.., your story location is: {Setting} \n");

            EnemyAi = enemy[new Random().Next(enemy.Length)];

            Console.WriteLine($"For your roll.., your story enemy is: {EnemyAi} \n");

            EnemyLeader = enemyLeader[new Random().Next(enemyLeader.Length)];

            Console.WriteLine($"For your roll.., the enemy leader is: {EnemyLeader}  \n");

            Goal = playerObjective[new Random().Next(playerObjective.Length)];
            EnemyGoal = enemyObjective[new Random().Next(enemyObjective.Length)];
            CombatDescription = combatDescription[new Random().Next(combatDescription.Length)];

            var storyparam = storyparams.TemplateForStory(Setting, EnemyAi, enemyElemental[random], EnemyGoal, Goal);

            var res = CreateChatCompletionAsync(storyparam);
            Console.WriteLine(res);


            // USE AS "GROUNDING" FOR SETTING THE STAGE
            //             chat.AppendSystemMessage("roleplay as a dungeon master, story teller to a dungeons and dragons storyline, adventure game"
            //             + $"use {newPlayer.Name} as the name of the main character"
            //             + "tell a dramatic and poetic heartful adventure"
            //             + "write the beginning, epic intro of an adventure story"
            //               + "use character development in the style of George RR Martin"
            //             + "use action description in the style of Hemingway"
            //             + "use as chapter 1 of 3 part story"
            //             + "Write 5 sentences with up to 15 words each"
            //             + $"use this {storyparam}  + {EnemyAi}  + {Goal}  + {Setting} as an first act outline for the story"
            //             + $"use this {storyparam} as an outline for the story");

            // await foreach (var res in chat.StreamResponseEnumerableFromChatbotAsync())
            // {
            // 	Console.WriteLine(res);
            // }

            // string response = await chat.GetResponseFromChatbotAsync();
            // StoryInfo = response;
            // ChapterOneComplete = response;
            // Console.WriteLine(response);

            //     var result = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
            //     {
            //         Model = Model.ChatGPTTurbo,
            //         Temperature = 0.1,
            //         MaxTokens = 50,
            //         Messages = new ChatMessage[] {
            //     new ChatMessage(ChatMessageRole.User, storyparam)
            // }
            //     });

            // Console.WriteLine(storyparam);

            Console.WriteLine("chapter 1 end");

            await Chapter1(newPlayer);
            return this;

        }


        public async Task<StoryObjects> Chapter1(PlayerStats playerstats)
        {

            Console.WriteLine("\n");
            Task.Delay(1000).Wait();
            Console.WriteLine("chapter one begins... \n");
            Task.Delay(3000).Wait();
            Console.WriteLine($"Battle stats: strength: {newPlayer.Strength}, speed: {newPlayer.Speed}, health: {newPlayer.Health} ");

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
            string[] enemyLeader = story.EnemyLeader();

            string[] enemyElemental = story.Elemental();
            string[] playerObjective = story.PlayerObjective();
            string[] enemyObjective = story.EnemyObjective();

            // grammar / words
            string[] fight = grammar.GetFightVerbs();
            string[] magicSpell = grammar.MagicSpells();
            string[] combatDescription = grammar.GetCombatAdverbs();
            string[] dodge = grammar.GetDodgeVerbs();

            string Leader = enemyLeader[new Random().Next(3, 8)];

            Task.Delay(2000).Wait();
            Console.WriteLine("An enemy approaches! \n");

            //int promptResponse = Prompt("would you like to fight? enter fight to continue", "fight", "run");
            string[] options = new string[] { "fight", "run" };

            Task.Delay(1000).Wait();
            Console.WriteLine("would you like to fight?");
            Task.Delay(2000).Wait();
            PlayerChoice();


            Task.Delay(3000).Wait();
            Console.WriteLine("You have chosen bravely... prepare yourself for battle...");

            while (true)
                {

                    Task.Delay(2000).Wait();
                    Console.WriteLine("The battle begins! ");

                    Task.Delay(2000).Wait();
                    Scenario scenario = newPlayer.Strength > 3 ? Scenario.StrengthBased :
                                 newPlayer.Speed > 3 ? Scenario.SpeedBased :
                                 newPlayer.Magic > 3 ? Scenario.MagicBased :
                                 newPlayer.Luck > 4 ? Scenario.LuckBased :
                                 newPlayer.Health > 5 ? Scenario.HealthBased :
                                 Scenario.Default;

                    switch (scenario)
                    {
                        case Scenario.StrengthBased:
                        chat.AppendSystemMessage(StoryInfo);
                        chat.AppendUserInput($"this is just half way into the adventure... the journey continues");
                        chat.AppendUserInput($"at this point in the story, characters encounter and must fight the enemy,"
                                + $"they battle in the {Setting} but the battle is {CombatDescription}"
                                + "player uses strength skil to fight"
                                + $"player has decided becuase of motive to fight typeof:  [single, singular] {EnemyAi} using their typeof: {Goal}"
                                + $"continue the story, player takes some damage in the {CombatDescription} battle, they navigate {Setting} to defeat one of the {EnemyAi}, the leader is still out there  ...one paragraph sentences"
                                + "the journey continues... the fight goes on..");


                            Task.Delay(2000).Wait();
                            Console.WriteLine($"Prepare your strength to fight... \n");

                        //var endStrengthScenario = await chat.GetResponseFromChatbotAsync();
                        //Console.WriteLine(endStrengthScenario);
                        //BattleDetail = endStrengthScenario;

                        Console.WriteLine("battle scenario");
                            newPlayer.Health -= new Random().Next(3, 10); ;
                            Task.Delay(2000).Wait();
                            Console.WriteLine($"You have HP: {newPlayer.Health} remaining");
                            ChapterTwoComplete = true;
                            await Chapter2();
                            return this;

                        case Scenario.SpeedBased:
                            Console.WriteLine("Speed based character...");
                            chat.AppendSystemMessage(StoryInfo);
                            chat.AppendUserInput($"continue the story.. characters struggle to fight {EnemyAi},"
                           + $"they use their skills but the {EnemyAi} proves to be a real challenge.. but wait!"
                           + "due to speed character takes minor damage, and deals with an injury"
                           + $"luckily, they use their speed to {dodge[new Random().Next(0, 10)]} the enemy.. they navigate the {Setting} and get away!"
                           + $"speed has helped the character escape the evil {EnemyAi}.. the journey continues ..four sentences");
                             var endSpeedScenario = await chat.GetResponseFromChatbotAsync();
                            Console.WriteLine(endSpeedScenario);
                            BattleDetail = endSpeedScenario;
                               newPlayer.Health -= new Random().Next(3, 10);
                                Console.WriteLine($"You have HP: {newPlayer.Health} remaining");

                            ChapterTwoComplete = true;
                        await Chapter2();
                        return this;


                        case Scenario.MagicBased:
                            chat.AppendSystemMessage(StoryInfo);
                            Console.WriteLine("You prepare your magic to defeat the upcoming enemy.");
                            Task.Delay(2000).Wait();
                            Console.WriteLine($"You have HP: {Health} remaining..");

                            chat.AppendUserInput($"continue the story.. characters encounter and must fight one of the {EnemyAi},"
                             + "the player take damage and is injured!.."
                             + $"the battle is {CombatDescription} but the player use their {magicSpell[new Random().Next(0, 8)]}"
                             + $"player is battling in this {Setting} now uses more magic abilities and {magicSpell[new Random().Next(0, 8)]}!.."
                             + $"the battle is magical,chaotic, they try relentlessly to defeat this individual {EnemyAi} they survive and continue their {Goal}.. four sentences");
                            var endMagicScenario = await chat.GetResponseFromChatbotAsync();
                            Console.WriteLine(endMagicScenario);
                            BattleDetail = endMagicScenario;
                            newPlayer.Health -= new Random().Next(3, 10);
                            Console.WriteLine($"You have HP: {newPlayer.Health} remaining");
                            ChapterTwoComplete = true;
                        await Chapter2();
                        return this;

                        case Scenario.Default:
                            Console.WriteLine("you were quite unexceptional and have died. Game Over.");
                            Console.ReadLine();
                            Environment.Exit(exitCode);
                        await Chapter2();
                        return this; ;
                    }
                    Console.WriteLine("Invalid choice. Please try again.");


                }



   
            return this;
        }   

        public async Task<StoryObjects> Chapter2()
        {

            // TODO: add some interacting aspect to this section
            // add one or two if checks
            // find inventory armor that adds to health

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

            StoryParameters nextPhase = new StoryParameters();

            Task.Delay(1000).Wait();
            Console.WriteLine("you have made progress in your adventure... \n");

            Task.Delay(1000).Wait();
            Console.WriteLine("would you like to investigate your surroundings?");

            Task.Delay(2000).Wait();
            PlayerChoice();

            Task.Delay(2000).Wait();
            Console.WriteLine("a new enemy approaches!... \n");

            Task.Delay(2500).Wait();
            Console.WriteLine("prepare for an enemy encounter!... \n");

            chat.AppendSystemMessage(StoryInfo + BattleDetail);

            // use a pre made template function for the append input of this section
            chat.AppendUserInput(nextPhase.ThirdPhase(Setting, EnemyAi, combatDescription[new Random().Next(2, 8)], new Random().Next(1,4)));
            chat.AppendUserInput($"write an update to the adventure, where the player must battle and defeat some of the {EnemyAi} and approach the enemy leader,"
            + $"write and describe the conflict and tension of the story begin to build. The player now faces obstacles and challenges that make it increasingly difficult to achieve their {Goal}"
            + $"they use their skills but the {EnemyAi} and their leader {EnemyLeader} proves to be a real challenge.. "
            + $"luckily, they use their speed to {dodge[new Random().Next(0, 10)]} the enemy.. they navigate the {Setting} and get away!"
            + $"the evil {EnemyLeader} leader.. the journey continues ..one paragraph \n");
            //var Chapter2Conclusion = await chat.GetResponseFromChatbotAsync();

            //BossEncounter1 = Chapter2Conclusion;

            //Console.WriteLine(Chapter2Conclusion);
            //BattleDetail = Chapter2Conclusion;


            Console.WriteLine("chapter 2 end");

            newPlayer.Health -= new Random().Next(3, 10);
            Task.Delay(1000).Wait();
            Console.WriteLine("you are wounded!... \n");

            Task.Delay(2000).Wait();
            Console.WriteLine($"You have HP: {newPlayer.Health} remaining \n");


            Task.Delay(1000).Wait();
            Console.WriteLine($"somehow have survived, but you must continue your journey to {Goal}... \n");

            ChapterTwoComplete = true;

            await Chapter3Async();
            return this;
        }

        public async Task<StoryObjects> Chapter3Async()
        {
            // NPC encounter
            // receive berry for health and speed or inv item to increase defense 
            // if luck is high, receive both

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
            string[] enemyLeader = story.EnemyLeader();
            string[] npcEncounter = story.NPC();
            string[] enemyAdjective = story.EnemyAdjective();
            string[] enemyElemental = story.Elemental();
            string[] playerObjective = story.PlayerObjective();
            string[] enemyObjective = story.EnemyObjective();

            // grammar / words
            string[] fight = grammar.GetFightVerbs();
            string[] magicSpell = grammar.MagicSpells();
            string[] combatDescription = grammar.GetCombatAdverbs();
            string[] dodge = grammar.GetDodgeVerbs();

            Task.Delay(1000).Wait();
            Console.WriteLine("Suddenly... a strange figure approaches..");
            Task.Delay(2000).Wait();
            Console.WriteLine("it is a random character! they offer you a quest do you accept?");
            Task.Delay(2500).Wait();
            Console.WriteLine("Enter `deny` to deny, or `accept` to accept");
            PlayerChoice();

            Task.Delay(1000).Wait();
            Console.WriteLine("a new side mission begins... one moment...");

            Console.WriteLine("Press enter to continue to the side quest... ");
            Console.ReadLine();

            chat.AppendSystemMessage(StoryInfo + BattleDetail + BossEncounter1);
            chat.AppendUserInput($" update story so that the player encounters a stranger and write a side quest for the story"
            + $"in the side quest the player must help a {npcEncounter[new Random().Next(npcEncounter.Length)]}"
            + $"write an outcome where the player is rewarded with a magic berry that heals them"
            + $"after helping the player continues the journey to achieve {Goal} and {EnemyGoal} and save the {Setting}.. player prepares for final upcoming battle..one paragraph \n");

            //var chapter3end = await chat.GetResponseFromChatbotAsync();
            //Console.WriteLine(chapter3end);

            Console.WriteLine("chapter 3 end");

            Task.Delay(2000).Wait();
            Console.WriteLine("You have found a magic berry that heals your health! + 5 health \n");
            Task.Delay(3000).Wait();

            Console.WriteLine("Your bravery in adventure has been rewarded, you now have have plus +2 Strengh +2 Speed \n");

            newPlayer.Health += 5;
            newPlayer.Strength += 2;
            newPlayer.Speed += 2;

            Task.Delay(2000).Wait();
            Console.WriteLine($"stats ->  strength: {newPlayer.Strength}, speed: {newPlayer.Speed}, health: {newPlayer.Health} \n");

            Task.Delay(1000).Wait();
            Console.WriteLine("Prepare for the final battle... \n");

            Console.WriteLine("Press enter to continue... ");
            Console.ReadLine();
            await Chapter4();
            return this;
        }

        // add a shop function? use coins to buy an item,
        // choose from list, list item increase player stat 

        public async Task<StoryObjects> Chapter4()
        {
            // story conclusion
            // player achieves goal
            // the end
            Console.WriteLine("The end..");
            return this;
        }

    

    }
}


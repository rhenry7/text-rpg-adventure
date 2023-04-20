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

        public string _storyInto { get; set; }

        public int _fightChoice { get; set; }

        public string StoryInfo
        {
            get => _storyInto;
            set => _storyInto = value;
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

        public void UseFightOptions()
        {

            Console.WriteLine("Would you like to fight...");
            Console.WriteLine("3 - for yes");
            Console.WriteLine("4 - for no");
            FightChoice = int.Parse(Console.ReadLine());
            if (FightChoice == 3)
            {
                Console.WriteLine("\n");
            }
            else
            {
                Console.WriteLine("you suck");

            }

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


          

            Setting = location[new Random().Next(location.Length)];
            EnemyAi = enemy[new Random().Next(enemy.Length)];
            Goal = playerObjective[new Random().Next(playerObjective.Length)];
            EnemyGoal = enemyObjective[new Random().Next(enemyObjective.Length)];
            CombatDescription = combatDescription[new Random().Next(combatDescription.Length)];

            var storyparam = storyparams.TemplateForStory(Setting, EnemyAi, enemyElemental[random], EnemyGoal, Goal);

            chat.AppendSystemMessage("roleplay as a dungeon master, story teller to a dungeons and dragons storyline, adventure game"
            + "tell a dramatic and poetic heartful adventure"
            + $"I would like you to use the  {storyparam} as an outline for the story");
            chat.AppendUserInput(" generate the beginning of an adventure storyline for an RPG game! "
              + " Create a story based on user input such as location, enemy, and objective."
              + $" If the user tells you a location, enemy and {EnemyGoal}, you create the setting for the story based on input. ..one paragraph");
            chat.AppendUserInput($"location: {Setting}");
            chat.AppendUserInput($"Enemy: {EnemyAi}");
            chat.AppendUserInput($"Objective: {Goal}");
            chat.AppendUserInput($"Objective: {Goal}");
            chat.AppendUserInput($"this is just the beginning... the journey continues");


            string response = await chat.GetResponseFromChatbotAsync();
            StoryInfo = response;
            ChapterOneComplete = response;
            Console.WriteLine(response);
            await Chapter1(newPlayer);
            return this;

        }


        //public async Task<StoryObjects> Chapter1(PlayerStats playerstats)
        //{
        //    Console.WriteLine("Would you like to fight...");
        //    Console.WriteLine("3 - for yes");
        //    Console.WriteLine("4 - for no");
        //    FightChoice = int.Parse(Console.ReadLine());
        //    if (FightChoice == 3)
        //    {
        //        Console.WriteLine("\n");
        //    }
        //    else
        //    {
        //        Console.WriteLine("you suck");

        //    }
        //}




        public async Task<StoryObjects> Chapter1(PlayerStats playerstats)
        {

            Console.WriteLine("\n");
            Task.Delay(2000).Wait();
            Console.WriteLine("in chapter one \n");
            Task.Delay(2000).Wait();
            Console.WriteLine($"stats ->  strength: {newPlayer.Strength}, speed: {newPlayer.Speed}, health: {newPlayer.Health} ");

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

            //int promptResponse = Prompt("would you like to fight? enter fight to continue", "fight", "run");
            string[] options = new string[] { "fight", "run" };

            Task.Delay(2000).Wait();
            Console.WriteLine("would you like to fight? enter fight to continue", "fight", "run");

            Console.WriteLine("Enter `run` to run, or `fight` to fignt");
            UseFightOptions();

                while (true)
                {

                    Task.Delay(1000).Wait();
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
                            var endStrengthScenario = await chat.GetResponseFromChatbotAsync();
                            Task.Delay(2000).Wait();
                            newPlayer.Health -= new Random().Next(3, 10); ;
                            Console.WriteLine($"You have HP: {newPlayer.Health} remaining");
                            Console.WriteLine(endStrengthScenario);
                            ChapterTwoComplete = true;
                            return this;

                        case Scenario.SpeedBased:
                            Console.WriteLine("Speed based character...");
                            chat.AppendSystemMessage(StoryInfo);
                            chat.AppendUserInput($"continue the story.. characters struggle to fight {EnemyAi},"
                           + $"they use their skills but the {EnemyAi} proves to be a real challenge.. but wait!"
                           + "due to speed character takes minor damage, and deals with an injury"
                           + $"luckily, they use their speed to {dodge[new Random().Next(0, 10)]} the enemy.. they navigate the {Setting} and get away!"
                           + $"speed has helped the character escape the evil {EnemyAi}.. the journey continues ..four sentences");
                            Health = Health - new Random().Next(0, 10);
                            var endSpeedScenario = await chat.GetResponseFromChatbotAsync();
                            Console.WriteLine(endSpeedScenario);

                            ChapterTwoComplete = true;
                            return this;


                        case Scenario.MagicBased:
                            chat.AppendSystemMessage(StoryInfo);
                            Console.WriteLine("You prepare your magic to defeat the upcoming enemy.");
                            Health = Health - new Random().Next(0, 10);
                            Task.Delay(2000).Wait();
                            Console.WriteLine($"You have HP: {Health} remaining..");

                            chat.AppendUserInput($"continue the story.. characters encounter and must fight one of the {EnemyAi},"
                             + "the player take damage and is injured!.."
                             + $"the battle is {CombatDescription} but the player use their {magicSpell[new Random().Next(0, 8)]}"
                             + $"player is battling in this {Setting} now uses more magic abilities and {magicSpell[new Random().Next(0, 8)]}!.."
                             + $"the battle is magical,chaotic, they try relentlessly to defeat this individual {EnemyAi} they survive and continue their {Goal}.. four sentences");
                            var endMagicScenario = await chat.GetResponseFromChatbotAsync();
                            Console.WriteLine(endMagicScenario);
                            ChapterTwoComplete = true;
                            return this;

                        case Scenario.Default:
                            Console.WriteLine("you were quite unexceptional and have died. Game Over.");
                            Console.ReadLine();
                            Environment.Exit(exitCode);
                            return this; ;
                    }
                    Console.WriteLine("Invalid choice. Please try again.");


                }

            
          
            
            return this;
        }   

        public StoryObjects Chapter2()
        {
            Console.WriteLine("Method2");
            return this;
        }

        public StoryObjects Chapter3()
        {
            Console.WriteLine("Method3");
            return this;
        }

        public StoryObjects Chapter4()
        {
            Console.WriteLine("Method4");
            return this;
        }

    

    }
}


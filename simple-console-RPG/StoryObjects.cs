using System;
using System.Numerics;
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
        
        public async Task<StoryObjects> Intro(PlayerStats playerStats)
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

            Setting = location[new Random().Next(location.Length)];
            EnemyAi = enemy[new Random().Next(enemy.Length)];
            Goal = playerObjective[new Random().Next(playerObjective.Length)];
            EnemyGoal = enemyObjective[new Random().Next(enemyObjective.Length)];
            CombatDescription = combatDescription[new Random().Next(combatDescription.Length)];

            var storyparam = storyparams.TemplateForStory(Setting, EnemyAi, enemyElemental[random], EnemyGoal, Goal);

            chat.AppendSystemMessage("roleplay as a dungeon master, story teller to a dungeons and dragons storyline, adventure game"
            + "tell a dramatic and poetic heartful adventure"
            + $"I would like you to use the  {storyparam} as an outline for the story");
            chat.AppendUserInput(" generate the first half of anadventure storyline for an RPG game! "
              + " Create a story based on user input such as location, enemy, and objective."
              + $" If the user tells you a location, enemy and {EnemyGoal}, you create the setting for the story based on input. ..one paragraph");
            chat.AppendUserInput($"location: {Setting}");
            chat.AppendUserInput($"Enemy: {EnemyAi}");
            chat.AppendUserInput($"Objective: {Goal}");

            string response = await chat.GetResponseFromChatbotAsync();
            StoryInfo = response;
            ChapterOneComplete = response;
            Console.WriteLine(response);

          
            return this;
        }


        public async Task<StoryObjects> Chapter1()
		{
            Console.WriteLine("in chapter one");
            // api junk
            string apikeyFilePath = "apikey.txt";
            string text = File.ReadAllText(apikeyFilePath);

            OpenAIAPI api = new OpenAIAPI(text);
            var chat = api.Chat.CreateConversation();

            chat.AppendSystemMessage(ChapterOneComplete);
            chat.AppendUserInput("the character has endured a rough battle, taken damage, and must now continue to the second half of the journey");
            string storySetUp = await chat.GetResponseFromChatbotAsync();
            Console.WriteLine("made it here?");
            Console.WriteLine(storySetUp);

            //Console.WriteLine("Method2");
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

        public async Task ChainMethodsAsync(PlayerStats playerStats)
        {
            StoryObjects storyObjects = await Intro(playerStats);
            StoryObjects anotherObject = await Chapter1();

            // Do something with the results
        }

    }
}


using simple_console_RPG;

//using Internal;
public class CharacterStats
{
    // character basics
    public string Name { get; set; }

    public int StartingClass { get; set; }

    public string CharacterClass { get; set; }

    public int StartingLevel { get; set; }

    // attributes
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

    public int DiceValue { get; set; }

    public string? RollDiceChoice { get; set; }

    // starting bonuses per class, in menu order (health is in HP, 1 point = 10 HP)
    private static readonly (string Name, int Health, int Speed, int Strength, int Magic, int Luck)[] Classes =
    {
        ("Elf",       0,  4, 2, 0, 0),
        ("Mage",      0,  0, 0, 6, 0),
        ("Hero",      0,  2, 5, 0, 0),
        ("Hobbit",    0,  0, 0, 0, 6),
        ("Dwarf",     0,  0, 8, 0, 0),
        ("Barbarian", 10, 0, 5, 0, 0),
        ("Thief",     0,  5, 0, 0, 1),
    };

    public void PrintOptions()
    {
        Console.WriteLine("Choose your starting class...");
        Task.Delay(1000);
        Console.WriteLine("1. Elf - +4 speed, +2 strength,"); // range; speed;
        Task.Delay(1000);
        Console.WriteLine("2. Mage - +6 magic"); // magic;
        Task.Delay(1000);
        Console.WriteLine("3. Hero - +5 strength, +2 speed"); // strength; speed;
        Task.Delay(1000);
        Console.WriteLine("4. Hobbit - +6 luck"); // luck;
        Task.Delay(1000);
        Console.WriteLine("5. Dwarf - +8 strength;"); // strengh;
        Task.Delay(1000);
        Console.WriteLine("6. Barbarian - +1 HP, +5 strength"); // hp; strength;
        Task.Delay(1000);
        Console.WriteLine("7. Thief - +5 speed, +1 luck"); // speed; luck;
        Task.Delay(1000);

        // accept either the number or the class name, and ask again on anything else
        while (true)
        {
            string input = GameInput.ReadLineOrQuit();
            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= Classes.Length)
            {
                StartingClass = choice;
                break;
            }

            int byName = Array.FindIndex(Classes, c => c.Name.Equals(input, StringComparison.OrdinalIgnoreCase));
            if (byName >= 0)
            {
                StartingClass = byName + 1;
                break;
            }

            Console.WriteLine($"Please enter a number from 1 to {Classes.Length}, or a class name.");
        }
        Console.WriteLine("\n");
    }

    // reads a whole number between min and max, asking again until it gets one
    private static int ReadInt(int min, int max)
    {
        while (true)
        {
            string input = GameInput.ReadLineOrQuit();
            if (int.TryParse(input, out int value) && value >= min && value <= max)
            {
                return value;
            }
            Console.Write($"Please enter a number from {min} to {max}: ");
        }
    }

    public async Task<PlayerStats> CreateCharacter()
    {
        Console.WriteLine("Enter your character name ");
        Name = (Console.ReadLine() ?? "").Trim();
        if (Name == "")
        {
            Name = "Adventurer";
        }
        Console.WriteLine("\n");

        Task.Delay(1000);
        Console.WriteLine($"Welcome {Name}! \n");
        PrintOptions();

        {
            var chosen = Classes[StartingClass - 1];
            CharacterClass = chosen.Name;
            PlayerStats playerStats = new PlayerStats();
            playerStats.Health = chosen.Health;
            playerStats.Speed = chosen.Speed;
            playerStats.Strength = chosen.Strength;
            playerStats.Magic = chosen.Magic;
            playerStats.Luck = chosen.Luck;

            Console.WriteLine($"You have chosen {CharacterClass}! \n");

            var pointsToSpend = 10;
            int pointsRemaining = 10;
            var stats =
                new [] { "Health", "Speed", "Strength", "Magic", "Luck" };
            var statsValues = new int[stats.Length];

            Task.Delay(1000);
            Console.WriteLine($"You have {pointsToSpend} points to spend. \n");
            Task.Delay(2000);
            Console.WriteLine("These are your stats!");
            Task.Delay(3000);
            Console.WriteLine($"Health: {playerStats.Health}");
            Console.WriteLine($"Speed: {playerStats.Speed}");
            Console.WriteLine($"Strength: {playerStats.Strength}");
            Console.WriteLine($"Magic: {playerStats.Magic}");
            Console.WriteLine($"Luck: {playerStats.Luck}");

            //AdventureGenerator story = new AdventureGenerator();
            for (int i = 0; i < stats.Length; i++)
            {
                Console.Write($"{stats[i]}: ");
                statsValues[i] += ReadInt(0, pointsRemaining);
                pointsRemaining = pointsToSpend -= statsValues[i];

                if (pointsRemaining == 0)
                {
                    Console.WriteLine($"You have ran out of points \n");
                    break;
                }

                Console
                    .WriteLine($"Remaining points to spend: {
                        pointsRemaining} \n");
            }

            playerStats.UpdateStats (statsValues, Name, CharacterClass);

            Task.Delay(1000);
            Console.WriteLine("These are your stats! \n");

            Task.Delay(1000);
            Console.WriteLine($"Health: {playerStats.Health}");

            Task.Delay(1000);
            Console.WriteLine($"Speed: {playerStats.Speed}");

            Task.Delay(1000);
            Console.WriteLine($"Strength: {playerStats.Strength}");

            Task.Delay(1000);
            Console.WriteLine($"Magic: {playerStats.Magic}");

            Task.Delay(1000);
            Console.WriteLine($"Luck: {playerStats.Luck} \n");

            RollDiceOptions();
            Task.Delay(1000).Wait();
            int diceRollOne = RollDice();
            int diceRolltwo = RollDice();
            int diceRollthree = RollDice();

            Console.WriteLine("one moment while we create your story... \n");

            //Console.WriteLine("What will you do now?... (chapter two) \n");
            //string firstChoice = Console.ReadLine(); // enter 3
            //string chapterThree = story.GenerateAdventure(null, null, null, firstChoice, playerStats).Result;
            //Console.WriteLine(chapterThree);
            //Console.WriteLine("What will you do now?... (chapter three) \n");
            //string secondChoice = Console.ReadLine(); // enter 4
            //string chapterFour = story.GenerateAdventure(null, null, null, secondChoice, null).Result;
            //Console.WriteLine("What will you do now?... (chapter four) \n");
            //string thirdChoice = Console.ReadLine(); // chapter four
            //string chapterFive = story.GenerateAdventure(null, null, null, thirdChoice, null).Result;
            //Console.WriteLine(chapterFive);
            //Console.WriteLine(chapterFour);
            return playerStats;
        }
    }

    public int RollDice()
    {
        return new Random().Next(0, 9);
    }

    public void RollDiceOptions()
    {
        Console.WriteLine("Would you like to roll the dice...");
        Console.WriteLine("2 - for yes");
        Console.WriteLine("1 - for no");
        RollDiceChoice = Console.ReadLine();
        if (RollDiceChoice == "2")
        {
            Console.WriteLine("\n");
        }
        else
        {
            Console.WriteLine("you suck");
        }
    }
}

using simple_console_RPG;

//using Internal;
public class CharacterStats

{
    // character basics
    public string? Name { get; set; }

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

    public void PrintOptions()
    {
        Console.WriteLine("Choose your starting class...");
        Console.WriteLine("1. Elf - range, speed;"); // range; speed;
        Console.WriteLine("2. Mage - magic"); // magic;
        Console.WriteLine("3. Hero - strength, speed"); // strength; speed;
        Console.WriteLine("4. Hobbit - luck"); // luck;
        Console.WriteLine("5. Dwarf - strengt;"); // strengh;
        Console.WriteLine("6. Barbarian - hp, strength"); // hp; strength;
        Console.WriteLine("7. Theif - speed, luck"); // speed; luck;        StartingClass = int.Parse(Console.ReadLine());
        StartingClass = int.Parse(Console.ReadLine());
        Console.WriteLine("\n");
    }

    public CharacterStats? CreateCharacter()
    {
        Console.WriteLine("Enter your character name ");
        Name = Console.ReadLine();
        Console.WriteLine($"Welcome {Name}!");
        PrintOptions();
        //Console.ReadLine();
        if (StartingClass == 1)
        {
            CharacterClass = "Elf";
            var elf = new CharacterStats();
            Console.WriteLine("You have chosen Elf! \n");
            var pointsToSpend = 10;
            int pointsRemaining = 10;
            var stats = new[] { "Health", "Speed", "Strength", "Magic", "Luck" };
            var statsValues = new int[stats.Length];

            Console.WriteLine($"Your starting level is {elf.Level}! \n");
            Console.WriteLine($"You have {pointsToSpend} points to spend.");
            Console.WriteLine("These are your stats!");
            Console.WriteLine($"Health: {elf.Health}");
            Console.WriteLine($"Speed: {elf.Speed}");
            Console.WriteLine($"Strength: {elf.Strength}");
            Console.WriteLine($"Magic: {elf.Magic}");
            Console.WriteLine($"Luck: {elf.Luck}");
            Console.WriteLine($"Level: {elf.Level}");
            Console.WriteLine($"Exp: {elf.EXP}");

            PlayerStats playerStats = new PlayerStats();
            AdventureGenerator story = new AdventureGenerator();



            for (int i = 0; i < stats.Length; i++)
            {
                if(pointsToSpend >= pointsRemaining)
                {
                Console.Write($"{stats[i]}: ");
                statsValues[i] = int.Parse(Console.ReadLine());
                pointsRemaining = pointsToSpend -= statsValues[i];
                Console.WriteLine($"Remaining points to spend: {pointsRemaining} \n");

                } else
                {
                    Console.WriteLine($"You fool! you have no more points!");
                }
            }

            playerStats.UpdateStats(statsValues, Name, CharacterClass);


            Console.WriteLine("These are your stats!");
            Console.WriteLine($"Health: {playerStats.Health}");
            Console.WriteLine($"Speed: {playerStats.Speed}");
            Console.WriteLine($"Strength: {playerStats.Strength}");
            Console.WriteLine($"Magic: {playerStats.Magic}");
            Console.WriteLine($"Luck: {playerStats.Luck}");

            RollDiceOptions();
            int diceRollOne = RollDice();
            int diceRolltwo = RollDice();
            int diceRollthree = RollDice();
            Console.WriteLine("one moment while we create your story... \n");
            string userStory = story.GenerateAdventure(diceRollOne, diceRolltwo, diceRollthree, null, playerStats).Result;
            Console.WriteLine(userStory);
            //Console.WriteLine("What will you do?...  (chapter one) \n");
            //string firstChoice = Console.ReadLine(); // 
            //string chapterTwo = story.GenerateAdventure(null, null, null, firstChoice, playerStats).Result;
            //Console.WriteLine(chapterTwo); 
            Console.WriteLine("What will you do now?... (chapter two) \n");
            string secondChoice = Console.ReadLine(); // enter 3
            string chapterThree = story.GenerateAdventure(null, null, null, secondChoice, playerStats).Result;
            Console.WriteLine(chapterThree);
            Console.WriteLine("What will you do now?... (chapter three) \n");
            string thirdChoice = Console.ReadLine(); // enter 4
            string chapterFour = story.GenerateAdventure(null, null, null, thirdChoice, playerStats).Result;
            // player might need to accept in order for the sideQuest to work
            Console.WriteLine(chapterFour);

            return elf;
        }

        return null;
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



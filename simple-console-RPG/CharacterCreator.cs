using System;
//using Internal;

public class CharacterStats
{

    // character basics
    public string Name { get; set; }
    public int StartingClass { get; set; }
    public int StartingLevel { get; set; }
 
    // attributes
    public int Health { get; set; }
    public int HP { get; set; }
    public int Speed { get; set; }
    public int Strength { get; set; }
    public int Magic { get; set; }
    public int Luck { get; set; }
    public int Level { get; set; }
    public int EXP { get; set; }

    // int staminaLevel / int endurance == staminaLevel++

    public void PrintOptions()
    {
        Console.WriteLine("Please choose an option...");
        Console.WriteLine("1. Elf"); // range; speed;
        Console.WriteLine("2. Mage"); // magic; 
        Console.WriteLine("3. Hero"); // strength; speed;
        Console.WriteLine("4. Hobbit"); // luck;
        Console.WriteLine("5. Dwarf"); // strengh;
        Console.WriteLine("6. Barbarian"); // hp; strength;
        Console.WriteLine("7. Theif"); // speed; luck;
        StartingClass = int.Parse(Console.ReadLine());


    }

    public void CreateCharacter() 
    {
        Console.WriteLine("Enter your character name ");
        Name = Console.ReadLine();
        Console.WriteLine($"Welcome {Name}!");
        Console.WriteLine("Choose your class");
        PrintOptions();
        int times = 100;

        if (StartingClass == 1)
        {
            Console.WriteLine("You have chosen Elf!");
            CharacterStats elf = new CharacterStats();
            elf.Health = 1;
            elf.HP = 10;
            elf.Speed = 5;
            elf.Strength = 2;
            elf.Magic = 0;
            elf.Luck = 0;
            elf.Level = 1;
            elf.EXP = 0;
            Console.WriteLine("These are your stats");
            Console.WriteLine($"Health: {elf.Health}");
            Console.WriteLine($"HP: {elf.HP}");
            Console.WriteLine($"Speed: {elf.Speed}");
            Console.WriteLine($"Strength: {elf.Strength}");
            Console.WriteLine($"Magic: {elf.Magic}");
            Console.WriteLine($"Luck: {elf.Luck}");
            Console.WriteLine($"Level: {elf.Level}");
            Console.WriteLine($"Exp: {elf.EXP}");
            Console.ReadLine();
        }
    }


    int diceRoll = new Random().Next(1, 10);

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Console RPG");
        CharacterStats player = new CharacterStats();
        player.CreateCharacter();
    }

}




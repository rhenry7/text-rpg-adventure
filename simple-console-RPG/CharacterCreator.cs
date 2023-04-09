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
    public int Range { get; set; } 
    public int Magic { get; set; }
    public int Luck { get; set; }
    public int Level { get; set; }
    public int EXP { get; set; }
    public int Dice { get; set; }
    // add range system for Elf
    // int staminaLevel / int endurance == staminaLevel++ (too complex for now?)
    // use RollDice for enemy interaction, enemy attack;

    public void RollDice()
    {
        Dice = new Random().Next(1, 10);
    }

    public void PrintOptions()
    {
        Console.WriteLine("Choose your starting class...");
        Console.WriteLine("1. Elf - range; speed;"); // range; speed;
        Console.WriteLine("2. Mage - magic;"); // magic; 
        Console.WriteLine("3. Hero - strength; speed;"); // strength; speed;
        Console.WriteLine("4. Hobbit - luck"); // luck;
        Console.WriteLine("5. Dwarf - strength;"); // strengh;
        Console.WriteLine("6. Barbarian - hp; strength;"); // hp; strength;
        Console.WriteLine("7. Theif - speed; luck"); // speed; luck;
        StartingClass = int.Parse(Console.ReadLine());
    }

    public void CreateCharacter() 
    {
        Console.WriteLine("Enter your character name ");
        Name = Console.ReadLine();
        Console.WriteLine($"Welcome {Name}!");
        Console.WriteLine("Choose your class");
        PrintOptions();

        if (StartingClass == 1)
        {
            Console.WriteLine("You have chosen Elf!");
            CharacterStats elf = new CharacterStats();

            elf.Health = 1;
            elf.HP = 10;
            elf.Speed = 4;
            elf.Strength = 2;
            elf.Range = 4;
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
    //int diceRoll = new Random().Next(1, 10);
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Console RPG");
        CharacterStats player = new CharacterStats();
        player.CreateCharacter();
    }

}




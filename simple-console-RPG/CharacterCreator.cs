using System;
//using Internal;

public class CharacterStats
{

    // character basics
    public string Name { get; set; }
    public string StartingClass { get; set; }
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

    public void printOptions()
    {
        Console.WriteLine("Please choose an option...");
        Console.WriteLine("1. Elf"); // range; speed;
        Console.WriteLine("2. Mage"); // magic; 
        Console.WriteLine("3. Hero"); // strength; speed;
        Console.WriteLine("4. Hobbit"); // luck;
        Console.WriteLine("5. Dwarf"); // strengh;
        Console.WriteLine("6. Barbarian"); // hp; strength;
        Console.WriteLine("7. Theif"); // speed; luck;

    }

    public void CreateCharacter() 
    {
        Console.WriteLine("Enter your character name ");
        Name = Console.ReadLine();
        //this.health = health;
        //this.hp = hp;
        //this.speed = speed;
        //this.strength = strength;
        //this.magic = magic;
        //this.luck = luck;
        //this.level = level;
        //this.exp = exp;

    }


    int diceRoll = new Random().Next(1, 10);

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Console RPG");
    }

}




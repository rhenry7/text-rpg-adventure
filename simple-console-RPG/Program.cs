using System;


public class characterStats
{

    // character basics
    private string characterName = "";
    private string startingClass = "";
    private int startingLevel = 0;

    // attributes
    private int health = 0;
    private int hp = 0;
    private int speed = 0;
    private int strength = 0;
    private int magic = 0;
    private int luck = 0;
    private int level = 1;
    private int exp = 0;
    // int staminaLevel / int endurance == staminaLevel++

    public characterStats(int health, int hp, int speed, int strength, int magic, int luck, int level, int exp) 
    {
        this.health = health;
        this.hp = hp;
        this.speed = speed;
        this.strength = strength;
        this.magic = magic;
        this.luck = luck;
        this.level = level;
        this.exp = exp;

    }

    public string GetCharName(object v) => characterName;
    public string GetStartingClass() => startingClass;
    public int GetStartingLevel() => startingLevel;
    public int GetHealth() => health;
    public int GetHP() => hp;
    public int GetSpeed() => speed;
    public int GetStrength() => strength;
    public int GetMagic() => magic;
    public int GetLuck() => luck;
    public int GetLevel() => level;
    public int GetExp() => exp;

    // add setters;
    public void SetCharName(string name) => characterName = name;
    public void SetStartingClass(string characterClass) => startingClass = characterClass;
    public void setStartingLevel(int level) => startingLevel = level;
    public void setHealth(int healthValue) => health = healthValue;
    public void setHP(int hpValue) => hp = hpValue;
    public void setSpeed(int speedValue) => speed = speedValue;
    public void setStrength(int strengthValue) => strength = strengthValue;
    public void setMagic(int magicValue) => magic = magicValue;
    public void setLuck(int luckValue) => luck = luckValue;
    public void setLevel(int characterLevel) => level = characterLevel;
    public void setExp(int expPoints) => exp = expPoints;

    int diceRoll = new Random().Next(1, 10);

    static void Main(string[] args)
    {
        void createCharacter(characterStats currentCharacter)
        {
            Console.WriteLine("Welcome to console RPG");
            Console.WriteLine("What is the name of your character? ");
            string characterName = Console.ReadLine();
            if(characterName != null)
            {
                currentCharacter.SetCharName(characterName);
                Console.WriteLine("Welcome " + characterName + "!");
            } 
            
        }
    }

}




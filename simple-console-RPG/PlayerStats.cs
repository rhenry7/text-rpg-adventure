using System;
namespace simple_console_RPG;

public class PlayerStats
{
    public int Health { get; set; }
    public int Speed { get; set; }
    public int Strength { get; set; }
    public int Magic { get; set; }
    public int Luck { get; set; }
    public string Name { get; set; }
    public string PlayerClass { get; set; }

    public void UpdateStats(int[] statsValues, string playerName, string playerClass)
    {
        // physical  attributes
        Health = statsValues[0] * 10;
        Speed = statsValues[1];
        Strength = statsValues[2];
        Magic = statsValues[3];
        Luck = statsValues[4];
        // character bio
        Name = playerName;
        PlayerClass = playerClass;
    }
}

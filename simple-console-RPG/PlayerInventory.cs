namespace simple_console_RPG;

public class PlayerInventory
{
    public int Helmet { get; set; }
    public int Shield { get; set; }
    public int Sword { get; set; }
    public int Staff { get; set; }
    public int Charm { get; set; }
    public int Bow { get; set; }
    public int Armour { get; set; }

    public void UpdatePlayerInventory(int[] statsValues)
    {
        Helmet = statsValues[0];
        Shield = statsValues[1];
        Sword = statsValues[2];
        Staff = statsValues[3];
        Charm = statsValues[4];
        Bow = statsValues[5];
        Armour = statsValues[6];
    }
}
using System.Net.Http.Headers;
using System.Text.Json;
using OpenAI_API;
using System.IO;
using simple_console_RPG;


public class AdventureGenerator
{
    public enum Location
    {
        Castle,
        Forrest,
        Desert,
        Swamp,
        Dungeon,
        Village,
        Mountain,
        Volcano,
        IceKingdom,
        RockyVillage,
    }


    public enum Enemy
    {
        GoblinArmy,
        EvilElves,
        MagicMushroomPeople,
        SwampMonsters,
        DungeonDragons,
        GiantTrolls,
        MagicBirds,
        LavaLizardsWithSwords,
        RobotPenguins,
        Knights,
    }

    public enum Objective
    {
        SaveThePrincess,
        AvengeYourFather,
        DestroyTheMagicOrb,
        FindTheAncientTreasure,
        TheSacredSword,
        HealTheAncientTree,
        DefeatTheEmperor,
        DeliverTheChosenOneToSafety,
        FindTheKeysToTheHiddenKingdom,
        SlayTheDragon,
    }


    public enum Colors { Red, Orange, Green, Blue, Black };
    public enum Fruits { Apple = 1, Banana = 2, Orange = 3, Peer = 4 };


    public async Task<string> GenerateAdventure()
    // int Location, int Enemy, int Objective
    {
        apiInfo apiKey = new apiInfo();
        OpenAIAPI api = new OpenAIAPI(apiKey.ApiInfo());
        var chat = api.Chat.CreateConversation();

        int DiceRollOne = new Random().Next(1, 10);
        int DiceRollTwo = new Random().Next(1, 10);
        int DiceRollThree = new Random().Next(1, 10);
        chat.AppendSystemMessage($"Write a console RPG, 80s style dungeones and dragons story/plot, one paragraph, \n" +
            $" set in {Enum.GetName(typeof(Location), DiceRollOne)} " +
            $"\n where the character must {Enum.GetName(typeof(Objective), DiceRollTwo)}" +
            $"and the objective is to {Enum.GetName(typeof(Objective), DiceRollThree)}");
        string response = await chat.GetResponseFromChatbotAsync();
        return response;

    }
}

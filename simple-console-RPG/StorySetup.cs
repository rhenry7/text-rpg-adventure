using System;
namespace simple_console_RPG
{
	public class StorySetup
	{
		public string[] EnemyType()

		{

			string[] enemyArray = {
                "Goblin Army",
                "Evil Elves",
                "Savage Wizards",
                "religous orcs",
                "zealous goblins",
                "Bandits",
                "Vampires",
                "Golemns",
                "Ghost pirates",
                "Mad Alchemists",
                "Sand Lizard",
                "Giant Spiders",
                "Mutant Spiders",
                "Mountain Barbarians",
                "Undead Knights",
                "Ghost Mercenaries",
                "Lava Lizards with swords",
                "Fire Dragon",
                "Poison Spiders",
                "Orcs",
                "Giants",
                "Ice Dragon"};
			return enemyArray;
		}

        public string[] EnemyLeader()

        {

			string[] enemyLeaderArray = new string[] { "Council of Elves", "Immortal Wizard", "Tribal Cheiftan", "The forgotten King", "The Dragon shapeshifter", "Prideful General", "The Mad King", "High Priests", "Ancient Pirate King" };
            return enemyLeaderArray;
        }

        public string[] Elemental()
        {
            string[] elementTypes = { "Fire", "Water", "Earth", "Air", "Lightning", "Ice", "Light", "Darkness", "Poison", "Acid", "Wind", "Metal", "Holy", "Shadow", "Nature", "Ghost", "Gravity", "Alien" };
            return elementTypes;
        }

        public string[] EnemyAdjective()

        {
            string[] leaderAdjective = new string[] {
                "Bald",
                "Skinny",
                "Zelous",
                "Drunk",
                "Silent",
                "Deaf",
                "Blind",
                "Short",
                "Tall",
                "Feeble" };
            return leaderAdjective;
        }

        public string[] Location()

        {

            string[] locationArray = new string[] { "Castle", "Forest", "Desert", "Swamp", "Rainy Dungeon", "Grassy Village", "Mountain", "Volcano", "Ice Kingdom", "Jungle" };
            return locationArray;
        }

        public string[] NPC()
        {
            string[] npcArray = new string[] { "Wise old person", "young child", "lost banker" };
            return npcArray;

        }

        public string[] PlayerObjective()
        {
            string[] objectiveArray = new string[] { "Save the princess", "Find the keys to the hidden kingdom", "Defeat the emperor", "find who killed your father", "solve the puzzle of the time treasure", "Heal the ancient tree", "uncover the corruption in the city", "help the rebels free their people", "Vanquish the wizard", "Destroy the magic orb" };
            return objectiveArray;
        }

        public string[] EnemyObjective()
        {
            string[] enemyMotivationArray = new string[] {
            "Ecco-terrism, The enemy is an environmental extremist who believes that humans are destroying the planet and seeks to stop them at any cost by destroying all of humanity",
            "Cult fanaticism, The enemy is Mysteriously motivated by religious fundamentalism and seeks to establish a theocratic state based on their particular belief system.",
            "Greed, The enemy is a greedy villian who believes that wealth is the most important goal, even if it means exploiting others and destroying any thing in their path.",
            "Pride, The enemy is a representative of a powerful foreign kindgom that seeks to exploit or dominate the world for their own benefit.",
            "Madness, The enemy is driven insane by dark magic, a curse, or a traumatic event, causing them to lash out against others.",
            "Redemption, The enemy seeks redemption for past misdeeds, but believes that the only way to achieve it is by committing a great act of evil.",
            "Survival, The enemy believs they are justified and is simply trying to survive in a dangerous world and views the players as a threat to their own existence.",
            "Honor, The enemy is Fiercely motivated by a sense of honor or duty, and believes that their actions are justified by a higher moral code.",
            "Independence, The enemy is fighting for independence and autonomy from a larger, more powerful state or kingdom, motivated by a desire for self-determination and sovereignty... they will destroy everything to achieve this",
            "Love, The enemy believs they are the hero is motivated by love for another person, whether it be a romantic partner, family member, or friend, and will do anything to protect them." };
            return enemyMotivationArray;
        }
        
        }
}




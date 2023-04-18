using System;
namespace simple_console_RPG
{
	public class StoryParameters



	{

        // firstPhase - combat
        // secondPhase - villianConflict
        // thirdPhase(sideQuest)
        // conclusion [good, bad, sad] - outcomech

        public string TemplateForStory(string location,string enemy,  string element, string enemyMotivation, string playerGoal) 

		{

			string story = $"the setting is a {location}"
				+ $"the cha {element} {enemy} is motivated by {enemyMotivation}"
                + $"the player wants to {playerGoal} and must complete the adventure, journey through {location} to accomplish this goal";

            return story;
		}

        public string SecondPhase(string location, string enemy, string combatDescription, int damage)
        {

            StorySetup adjectives = new StorySetup();
            string[] enemyAttack = adjectives.Elemental();
            string[] enemyDescription = adjectives.EnemyAdjective();
        
            string combatPhase = $"the player has just fought a {enemyDescription[new Random().Next(enemyDescription.Length)]} {enemy}" +
                $"the battle was {combatDescription} and the enemy wounded the player with a {enemyAttack[new Random().Next(enemyDescription.Length)]} has lost {damage} health"
                + $"the player now continues the journey, after having made more progress in the {location}, mildly injured... one paragraph";

            return combatPhase;
        }

        public string ThirdPhase(string location, string enemy, string combatDescription, int damage)
        {

            StorySetup adjectives = new StorySetup();
            string[] enemyAttack = adjectives.Elemental();
            string[] enemyDescription = adjectives.EnemyAdjective();

            string combatPhase = $"at this stage of the quest, approaching the end, and after defeating legions of {enemy}" 
                + "the player has completed a positive meeting with a helpful stranger, the march into the enemy"
                + $"a {combatDescription} encounter with the enemy leader, the player has over come an {enemyAttack[new Random().Next(enemyDescription.Length)]} attack"
                + $"the player now continues the journey, after having made more progress in the {location}, mildly injured... one paragraph";

            return combatPhase;
        }

        public string StoryConclusion(string location, string enemy, string combatDescription, string enemyGoal, string playerGoal)
        {

            StorySetup adjectives = new StorySetup();
            string[] enemyAttack = adjectives.Elemental();
            string[] enemyDescription = adjectives.EnemyAdjective();

            string conclusion = $"at the end of the adventure, a substantial passage of time, the player has succeeded in acheiving their goal"
                + $"they have had a strong emotional experience along this journey and the {location} has been saved by the player"
                + "the leader of the enemy has been defeated"
                + $"the {enemyGoal} has been stopped and player has successfully achieved {playerGoal}"
                + $"the player has trekked for some time deep in the heart of the {location}, vanquished the {enemy}";

            return conclusion;
        }




    }
}


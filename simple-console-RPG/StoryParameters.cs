using System;
namespace simple_console_RPG
{
	public class StoryParameters



	{

        // firstPhase - combat
        // secondPhase - villianConflict
        // thirdPhase(sideQuest)
        // conclusion [good, bad, sad] - outcome


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




    }
}


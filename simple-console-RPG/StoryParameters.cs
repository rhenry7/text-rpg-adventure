using System;
namespace simple_console_RPG
{
	public class StoryParameters
	{
		public string TemplateForStory(string location,string enemy, string enemyAdjective, string enemyMotivation, string playerGoal) 

		{

			string story = $"the setting is a {location}"
				+ $"the {enemyAdjective} {enemy} is motivated by {enemyMotivation}"
                + $"the player wants to {playerGoal} and must complete the adventure, journey through {location} to accomplish this goal";
            return story;
		}

        // firstPhase - combat
        // secondPhase - villianConflict
        // thirdPhase(sideQuest)
        // conclusion [good, bad, sad] - outcome


    }
}


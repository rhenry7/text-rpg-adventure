using System;
namespace simple_console_RPG
{
	public class Grammar
	{
		public void AdventureVerbs()
		{
            // verbs
            string[] fightVerbs = new string[] { "Attack", "Strike", "Assault", "Charge", "Pummel", "Smash", "Thrash", "Beat", "Conquer", "Vanquish" };
            string[] dodgeVerbs = new string[] { "Duck", "Dodge", "Evade", "Escape", "Flee", "Jump", "Roll", "Sprint", "Tumble", "Weave" };
            // adverbs
            string[] combatAdverbs = new string[] { "Ferociously", "Savagely", "Brutally", "Viciously", "Relentlessly", "Mercilessly", "Fiercely", "Wildly", "Intensely", "Violently" };
            string[] dodgeAdverbs = new string[] { "Swiftly", "Gracefully", "Effortlessly", "Nimbly", "Quickly", "Evasively", "Skillfully", "Agilely", "Dexterously", "Acrobatically" };
            // adjectives
            string[] enemyAdjectives = new string[] { "Fierce", "Savage", "Mysterious", "Brutal", "Hateful", "Cruel", "Malignant", "Wicked", "Cunning", "Diabolical" };
            string[] magicAdverbs = new string[] { "Mystically", "Enchantingly", "Eerily", "Magically", "Spellbindingly", "Charmingly", "Spiritedly", "Ethereally", "Supernaturally", "Enigmatically" };
            // magic attacks
            string[] magicSpells = new string[] { "Fireball", "Ice Lance", "Thunderbolt", "Frost Nova", "Arcane Missile", "Shadow Bolt", "Divine Light", "Nature's Wrath", "Gravity Well", "Time Warp" };

        }
    }
}


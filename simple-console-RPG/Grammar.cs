namespace simple_console_RPG
{
	public class Grammar
    {
        public string[] GetFightVerbs()
        {
            string[] fightVerbs = new string[] { "Attack", "Strike", "Assault", "Charge", "Pummel", "Smash", "Thrash", "Beat", "Conquer", "Vanquish" };
            return fightVerbs;
        }

        public string[] GetDodgeVerbs()
        {
            string[] dodgeVerbs = new string[] { "Duck", "Dodge", "Evade", "Escape", "Flee", "Jump", "Roll", "Sprint", "Tumble", "Weave" };
            return dodgeVerbs;
        }

        public string[] GetCombatAdverbs()
        {
            string[] combatAdverbs = new string[] { "Ferociously", "Savagely", "Brutally", "Viciously", "Relentlessly", "Mercilessly", "Fiercely", "Wildly", "Intensely", "Violently" };
            return combatAdverbs;
        }

        public string[] GetEnemyAdjectives()
        {
            string[] enemyAdjectives = new string[] { "Cruel", "Savage", "Mysterious", "Brutal", "Hateful", "Lazy", "Malignant", "Wicked", "Cunning", "Genius" };
            return enemyAdjectives;
        }

        public string[] MagicAdverbs()
        {
            string[] magicAdverbs = new string[] { "Mystically", "Enchantingly", "Eerily", "Magically", "Spellbindingly", "Charmingly", "Spiritedly", "Ethereally", "Supernaturally", "Enigmatically" };
            return magicAdverbs;
        }

        public string[] MagicSpells()
        {
            string[] magicSpells = new string[] { "Fireball", "Ice Lance", "Thunderbolt", "Frost Nova", "Arcane Missile", "Shadow Bolt", "Divine Light", "Nature's Wrath", "Gravity Well", "Time Warp" };
            return magicSpells;

        }

        // Define other methods to return other arrays as needed
    }
}


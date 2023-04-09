using System;
namespace simple_console_RPG
{
	public class Dice
	{

        public int DiveValue { get; set; }
        public Dice()
		{
            DiveValue = new Random().Next(1, 10);

        }
    }
}


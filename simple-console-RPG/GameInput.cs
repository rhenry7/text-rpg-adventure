namespace simple_console_RPG
{
    public static class GameInput
    {
        // reads one trimmed line; ends the game if input is closed (Ctrl+D / end of piped input)
        public static string ReadLineOrQuit()
        {
            string? line = Console.ReadLine();
            if (line == null)
            {
                Console.WriteLine("\nFarewell, adventurer...");
                Environment.Exit(0);
            }
            return line.Trim();
        }
    }
}
